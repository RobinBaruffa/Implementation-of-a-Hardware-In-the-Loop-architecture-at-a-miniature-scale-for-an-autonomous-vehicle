using System.Collections.Generic;

/// <summary>
/// Sensor exportation class for a lidar
/// </summary>
public class SensorLidarExport : SensorExport
{
    /// <summary>Data of the sensor.</summary>
    new public List<LidarData> sensorData { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    public SensorLidarExport(string name, int id) : base(name, id) { sensorData = new List<LidarData>(); }

    /// <summary>
    /// NOT WORKING
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    /// <param name="e">The event to retrieve the data of the simulated sensor.</param>
    /*
    public SensorLidarExport(string name, int id, Action<float, LinkedList<SphericalCoordinate>> e) : base(name, id) { e += Save; }
    */

    /// <summary>
    /// Saves the current collected points on the given timestamp.
    /// </summary>
    /// <param name="time">Time of the data.</param>
    /// <param name="hits">The ray data</param>
    public void Save(float time, int id, LinkedList<SphericalCoordinate> hits)
    {
        if (hits.Count != 0)
        {
            if (id == this.id)
            {
                sensorData.Add(new LidarData(time, hits));
                // Debug.Log(hits.First.Value.ToString());
            }
        }
    }

    /// <summary>
    /// Clear lidar data
    /// </summary>
    public override void clearData()
    {
        sensorData.Clear();
    }
}

/// <summary>
/// A data sample returned by the lidar
/// </summary>
public class LidarData : SensorData
{
    /// <summary>The position of the obstacle given the ray.</summary>
    new public LinkedList<SphericalCoordinate> data { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="timestamp">The timestamp of the data.</param>
    /// <param name="data">The positon of the obstacle given the ray.</param>
    public LidarData(float timestamp, LinkedList<SphericalCoordinate> data) : base(timestamp)
    {
        this.data = data;
    }
}

