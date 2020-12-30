using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MacOsSystemProfiler
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var cpuProfiler = new CpuProfiler();

            var sysProfile = await MacOsProfiler.GetProfile(new ProfilerDataType[] {
                ProfilerDataType.SPHardwareDataType,
                ProfilerDataType.SPMemoryDataType,
                ProfilerDataType.SPNetworkDataType,
                ProfilerDataType.SPSoftwareDataType,
                ProfilerDataType.SPStorageDataType
            });

            foreach (var hw in sysProfile.SPHardwareDataType)
            {
                Console.WriteLine($"{hw.machine_name} {hw.physical_memory}");
            }
            foreach (var sw in sysProfile.SPSoftwareDataType)
            {
                Console.WriteLine($" {sw.os_version}");
            }
            Console.WriteLine("Storage");
            foreach (var store in sysProfile.SPStorageDataType)
            {
                Console.WriteLine($" {store._name} ({store.bsd_name})");
            }
            Console.WriteLine("Network");
            foreach (var net in sysProfile.SPNetworkDataType)
            {
                Console.WriteLine($" {net._name} ({string.Join(",", net.ip_address ?? new List<string>())})");
            }

            Console.WriteLine("Samples:");
            cpuProfiler.WaitForReady();
            for (var n = 0; n < 5; n++)
            {
                var memPressure = await MacOsMemoryPressure.GetMemoryPressure();
                Console.WriteLine($" CPU Usage:   {cpuProfiler.UserPercentage}%, {cpuProfiler.SystemPercentage}%, {cpuProfiler.IdlePercentage}%");
                Console.WriteLine($" Memory Used: {memPressure.MemoryFreePercentage}%\n");
                cpuProfiler.WaitForNextSample();
            }
        }
    }
}
