using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sensor exportation class for a camera
/// </summary>
public class SensorCameraExport : SensorExport
{
    /// <summary>Data of the sensor.</summary>
    public new List<CameraData> sensorData { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    public SensorCameraExport(string name, int id) : base(name, id) { sensorData = new List<CameraData>(); }

    /// <summary>
    /// Saves the current collected image on the given timestamp.
    /// </summary>
    /// <param name="time">Time of the data</param>
    /// <param name="img">The image in png format</param>
    public void Save(double time, int id, byte[] img)
    {
        if (id == this.id)
        {
            sensorData.Add(new CameraData(time, img));
        }
    }

    /// <summary>
    /// Clear camera data
    /// </summary>
    public override void clearData()
    {
        sensorData.Clear();
    }
}
/// <summary>
/// A data sample returned by the camera
/// </summary>
public class CameraData : SensorData
{
    /// <summary>The position of the obstacle given the ray.</summary>
    new public byte[] data { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="timestamp">The timestamp of the data.</param>
    /// <param name="data">The image in png format.</param>
    public CameraData(double timestamp, byte[] data) : base(timestamp)
    {
        this.data = data;
    }
}

