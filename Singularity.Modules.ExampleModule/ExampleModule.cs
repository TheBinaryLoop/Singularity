using System;
using Singularity.Base;

namespace Singularity.Modules.ExampleModule
{
    public class ExampleModule : IModule
    {
        public string Name => "ExampleModule";

        public Version Version => new Version(0, 1, 0, 1);

        public ExampleModule()
        {
            Console.WriteLine($"[{Name} v{Version}] Constructing...");
            Console.WriteLine($"[{Name} v{Version}] Constructed");
        }

        public bool Initialize()
        {
            Console.WriteLine($"[{Name} v{Version}] Initializing...");
            Console.WriteLine($"[{Name} v{Version}] Initialized");
            return true;
        }

        public bool Activate()
        {
            Console.WriteLine($"[{Name} v{Version}] Activating...");
            Console.WriteLine($"[{Name} v{Version}] Activated");
            return true;
        }

        public bool Deactivate()
        {
            Console.WriteLine($"[{Name} v{Version}] Deactivating...");
            Console.WriteLine($"[{Name} v{Version}] Deactivated");
            return true;
        }

    }
}
