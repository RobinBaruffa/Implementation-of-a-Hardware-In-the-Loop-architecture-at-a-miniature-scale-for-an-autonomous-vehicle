/// <summary>
/// Stores and formats data to export it.
/// </summary>
public abstract class SensorExport
{
    /// <summary>Name of the sensor</summary>
    public string name { get; private set; }

    /// <summary>Id of the sensor</summary>
    public int id { get; private set; }

    /// <summary>Data of the sensor.</summary>
    public SensorData sensorData { get; private set; }

    /// <summary>
    /// Constractor
    /// </summary>
    /// <param name="name">Name of the sensor.</param>
    /// <param name="id">Id of the sensor.</param>
    public SensorExport(string name, int id)
    {
        this.name = name;
        this.id = id;
    }

    /// <summary>
    /// Copy Constractor
    /// </summary>
    /// <param name="sensor">The sensor to copy.</param>
    public SensorExport(SensorExport sensor)
    {
        this.name = sensor.name;
        this.id = sensor.id;
    }

    /// <summary>
    /// Clear sensor data.
    /// </summary>
    public abstract void clearData();

    /*
    public void mergeData()
    {
        JSONCreator.mergeData(this);
        data = new List<object>();
    }

    /// <summary>
    /// Test function. Do not use it !
    /// </summary>
    public void test()
    {
        string json = JsonConvert.SerializeObject(this, Formatting.Indented);
        Debug.Log(json);
    }
    */

}

public abstract class SensorData
{
    /// <summary>The timestamp of the data.</summary>
    public double timestamp { get; private set; }
    /// <summary>The measure of the radar.</summary>
    public int data;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="timestamp">The timestamp of the measure.</param>
    public SensorData(double timestamp)
    {
        this.timestamp = timestamp;
    }
}
