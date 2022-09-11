namespace TrackMobility.Database.Models;

internal class Overview
{
    public int Version { get; set; }

    public string DeviceName { get; set; }

    public string DeviceManufacturer { get; set; }

    public string DeviceModel { get; set; }

    public string DeviceVersion { get; set; }

    public DevicePlatform DevicePlatform { get; set; }

    public List<TransportEntry> TransportEntryList { get; set; }

    public Overview()
    {
        Version = Defines.CurrentVersion;
        DeviceName = string.Empty;
        DeviceManufacturer = string.Empty;
        DeviceModel = string.Empty;
        DeviceVersion = string.Empty;
        DevicePlatform = new DevicePlatform();
        TransportEntryList = new List<TransportEntry>();
    }
}
