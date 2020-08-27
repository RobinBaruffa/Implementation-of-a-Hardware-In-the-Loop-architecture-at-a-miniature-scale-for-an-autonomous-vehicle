using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Sensor exportation class for a radar.
/// </summary>
public class SensorRadarExport : SensorExport
{
    /// <summary>Data of the sensor.</summary>
    new public List<RadarData> sensorData { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    public SensorRadarExport(string name, int id) : base(name, id) { sensorData = new List<RadarData>(); }


    /// <summary>
    /// Saves the current collected image on the given timestamp.
    /// </summary>
    /// <param name="time">Time of the data</param>
    /// <param name="radar data">The image in png format</param>
    public void Save(double time, int id, RadarDist data)
    {
        if (id == this.id)
        {
            sensorData.Add(new RadarData(time, data));
        }
    }

    /// <summary>
    /// Clear radar data
    /// </summary>
    public override void clearData()
    {
        sensorData.Clear();
    }
}
/// <summary>Data of obtained with a radar ray</summary>
public class RadarDist
{
    /// <summary>Boolean, true if it has hit, false otherwise.</summary>
    public bool isHit;
    /// <summary>If the ray has hit,e the distance of the obstacle.</summary>
    public float distance;
    /// <summary>The maximum distance of detection</summary>
    public float maxDistance;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="isHit">True if the ray has hit, false otherwise.</param>
    /// <param name="distance">If the ray has hit, the distance of the obstacle.</param>
    /// <param name="maxDistance">Maximum distance of detection</param>
    public RadarDist(bool isHit, float distance, float maxDistance)
    {
        this.isHit = isHit;
        this.distance = distance;
        this.maxDistance = maxDistance;
    }
}
/// <summary>The class gathering timestamped informations of a radar measure.</summary>
public class RadarData : SensorData
{
    /// <summary>The measure of the radar.</summary>
    new public RadarDist data;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="time">The timestamp of the measure.</param>
    /// <param name="isHit">True if the ray has hit, false otherwise.</param>
    /// <param name="distance">If the ray has hit, the distance of the obstacle.</param>
    public RadarData(double timestamp, bool isHit, float distance, float maxDistance) : base(timestamp)
    {
        data = new RadarDist(isHit, distance, maxDistance);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="time">The timestamp of the measure.</param>
    /// <param name="data">The measure of the radar.</param>
    [JsonConstructor]
    public RadarData(double timestamp, RadarDist data) : base(timestamp)
    {
        this.data = data;
    }
}
