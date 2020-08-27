using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// Sensor exportation class for a radar.
/// </summary>
public class SensorSpeedExport : SensorExport
{
    /// <summary>Data of the sensor.</summary>
    new public List<SpeedData> sensorData { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    public SensorSpeedExport(string name, int id) : base(name, id) { sensorData = new List<SpeedData>(); }


    /// <summary>
    /// Saves the current collected image on the given timestamp.
    /// </summary>
    /// <param name="time">Time of the data</param>
    /// <param name="radar data">The image in png format</param>
    public void Save(float time, int id, SpeedData data)
    {
        if (id == this.id)
        {
            sensorData.Add(data);
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
/// <summary>The class gathering timestamped informations of a radar measure.</summary>
public class SpeedData : SensorData
{
    /// <summary>The velocity of the vehicle.</summary>
    public float selfSpeed;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="time">The timestamp of the measure.</param>
    /// <param name="selfSpeed">The speed of the vehicle.</param>
    [JsonConstructor]
    public SpeedData(float timestamp, float selfSpeed) : base(timestamp)
    {
        this.selfSpeed = selfSpeed;
    }
}
