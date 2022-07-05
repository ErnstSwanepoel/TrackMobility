namespace TrackMobility;

public partial class MainPage : ContentPage
{
	private int tripCounter = 0;
    private string database = Path.Combine(FileSystem.AppDataDirectory, "tripLog.json");

	public MainPage()
	{
		InitializeComponent();
        SaveBikeTrip.Clicked += UpdateTripCounter;
        SaveBusTrip.Clicked += UpdateTripCounter;
        SaveTrainTrip.Clicked += UpdateTripCounter;
        SaveCarTrip.Clicked += UpdateTripCounter;

        if (File.Exists(database))
        {
            tripCounter = int.Parse(File.ReadAllText(database));
            RefreshTripCounterText();
        }
    }

    private void UpdateTripCounter(object sender, EventArgs e)
    {
        tripCounter++;
        RefreshTripCounterText();
        File.WriteAllText(database, tripCounter.ToString());
    }

    private void RefreshTripCounterText()
    {
        TripCounter.Text = $"Trips saved: {tripCounter}";
        SemanticScreenReader.Announce(TripCounter.Text);
    }
}

