using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace MacOsSystemProfiler
{
    public static class MacOsProfiler
    {
        public static async Task<SystemProfile> GetProfile()
        {
            using Process p = new()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"system_profiler -json SPHardwareDataType SPMemoryDataType SPNetworkDataType SPSoftwareDataType SPStorageDataType\"",
                    RedirectStandardOutput = true
                }
            };
            _ = p.Start();
            p.WaitForExit();
            var s = await p.StandardOutput.ReadToEndAsync();

            var sysProfile = JsonSerializer.Deserialize<SystemProfile>(s);

            return sysProfile;
        }
    }
}
