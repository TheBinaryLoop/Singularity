using System;
using System.IO;
using Singularity.Base;

namespace Singularity
{
    class Program
    {
        static void Main(string[] args)
        {
            ModuleManager<IModule> moduleManager = new ModuleManager<IModule>();

            moduleManager.LoadModules(Path.GetFullPath("Modules"), "*.dll", SearchOption.AllDirectories);

            // Initializing and activating
            foreach (var module in moduleManager.Modules)
            {
                module.Initialize();
                module.Activate();
            }

            // Deactivating
            foreach (var module in moduleManager.Modules)
            {
                module.Deactivate();
            }

            Console.ReadLine();
        }
    }
}
