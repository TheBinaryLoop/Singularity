using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Singularity.Base
{
    public class ModuleManager<T>
    {
        public List<T> Modules { get; }

        public ModuleManager() => Modules = new List<T>();

        public void LoadModules(string path, string searchPattern = "*.dll", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            if (!typeof(T).IsInterface)
                throw new ArgumentException("T must be an interface.");

            path = Path.GetFullPath(path);

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} isn't a valid directory.");

            ICollection<Assembly> assemblies = new List<Assembly>();
            foreach (string moduleFile in Directory.GetFiles(path, searchPattern, searchOption))
            {
                Assembly assembly = Assembly.LoadFile(moduleFile);
                foreach (AssemblyName assemblyName in assembly.GetReferencedAssemblies())
                {
                    Console.WriteLine($"{assemblyName.Name}");
                }
                //Console.WriteLine($"{assembly.FullName.Replace(", Version=", "_v").Split(',')[0]}");
                assemblies.Add(assembly);
            }

            Type moduleType = typeof(T);
            ICollection<Type> moduleTypes = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly == null)
                {
                    //TODO: Warn the user. Because this should not normaly happen.
                    continue;
                }

                try
                {
                    Type[] types = assembly.GetTypes();

                    foreach (Type type in types)
                    {
                        //Console.Write($"{type.FullName}: ");
                        //if (type.IsInterface || type.IsAbstract || type.GetCustomAttribute(typeof(ModuleAttribute)) == null)
                        if (type.IsInterface || type.IsAbstract)
                        {
                            //Console.WriteLine("Rejected");
                            continue;
                        }
                        if (type.GetInterface(moduleType.FullName) != null)
                        {
                            //Console.WriteLine("Accepted");
                            moduleTypes.Add(type);
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    // TODO: Proper logging
                    foreach (Exception exception in ex.LoaderExceptions)
                    {
                        Console.WriteLine(exception);
                    }
                }
            }

            foreach (Type type in moduleTypes)
            {
                T module = (T)Activator.CreateInstance(type);
                Modules.Add(module);
            }
        }


    }
}
