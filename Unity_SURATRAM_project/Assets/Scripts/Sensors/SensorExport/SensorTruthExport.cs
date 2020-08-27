using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// Sensor exportation class for a radar.
/// </summary>
public class SensorTruthExport : SensorExport
{
    /// <summary>Data of the sensor.</summary>
    new public List<TruthData> sensorData { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name of the Sensor</param>
    /// <param name="id">The id of the Sensor</param>
    public SensorTruthExport(string name, int id) : base(name, id) { sensorData = new List<TruthData>(); }


    /// <summary>
    /// Saves the current collected image on the given timestamp.
    /// </summary>
    /// <param name="time">Time of the data</param>
    /// <param name="radar data">The image in png format</param>
    public void Save(float time, int id, TruthData data)
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
public class TruthData : SensorData
{
    /// <summary>The velocity of the vehicle.</summary>
    private (float x, float y, float z) selfVelocity;
    /// <summary>The interdistance from the center of the vehicle to the center of the other entity.</summary>
    public List<float> interdistance;
    /// <summary>The interdistance of the two closest point that belongs to the vehicle and entity.</summary>
    public List<float> interdistanceShortestPoint;
    /// <summary>The vehicle global position.</summary>
    public (float x, float y, float z) selfGlobalPosition;
    /// <summary>Entities global position.</summary>
    public List<(float x, float y, float z)> entitiesGlobalPosition;
    /// <summary>Entities global angle.</summary>
    public List<(float x, float y, float z)> entitiesGlobalAngle;
    /// <summary>Enitities angle relative to the vehicle.</summary>
    public List<(float x, float y, float z)> entitiesRelativeAngle;
    /// <summary>The vehicle global angle.</summary>
    public (float x, float y, float z) selfGlobalAngle;
    /// <summary>Entities position relative from the vehicle.</summary>
    public List<(float x, float y, float z)> entitiesRelativePosition;
    /// <summary>Entities velocity.</summary>
    public List<(float x, float y, float z)> entitiesVelocity;
    /// <summary>Type of the entites, it is the exact same order than the previous List<> attributes.</summary>
    public List<EntityType> entitiesRecognition;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="time">The timestamp of the measure.</param>
    /// <param name="selfVelocity">The velocity of the vehicle.</param>
    /// <param name="interdistance">The interdistance from the center of the vehicle to the center of the other entity.</param>
    /// <param name="interdistanceShortestPoint">The interdistance of the two closest point that belongs to the vehicle and entity.</param>
    /// <param name="selfGlobalPosition">The vehicle global position.</param>
    /// <param name="entitiesGlobalPosition">Entities global position.</param>
    /// <param name="entitiesRelativePosition">Entities position relative from the vehicle.</param>
    /// <param name="entitiesVelocity">Entities velocity.</param>
    /// <param name="entitiesRecognition">Type of the entites, it is the exact same order than the previous List<> attributes.</param>
    /// <param name="entitiesGlobalAngle">Entities global angle.</param>
    /// <param name="entitiesRelativeAngle">Enitities angle relative to the vehicle.</param>
    /// <param name="selfGlobalAngle">The vehicle global angle.</param>
    [JsonConstructor]
    public TruthData(float timestamp, (float x, float y, float z) selfVelocity, List<float> interdistance, List<float> interdistanceShortestPoint, (float x, float y, float z) selfGlobalPosition, List<(float x, float y, float z)> entitiesGlobalPosition, List<(float x, float y, float z)> entitiesGlobalAngle, List<(float x, float y, float z)> entitiesRelativeAngle, (float x, float y, float z) selfGlobalAngle, List<(float x, float y, float z)> entitiesRelativePosition, List<(float x, float y, float z)> entitiesVelocity, List<EntityType> entitiesRecognition) : base(timestamp)
    {
        this.selfVelocity = selfVelocity;
        this.interdistance = interdistance;
        this.interdistanceShortestPoint = interdistanceShortestPoint;
        this.selfGlobalPosition = selfGlobalPosition;
        this.entitiesGlobalPosition = entitiesGlobalPosition;
        this.entitiesGlobalAngle = entitiesGlobalAngle;
        this.entitiesRelativeAngle = entitiesRelativeAngle;
        this.selfGlobalAngle = selfGlobalAngle;
        this.entitiesRelativePosition = entitiesRelativePosition;
        this.entitiesVelocity = entitiesVelocity;
        this.entitiesRecognition = entitiesRecognition;
    }

    public TruthData(float timestamp, (float x, float y, float z) selfGlobalPosition, (float x, float y, float z) selfGlobalAngle, (float x, float y, float z)[] entitiesGlobalPosition) : base(timestamp)
    {
        this.selfGlobalPosition = selfGlobalPosition;
        this.selfGlobalAngle = selfGlobalAngle;
        this.entitiesGlobalPosition = new List<(float x, float y, float z)>(entitiesGlobalPosition);
    }

    public TruthData(float timestamp, (float x, float y, float z) selfVelocity, float[] interdistance, float[] interdistanceShortestPoint, (float x, float y, float z) selfGlobalPosition, (float x, float y, float z)[] entitiesGlobalPosition, (float x, float y, float z)[] entitiesGlobalAngle, (float x, float y, float z)[] entitiesRelativeAngle, (float x, float y, float z) selfGlobalAngle, (float x, float y, float z)[] entitiesRelativePosition, (float x, float y, float z)[] entitiesVelocity, List<EntityType> entitiesRecognition) : base(timestamp)
    {
        this.selfVelocity = selfVelocity;
        this.interdistance = new List<float>(interdistance);
        this.interdistanceShortestPoint = new List<float>(interdistanceShortestPoint);
        this.selfGlobalPosition = selfGlobalPosition;
        this.entitiesGlobalPosition = new List<(float x, float y, float z)>(entitiesGlobalPosition);
        this.entitiesGlobalAngle = new List<(float x, float y, float z)>(entitiesGlobalAngle);
        this.entitiesRelativeAngle = new List<(float x, float y, float z)>(entitiesRelativeAngle);
        this.selfGlobalAngle = selfGlobalAngle;
        this.entitiesRelativePosition = new List<(float x, float y, float z)>(entitiesRelativePosition);
        this.entitiesVelocity = new List<(float x, float y, float z)>(entitiesVelocity);
        this.entitiesRecognition = entitiesRecognition;
    }
}

public enum EntityType
{
    Bus,
    Car,
    Pedestrian,
    RoadSign
}

public class Vector3Serilazable
{
    public float x;
    public float y;
    public float z;

    public Vector3Serilazable(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}
