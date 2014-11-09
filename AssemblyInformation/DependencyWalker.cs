using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AssemblyInformation
{
    internal class DependencyWalker
    {
        private readonly Dictionary<string, Binary> assemblyMap = new Dictionary<string, Binary>();
        private readonly List<string> errors = new List<string>();

        public event EventHandler<ReferringAssemblyStatusChangeEventArgs> ReferringAssemblyStatusChanged;

        public IEnumerable<Binary> FindDependencies(AssemblyName assemblyName, bool recursive, out List<string> loadErrors)
        {
            loadErrors = new List<string>();
            this.assemblyMap.Clear();
            this.errors.Clear();
            List<Binary> dependencies = new List<Binary>();

            Assembly assembly = FindAssembly(assemblyName);
            if (null == assembly)
            {
                this.errors.Add("Failed to load: " + assemblyName.FullName);
            }
            else
            {
                this.FindDependencies(assembly, recursive, 0);
                dependencies.AddRange(this.assemblyMap.Values.OrderBy(p => p.FullName));
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
            this.assemblyMap.Clear();
            this.errors.Clear();
            this.FindDependencies(assembly, recursive, 0);
            List<Binary> dependencies = new List<Binary>(this.assemblyMap.Values).OrderBy(p => p.FullName).ToList();

            foreach (var dependency in dependencies)
            {
                Trace.WriteLine(String.Format("{0} => {1}", dependency.DisplayName, dependency.IsSystemBinary));
            }

            return dependencies;
        }

        public IEnumerable<string> FindReferringAssemblies(Assembly testAssembly, string directory, bool recursive)
        {
            List<string> referringAssemblies = new List<string>();
            List<string> binaries = new List<string>();
            try
            {
                this.ReferringAssemblyStatusChanged(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = "Finding all binaries" });
                this.FindAssemblies(new DirectoryInfo(directory), binaries, recursive);
            }
            catch (Exception)
            {
                this.UpdateProgress(Resource.FailedToListBinaries, -2);
                return null;
            }

            if (binaries.Count == 0)
            {
                return referringAssemblies;
            }

            int baseDirPathLength = directory.Length;
            if (!directory.EndsWith("\\"))
            {
                baseDirPathLength++;
            }

            int i = 0;
            foreach (var binary in binaries)
            {
                string message = String.Format(Resource.AnalyzingAssembly, Path.GetFileName(binary));
                int progress = (i++ * 100) / binaries.Count;
                if (progress == 100)
                {
                    progress = 99;
                }

                if (!this.UpdateProgress(message, progress))
                {
                    return referringAssemblies;
                }

                try
                {
                    Assembly assembly = Assembly.LoadFile(binary);
                    DependencyWalker dw = new DependencyWalker();
                    List<string> loadErrors;
                    var dependencies = dw.FindDependencies(assembly, false, out loadErrors);
                    if (null == dependencies)
                    {
                        continue;
                    }

                    if (
                        dependencies.Where(
                            p =>
                            String.Compare(p.FullName, testAssembly.FullName, StringComparison.OrdinalIgnoreCase) == 0)
                            .Count() > 0)
                    {
                        referringAssemblies.Add(binary.Remove(0, baseDirPathLength));
                    }

                    this.errors.AddRange(loadErrors);
                }
                catch (ArgumentException)
                {
                }
                catch (FileLoadException)
                {
                }
                catch (FileNotFoundException)
                {
                }
                catch (BadImageFormatException)
                {
                }
            }

            return referringAssemblies.OrderBy(p => p);
        }

        private static Assembly FindAssembly(AssemblyName assName)
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
                    {
                        continue;
                    }

                    string[] parts = assemblyName.Split(',');
                    if (parts.Length > 0)
                    {
                        string name = parts[0].Trim() + ".dll";
                        assemblyName = name;
                    }
                }
                catch (ArgumentException)
                {
                }
                catch (IOException)
                {
                }
                catch (BadImageFormatException)
                {
                }
            }

            return assembly;

            ////if (loaded && null != assembly)
            ////{
            ////    foreach (AssemblyName referencedAssembly in assembly.GetReferencedAssemblies())
            ////    {
            ////        if (!bareMode)
            ////        {
            ////            if (assemblyMap.ContainsKey(referencedAssembly.FullName)) return;
            ////            assemblyMap[referencedAssembly.FullName] = 1;
            ////        }
            ////        else
            ////        {
            ////            if (assemblyMap.ContainsKey(referencedAssembly.Name)) return;
            ////            assemblyMap[referencedAssembly.Name] = 1;
            ////        }
            ////        if(recursive)
            ////        {
            ////            FindDependencies(referencedAssembly, true, ++level);
            ////        }
            ////    }
            ////}
            ////else
            ////{
            ////    errors.Add("Failed to load: " + assName.FullName);
            ////}
        }

        private void FindAssemblies(DirectoryInfo directoryInfo, List<string> binaries, bool recursive)
        {
            string message = string.Format(Resource.AnalyzingFolder, directoryInfo.Name);
            if (!this.UpdateProgress(message, -1))
            {
                return;
            }

            binaries.AddRange(directoryInfo.GetFiles("*.dll").Select(fileInfo => fileInfo.FullName));
            binaries.AddRange(directoryInfo.GetFiles("*.exe").Select(fileInfo => fileInfo.FullName));

            if (recursive)
            {
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    this.FindAssemblies(directory, binaries, true);
                }
            }
        }

        private void FindDependencies(Assembly assembly, bool recursive, int level)
        {
            foreach (AssemblyName referencedAssembly in assembly.GetReferencedAssemblies())
            {
                string name = referencedAssembly.FullName;

                if (this.assemblyMap.ContainsKey(name))
                {
                    continue;
                }

                this.assemblyMap[name] = new Binary(referencedAssembly);

                if (AssemblyInformationLoader.SystemAssemblies.Where(p => referencedAssembly.FullName.StartsWith(p)).Count() > 0)
                {
                    this.assemblyMap[name].IsSystemBinary = true;
                    continue;
                }

                if (recursive)
                {
                    Assembly referredAssembly = FindAssembly(referencedAssembly);

                    if (null != referredAssembly)
                    {
                        this.assemblyMap[name] = new Binary(referencedAssembly, referredAssembly);
                        this.FindDependencies(referredAssembly, true, ++level);
                    }
                }
            }
        }

        private bool UpdateProgress(string message, int progress)
        {
            if (null != this.ReferringAssemblyStatusChanged)
            {
                var eventArg = new ReferringAssemblyStatusChangeEventArgs { StatusText = message, Progress = progress };
                this.ReferringAssemblyStatusChanged(this, eventArg);
                return !eventArg.Cancel;
            }

            return true;
        }
    }
}
