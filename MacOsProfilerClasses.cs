using System.Collections.Generic;

namespace MacOsSystemProfiler
{
    // Generated with:
    // https://json2csharp.com

#pragma warning disable IDE1006 // Naming Styles

    public class SPHardwareDataType
    {
        public string _name { get; set; }
        public string cpu_type { get; set; }
        public string current_processor_speed { get; set; }
        public string l2_cache_core { get; set; }
        public string l3_cache { get; set; }
        public string machine_model { get; set; }
        public string machine_name { get; set; }
        public int number_processors { get; set; }
        public int packages { get; set; }
        public string physical_memory { get; set; }
        public string serial_number { get; set; }
    }

    public class Dhcp
    {
        public string dhcp_domain_name_servers { get; set; }
        public int dhcp_lease_duration { get; set; }
        public string dhcp_message_type { get; set; }
        public string dhcp_routers { get; set; }
        public string dhcp_server_identifier { get; set; }
        public string dhcp_subnet_mask { get; set; }
    }

    public class DNS
    {
        public List<string> ServerAddresses { get; set; }
    }

    public class Ethernet
    {
        public string MACAddress { get; set; }
        public List<object> MediaOptions { get; set; }
        public string MediaSubType { get; set; }
    }

    public class AdditionalRoute
    {
        public string DestinationAddress { get; set; }
        public string SubnetMask { get; set; }
    }

    public class IPv4
    {
        public List<AdditionalRoute> AdditionalRoutes { get; set; }
        public List<string> Addresses { get; set; }
        public string ARPResolvedHardwareAddress { get; set; }
        public string ARPResolvedIPAddress { get; set; }
        public string ConfigMethod { get; set; }
        public string ConfirmedInterfaceName { get; set; }
        public string InterfaceName { get; set; }
        public string NetworkSignature { get; set; }
        public string Router { get; set; }
        public List<string> SubnetMasks { get; set; }
    }

    public class IPv6
    {
        public List<string> Addresses { get; set; }
        public string ConfigMethod { get; set; }
        public string ConfirmedInterfaceName { get; set; }
        public string InterfaceName { get; set; }
        public string NetworkSignature { get; set; }
        public List<int> PrefixLength { get; set; }
        public string Router { get; set; }
    }

    public class Proxies
    {
        public List<string> ExceptionsList { get; set; }
        public string FTPPassive { get; set; }
    }

    public class SleepProxy
    {
        public string _name { get; set; }
        public int MarginalPower { get; set; }
        public int Metric { get; set; }
        public int Portability { get; set; }
        public int TotalPower { get; set; }
        public int Type { get; set; }
    }

    public class SPNetworkDataType
    {
        public string _name { get; set; }
        public Dhcp dhcp { get; set; }
        public DNS DNS { get; set; }
        public Ethernet Ethernet { get; set; }
        public string hardware { get; set; }
        public string @interface { get; set; }
        public List<string> ip_address { get; set; }
        public IPv4 IPv4 { get; set; }
        public IPv6 IPv6 { get; set; }
        public Proxies Proxies { get; set; }
        public List<SleepProxy> sleep_proxies { get; set; }
        public int spnetwork_service_order { get; set; }
        public string type { get; set; }
    }

    public class SPSoftwareDataType
    {
        public string _name { get; set; }
        public string boot_mode { get; set; }
        public string boot_volume { get; set; }
        public string kernel_version { get; set; }
        public string os_version { get; set; }
        public string uptime { get; set; }
        public string user_name { get; set; }
    }

    public class PhysicalDrive
    {
        public string device_name { get; set; }
        public string is_internal_disk { get; set; }
        public string media_name { get; set; }
        public string medium_type { get; set; }
        public string partition_map_type { get; set; }
        public string protocol { get; set; }
        public string smart_status { get; set; }
    }

    public class SPStorageDataType
    {
        public string _name { get; set; }
        public string bsd_name { get; set; }
        public string file_system { get; set; }
        public object free_space_in_bytes { get; set; }
        public string ignore_ownership { get; set; }
        public string mount_point { get; set; }
        public PhysicalDrive physical_drive { get; set; }
        public object size_in_bytes { get; set; }
        public string volume_uuid { get; set; }
        public string writable { get; set; }
    }

    public class SystemProfile
    {
        public List<SPHardwareDataType> SPHardwareDataType { get; set; }
        public List<SPNetworkDataType> SPNetworkDataType { get; set; }
        public List<SPSoftwareDataType> SPSoftwareDataType { get; set; }
        public List<SPStorageDataType> SPStorageDataType { get; set; }
    }

#pragma warning restore IDE1006 // Naming Styles


}
