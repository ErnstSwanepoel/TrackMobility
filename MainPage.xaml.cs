using System.Text.Json;
using System.Text.Json.Serialization;
using TrackMobility.Database.Models;
using TrackMobility.Resources.Shared;

namespace TrackMobility;

public partial class MainPage : ContentPage
{
    private string databasePath = Path.Combine(FileSystem.AppDataDirectory, "tripLog.json");
    private Overview databaseOverview;

	public MainPage()
	{
		InitializeComponent();
        SaveBikeTrip.Clicked += UpdateTripCounter;
        SaveBusTrip.Clicked += UpdateTripCounter;
        SaveTrainTrip.Clicked += UpdateTripCounter;
        SaveCarTrip.Clicked += UpdateTripCounter;

        if (File.Exists(databasePath))
        {
            try
            {
                databaseOverview = JsonSerializer.Deserialize<Overview>(File.ReadAllText(databasePath));
            }
            catch (JsonException)
            {
            }
        }

        if (databaseOverview == null
            || databaseOverview.Version != Database.Defines.CurrentVersion)
        {
            databaseOverview = new Overview()
            {
                Version = Database.Defines.CurrentVersion,
                DeviceName = DeviceInfo.Name,
                DeviceManufacturer = DeviceInfo.Manufacturer,
                DeviceModel = DeviceInfo.Model,
                DevicePlatform = DeviceInfo.Platform,
                DeviceVersion = DeviceInfo.VersionString,
                TransportEntryList = new List<TransportEntry>()
            };
        }

        RefreshTripCounterText();
    }

    private void UpdateTripCounter(object sender, EventArgs e)
    {
        var method = TransportMethod.Bike;
        var buttonText = ((Button)sender).Text;
        switch (buttonText)
        {
            case SharedButton.TextForBike:
                method = TransportMethod.Bike;
                break;

            case SharedButton.TextForBus:
                method = TransportMethod.Bus;
                break;

            case SharedButton.TextForCar:
                method = TransportMethod.Car;
                break;

            case SharedButton.TextForTrain:
                method = TransportMethod.Train;
                break;

            default:
                throw new NotImplementedException($"Invalid transport type {buttonText} for {nameof(UpdateTripCounter)} method.");
        }

        databaseOverview.TransportEntryList.Add(new TransportEntry() 
            { 
                DateTime = DateTime.Now,
                Method = method
            });

        RefreshTripCounterText();
        File.WriteAllText(databasePath, JsonSerializer.Serialize(databaseOverview));
    }

    private void RefreshTripCounterText()
    {
        TripCounter.Text = $"Trips saved: {databaseOverview.TransportEntryList.Count}";
        SemanticScreenReader.Announce(TripCounter.Text);
    }
}

