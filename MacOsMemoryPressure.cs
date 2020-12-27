using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MacOsSystemProfiler
{
    public class MacOsMemoryPressure
    {
        private MacOsMemoryPressure()
        {
        }

        public int PagesPurgeable { get; private set; }
        public int PagesPurged { get; private set; }
        public int PagesFree { get; private set; }
        public int Swapins { get; private set; }
        public int Swapouts { get; private set; }
        public int PagesActive { get; private set; }
        public int PagesInactive { get; private set; }
        public int PagesSpeculative { get; private set; }
        public int PagesThrottled { get; private set; }
        public int PagesWiredDown { get; private set; }
        public int PagesCompressor { get; private set; }
        public int PagesDecompressed { get; private set; }
        public int MemoryFreePercentage { get; private set; }
        public int Pageouts { get; private set; }
        public int Pageins { get; private set; }
        public int PagesCompressed { get; private set; }

        public static async Task<MacOsMemoryPressure> GetMemoryPressure()
        {
            using Process p = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"memory_pressure\"",
                    RedirectStandardOutput = true
                }
            };
            _ = p.Start();
            p.WaitForExit();
            var s = await p.StandardOutput.ReadToEndAsync();

            var mp = new MacOsMemoryPressure();
            var lines = s.Split('\n');
            foreach (var ln in lines)
            {
                var parts = (from line in ln.Split(':') select line.Trim()).ToArray();
                if (parts.Length == 2)
                {
                    switch (parts[0])
                    {
                        case "Pages free":
                            mp.PagesFree = int.Parse(parts[1]);
                            break;
                        case "Pages purgeable":
                            mp.PagesPurgeable = int.Parse(parts[1]);
                            break;
                        case "Pages purged":
                            mp.PagesPurged = int.Parse(parts[1]);
                            break;
                        case "Swapins":
                            mp.Swapins = int.Parse(parts[1]);
                            break;
                        case "Swapouts":
                            mp.Swapouts = int.Parse(parts[1]);
                            break;
                        case "Pages active":
                            mp.PagesActive = int.Parse(parts[1]);
                            break;
                        case "Pages inactive":
                            mp.PagesInactive = int.Parse(parts[1]);
                            break;
                        case "Pages speculative":
                            mp.PagesSpeculative = int.Parse(parts[1]);
                            break;
                        case "Pages throttled":
                            mp.PagesThrottled = int.Parse(parts[1]);
                            break;
                        case "Pages wired down":
                            mp.PagesWiredDown = int.Parse(parts[1]);
                            break;
                        case "Pages used by compressor":
                            mp.PagesCompressor = int.Parse(parts[1]);
                            break;
                        case "Pages decompressed":
                            mp.PagesDecompressed = int.Parse(parts[1]);
                            break;
                        case "Pages compressed":
                            mp.PagesCompressed = int.Parse(parts[1]);
                            break;
                        case "Pageins":
                            mp.Pageins = int.Parse(parts[1]);
                            break;
                        case "Pageouts":
                            mp.Pageouts = int.Parse(parts[1]);
                            break;
                        case "System-wide memory free percentage":
                            mp.MemoryFreePercentage = int.Parse(parts[1].Replace("%", ""));
                            break;
                        default:
                            break;
                    }
                }
            }

            return mp;
        }
    }
}
