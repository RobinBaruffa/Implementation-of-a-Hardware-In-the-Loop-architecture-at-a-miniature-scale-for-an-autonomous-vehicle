using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class represents a radar
/// </summary>
public class SpeedSensor : MonoBehaviour
{
    /// <summary>Id of the radar.</summary>
    public int id;

    /// <summary>Store the last data sent.</summary>
    private Vector3 lastPosition;
    public WheelCollider wheelColliderRearLeft;
    public WheelCollider wheelColliderRearRight;

    /// <summary>Event when a screenshot is taken.</summary>
    public static event NewPoints OnScanned;
    /// <summary>Event when a screenshot is taken.</summary>
    public delegate void NewPoints(float time, int id, SpeedData data);


    /// <summary>Update function.</summary>
    public void FixedUpdate()
    {
        //Vector3 selfGlobalPosition = gameObject.transform.position;
        //float selfSpeed = (selfGlobalPosition.magnitude - lastPosition.magnitude)/ Time.fixedDeltaTime;

        SpeedData data = new SpeedData(Time.time, 0.5f*(wheelColliderRearLeft.rpm + wheelColliderRearRight.rpm));

        //lastPosition = selfGlobalPosition;

        OnScanned(Time.time, id, data);
    }

    /// <summary>Start function.</summary>
    public void Start()
    {
        lastPosition = gameObject.transform.position;
    }

    /// <summary>
    /// Converts a vector3 to a 3-tuple.
    /// </summary>
    /// <param name="v">The vector to convert.</param>
    /// <returns>The tuple of triple coordinates x, y, z.</returns>
    public static (float x, float y, float z) vector3ToTuple(Vector3 v)
    {
        return (v.x, v.y, v.z);
    }
}
