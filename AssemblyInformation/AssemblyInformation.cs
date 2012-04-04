using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace AssemblyInformation 
{
    class AssemblyInformationLoader 
    {
        public bool JitTrackingEnabled { get; private set; }
        public bool JitOptimized { get; private set; }
        public bool IgnoreSymbolStoreSequencePoints { get; private set; }
        public bool EditAndContinueEnabled { get; private set; }
        public string AssemblyKind { get; private set; }
        public string TargetProcessor { get; private set; }
        public string AssemblyFullName { get; private set; }
        public string FrameWorkVersion { get; private set; }

        public Assembly Assembly { get; private set; }
        public DebuggableAttribute.DebuggingModes? DebuggingFlags { get; private set; }

        static readonly Dictionary<PortableExecutableKinds, string> PortableExecutableKindsNames = new Dictionary<PortableExecutableKinds, string>();
        static readonly Dictionary<ImageFileMachine, string> ImageFileMachineNames = new Dictionary<ImageFileMachine, string>();

        public static readonly List<string> SystemAssemblies = new List<string>()
                                                                    {
                                                                        "System",
                                                                        "mscorlib",
                                                                        "Windows",
                                                                        "PresentationCore",
                                                                        "PresentationFramework",
                                                                        "Microsoft.VisualC"
                                                                    };
        static AssemblyInformationLoader ()
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
            LoadInformation();
        }

        void LoadInformation()
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
                        if (!String.IsNullOrEmpty(AssemblyKind)) 
                        {
                            AssemblyKind += Environment.NewLine;
                        }
                        AssemblyKind += "- " + PortableExecutableKindsNames[kind];
                    }
                }
                //assemblyKindTextBox.Text = PortableExecutableKindsNames[portableExecutableKinds];
                TargetProcessor = ImageFileMachineNames[imageFileMachine];

                //Any CPU builds are reported as 32bit. 
                //32bit builds will have more value for PortableExecutableKinds
                if(imageFileMachine == ImageFileMachine.I386 && portableExecutableKinds == PortableExecutableKinds.ILOnly)
                {
                    TargetProcessor = "AnyCPU";
                }
            }
            
            if (debugAttribute != null)
            {
                JitTrackingEnabled = debugAttribute.IsJITTrackingEnabled;
                JitOptimized = !debugAttribute.IsJITOptimizerDisabled;
                IgnoreSymbolStoreSequencePoints = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints) != DebuggableAttribute.DebuggingModes.None;
                EditAndContinueEnabled = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.EnableEditAndContinue) != DebuggableAttribute.DebuggingModes.None;

                DebuggingFlags = debugAttribute.DebuggingFlags;
            }
            else  // No DebuggableAttribute means IsJITTrackingEnabled=false, IsJITOptimizerDisabled=false, IgnoreSymbolStoreSequencePoints=false, EnableEditAndContinue=false
            {
                JitTrackingEnabled = false;
                JitOptimized = true;
                IgnoreSymbolStoreSequencePoints = false;
                EditAndContinueEnabled = false;
                DebuggingFlags = null;
            }

            AssemblyFullName = Assembly.FullName;

            FrameWorkVersion = Assembly.ImageRuntimeVersion;
        }
    }

    class Binary
    {
        public string FullName { get; private set; }
        public string DisplayName { get; private set; }
        public string FullPath { get; private set; }
        public bool IsSystemBinary { get; set; }

        public Binary(AssemblyName assemblyName, string fullPath = null, bool isSystemBinary = false)
        {
            FullName = assemblyName.FullName;
            DisplayName = assemblyName.Name;
            FullPath = fullPath;
            IsSystemBinary = isSystemBinary;
        }

        public Binary(AssemblyName assemblyName, Assembly assembly):this(assemblyName, assembly.Location, assembly.GlobalAssemblyCache)
        {
        }
    }

    class DependencyWalker
    {
        readonly Dictionary<string, Binary> assemblyMap = new Dictionary<string, Binary>();
        private readonly List<string> errors = new List<string>();

        public IEnumerable<Binary> FindDependencies(AssemblyName assemblyName, bool recursive, out List<string> loadErrors)
        {
            loadErrors = new List<string>();
            assemblyMap.Clear();
            errors.Clear();
            List<Binary> dependencies = new List<Binary>();
            
            Assembly assembly = FindAssembly(assemblyName);
            if (null == assembly) 
            {
                errors.Add("Failed to load: " + assemblyName.FullName);
            }
            else
            {
                FindDependencies(assembly, recursive, 0);
                dependencies.AddRange(assemblyMap.Values.OrderBy(p => p.FullName));
            }

            foreach (var dependency in dependencies)
            {
                Trace.WriteLine(String.Format("{0} => {1}", dependency.DisplayName, dependency.IsSystemBinary));
            }
            return dependencies;
        }

        public IEnumerable<Binary> FindDependencies(Assembly assembly, bool recursive, out List<string> loadErrors) 
        {
            loadErrors = new List<string>();
            assemblyMap.Clear();
            errors.Clear();
            FindDependencies(assembly, recursive, 0);
            List<Binary> dependencies = new List<Binary>(assemblyMap.Values).OrderBy(p => p.FullName).ToList();

            foreach (var dependency in dependencies) 
            {
                Trace.WriteLine(String.Format("{0} => {1}", dependency.DisplayName, dependency.IsSystemBinary));
            }
            return dependencies;
        }

        static Assembly FindAssembly(AssemblyName assName) 
        {
            int retryCount = 0;
            Assembly assembly = null;
            string assemblyName = assName.FullName;

            while (retryCount < 2) 
            {
                retryCount++;
                try 
                {
                    if (!File.Exists(assemblyName)) 
                    {
                        assembly = Assembly.Load(assemblyName);
                    }
                    else 
                    {
                        FileInfo fileInfo = new FileInfo(assemblyName);
                        assembly = Assembly.LoadFile(fileInfo.FullName);
                    }
                    break;
                }
                catch (FileNotFoundException) 
                {
                    if (assemblyName.EndsWith(".dll"))
                        continue;
                    string[] parts = assemblyName.Split(',');
                    if (parts.Length > 0) {
                        string name = parts[0].Trim() + ".dll";
                        assemblyName = name;
                    }
                }
                catch (ArgumentException) { }
                catch (IOException) { }
                catch (BadImageFormatException) { }
            }
            return assembly;

            //if (loaded && null != assembly) 
            //{
            //    foreach (AssemblyName referencedAssembly in assembly.GetReferencedAssemblies()) 
            //    {
            //        if (!bareMode) 
            //        {
            //            if (assemblyMap.ContainsKey(referencedAssembly.FullName)) return;
            //            assemblyMap[referencedAssembly.FullName] = 1;
            //        }
            //        else 
            //        {
            //            if (assemblyMap.ContainsKey(referencedAssembly.Name)) return;
            //            assemblyMap[referencedAssembly.Name] = 1;
            //        }
            //        if(recursive)
            //        {
            //            FindDependencies(referencedAssembly, true, ++level);
            //        }
            //    }
            //}
            //else 
            //{
            //    errors.Add("Failed to load: " + assName.FullName);
            //}
        }   

        private void FindDependencies(Assembly assembly, bool recursive, int level)
        {
            foreach (AssemblyName referencedAssembly in assembly.GetReferencedAssemblies()) 
            {
                string name = referencedAssembly.FullName;

                if (assemblyMap.ContainsKey(name)) continue;
                assemblyMap[name] = new Binary(referencedAssembly);
                
                if(AssemblyInformationLoader.SystemAssemblies.Where(p => referencedAssembly.FullName.StartsWith(p)).Count() >0)
                {
                    assemblyMap[name].IsSystemBinary = true;
                    continue;
                }

                if (recursive)
                {
                    Assembly referredAssembly = FindAssembly(referencedAssembly);
                    
                    if (null != referredAssembly )
                    {
                        assemblyMap[name] = new Binary(referencedAssembly, referredAssembly);
                        FindDependencies(referredAssembly, true, ++level);
                    }
                }
            }
        }   		

        public IEnumerable<string> FindReferringAssemblies(Assembly testAssembly, string directory, bool recursive) 
        {
            List<string> referringAssemblies = new List<string>();
            List<string> binaries = new List<string>();
            try
            {
                ReferringAssemblyStatusChanged(this,
                                               new ReferringAssemblyStatusChangeEventArgs
                                                   {StatusText = "Finding all binaries"});
                FindAssemblies(new DirectoryInfo(directory), binaries, recursive);
            }
            catch (Exception ex)
            {
                UpdateProgress(Resource.FailedToListBinaries, -2);
                return null;
            }

            if (binaries.Count == 0) return referringAssemblies;
            int baseDirPathLength = directory.Length;
            if (!directory.EndsWith("\\")) baseDirPathLength++;

            int i =0;
            foreach (var binary in binaries)
            {
                string message = String.Format(Resource.AnalyzingAssembly, Path.GetFileName(binary));
                int progress = (i++ * 100) / binaries.Count;
                if (progress == 100) progress = 99;
                if (!UpdateProgress(message, progress)) return referringAssemblies;

                try 
                {
                    Assembly assembly = Assembly.LoadFile(binary);
                    DependencyWalker dw = new DependencyWalker();
                    List<string> loadErrors;
                    var dependencies = dw.FindDependencies(assembly, false, out loadErrors);
                    if (null == dependencies) continue;
                    if (dependencies.Where(p => String.Compare(p.FullName, testAssembly.FullName, StringComparison.OrdinalIgnoreCase) == 0).Count() > 0) 
                    {
                        referringAssemblies.Add(binary.Remove(0, baseDirPathLength));
                    }
                    errors.AddRange(loadErrors);
                }
                catch (ArgumentException) { }
                catch (FileLoadException) { }
                catch (FileNotFoundException) { }
                catch (BadImageFormatException) { }
            }
            
            return referringAssemblies.OrderBy(p =>p);
        }

        void FindAssemblies(DirectoryInfo directoryInfo, List<string> binaries, bool recursive)
        {
            string message = string.Format(Resource.AnalyzingFolder, directoryInfo.Name);
            if (!UpdateProgress(message, -1)) return;

            binaries.AddRange(directoryInfo.GetFiles("*.dll").Select(fileInfo => fileInfo.FullName));
            binaries.AddRange(directoryInfo.GetFiles("*.exe").Select(fileInfo => fileInfo.FullName));

            if (recursive)
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    FindAssemblies(directory, binaries, true);
                }
            }
        }

        bool UpdateProgress(string message, int progress) 
        {
            if (null != ReferringAssemblyStatusChanged) 
            {
                var eventArg = new ReferringAssemblyStatusChangeEventArgs { StatusText = message, Progress = progress };
                ReferringAssemblyStatusChanged(this, eventArg);
                return !eventArg.Cancel;
            }
            return true;
        }

        public event EventHandler<ReferringAssemblyStatusChangeEventArgs> ReferringAssemblyStatusChanged;
    }

    class ReferringAssemblyStatusChangeEventArgs:EventArgs
    {
        public string StatusText { get; set; }
        public int Progress { get; set; }
        public bool Cancel { get; set; }
    }
}

