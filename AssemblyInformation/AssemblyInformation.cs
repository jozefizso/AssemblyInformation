using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace AssemblyInformation
{
    internal class AssemblyInformationLoader
    {
        public static readonly List<string> SystemAssemblies = new List<string>()
                                                                    {
                                                                        "System",
                                                                        "mscorlib",
                                                                        "Windows",
                                                                        "PresentationCore",
                                                                        "PresentationFramework",
                                                                        "Microsoft.VisualC"
                                                                    };

        private static readonly Dictionary<ImageFileMachine, string> ImageFileMachineNames = new Dictionary<ImageFileMachine, string>();

        private static readonly Dictionary<PortableExecutableKinds, string> PortableExecutableKindsNames = new Dictionary<PortableExecutableKinds, string>();

        static AssemblyInformationLoader()
        {
            PortableExecutableKindsNames[PortableExecutableKinds.ILOnly] = "Contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.";
            PortableExecutableKindsNames[PortableExecutableKinds.NotAPortableExecutableImage] = "Not in portable executable (PE) file format.";
            PortableExecutableKindsNames[PortableExecutableKinds.PE32Plus] = "Requires a 64-bit platform.";
            PortableExecutableKindsNames[PortableExecutableKinds.Required32Bit] = "Can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.";
            PortableExecutableKindsNames[PortableExecutableKinds.Unmanaged32Bit] = "Contains pure unmanaged code.";

            ImageFileMachineNames[ImageFileMachine.I386] = "Targets a 32-bit Intel processor.";
            ImageFileMachineNames[ImageFileMachine.IA64] = "Targets a 64-bit Intel processor.";
            ImageFileMachineNames[ImageFileMachine.AMD64] = "Targets a 64-bit AMD processor.";
        }

        public AssemblyInformationLoader(Assembly assembly)
        {
            Assembly = assembly;
            this.LoadInformation();
        }

        public Assembly Assembly { get; private set; }

        public string AssemblyFullName { get; private set; }

        public string AssemblyKind { get; private set; }

        public DebuggableAttribute.DebuggingModes? DebuggingFlags { get; private set; }

        public bool EditAndContinueEnabled { get; private set; }

        public string FrameWorkVersion { get; private set; }

        public bool IgnoreSymbolStoreSequencePoints { get; private set; }

        public bool JitOptimized { get; private set; }

        public bool JitTrackingEnabled { get; private set; }

        public string TargetProcessor { get; private set; }

        private void LoadInformation()
        {
            DebuggableAttribute debugAttribute = Assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().FirstOrDefault();

            var modules = Assembly.GetModules(false);
            if (modules.Length > 0)
            {
                PortableExecutableKinds portableExecutableKinds;
                ImageFileMachine imageFileMachine;
                modules[0].GetPEKind(out portableExecutableKinds, out imageFileMachine);

                foreach (PortableExecutableKinds kind in Enum.GetValues(typeof(PortableExecutableKinds)))
                {
                    if ((portableExecutableKinds & kind) == kind && kind != PortableExecutableKinds.NotAPortableExecutableImage)
                    {
                        if (!String.IsNullOrEmpty(this.AssemblyKind))
                        {
                            this.AssemblyKind += Environment.NewLine;
                        }

                        this.AssemblyKind += "- " + PortableExecutableKindsNames[kind];
                    }
                }
                ////assemblyKindTextBox.Text = PortableExecutableKindsNames[portableExecutableKinds];
                this.TargetProcessor = ImageFileMachineNames[imageFileMachine];

                // Any CPU builds are reported as 32bit.
                // 32bit builds will have more value for PortableExecutableKinds
                if (imageFileMachine == ImageFileMachine.I386 && portableExecutableKinds == PortableExecutableKinds.ILOnly)
                {
                    this.TargetProcessor = "AnyCPU";
                }
            }

            if (debugAttribute != null)
            {
                this.JitTrackingEnabled = debugAttribute.IsJITTrackingEnabled;
                this.JitOptimized = !debugAttribute.IsJITOptimizerDisabled;
                this.IgnoreSymbolStoreSequencePoints = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints) != DebuggableAttribute.DebuggingModes.None;
                this.EditAndContinueEnabled = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.EnableEditAndContinue) != DebuggableAttribute.DebuggingModes.None;

                this.DebuggingFlags = debugAttribute.DebuggingFlags;
            }
            else
            {
                // No DebuggableAttribute means IsJITTrackingEnabled=false, IsJITOptimizerDisabled=false, IgnoreSymbolStoreSequencePoints=false, EnableEditAndContinue=false
                this.JitTrackingEnabled = false;
                this.JitOptimized = true;
                this.IgnoreSymbolStoreSequencePoints = false;
                this.EditAndContinueEnabled = false;
                this.DebuggingFlags = null;
            }

            this.AssemblyFullName = Assembly.FullName;

            this.FrameWorkVersion = Assembly.ImageRuntimeVersion;
        }
    }
}
