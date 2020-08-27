using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class represents a radar
/// </summary>
public class TruthSensor : MonoBehaviour
{
    /// <summary>Id of the radar.</summary>
    public int id;

    /// <summary>List of tracked GameObjects.</summary>
    public List<GameObject> trackedEntities;
    /// <summary>List of type of the tracked GameObjects.</summary>
    public List<EntityType> entitiesType;

    /// <summary>Store the last data sent.</summary>
    private TruthData lastData;

    /// <summary>Event when a screenshot is taken.</summary>
    public static event NewPoints OnScanned;
    /// <summary>Event when a screenshot is taken.</summary>
    public delegate void NewPoints(float time, int id, TruthData data);


    /// <summary>Update function.</summary>
    public void FixedUpdate()
    {
        float[] interdistance = new float[trackedEntities.Count];
        float[] interdistanceShortestPoint = new float[trackedEntities.Count];
        Vector3[] entitiesGlobalPosition = new Vector3[trackedEntities.Count];
        Vector3[] entitiesGlobalAngle = new Vector3[trackedEntities.Count];
        Vector3[] entitiesRelativePosition = new Vector3[trackedEntities.Count];
        Vector3[] entitiesRelativeAngle = new Vector3[trackedEntities.Count];
        Vector3[] entitiesVelocity = new Vector3[trackedEntities.Count];

        Vector3 selfGlobalPosition = gameObject.transform.position;
        Vector3 selfGlobalAngle = gameObject.transform.eulerAngles;
        Vector3 selfVelocity = selfGlobalPosition - tupleToVector3(lastData.selfGlobalPosition);
        for (int i = 0; i < trackedEntities.Count; i++)
        {
            entitiesGlobalPosition[i] = trackedEntities[i].transform.position;
            entitiesGlobalAngle[i] = trackedEntities[i].transform.eulerAngles;
            entitiesRelativePosition[i] = gameObject.transform.InverseTransformPoint(entitiesGlobalPosition[i]); // Transform from global to local space.
            entitiesRelativeAngle[i] = entitiesGlobalAngle[i] - selfGlobalAngle;
            interdistance[i] = (entitiesRelativePosition[i]).magnitude;
            entitiesVelocity[i] = (entitiesGlobalPosition[i] - tupleToVector3(lastData.entitiesGlobalPosition[i]));
            // interdistanceShortestPoint[i] =
        }

        if (trackedEntities.Count > 0)
        {
            TruthData data = new TruthData(Time.time, vector3ToTuple(selfVelocity), interdistance, interdistanceShortestPoint, vector3ToTuple(selfGlobalPosition), vector3ToTuple(entitiesGlobalPosition), vector3ToTuple(entitiesGlobalAngle), vector3ToTuple(entitiesRelativeAngle), vector3ToTuple(selfGlobalAngle), vector3ToTuple(entitiesRelativePosition), vector3ToTuple(entitiesVelocity), entitiesType);

            lastData = data;

            OnScanned(Time.time, id, data);
        }
    }

    /// <summary>Start function.</summary>
    public void Start()
    {
        Vector3[] entitiesGlobalPosition = new Vector3[trackedEntities.Count];
        for (int i = 0; i < trackedEntities.Count; i++)
        {
            entitiesGlobalPosition[i] = trackedEntities[i].transform.position;
        }
        lastData = new TruthData(Time.time, vector3ToTuple(gameObject.transform.position), vector3ToTuple(gameObject.transform.eulerAngles), vector3ToTuple(entitiesGlobalPosition));
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

    /// <summary>
    /// Converts a tuple to a vector3.
    /// </summary>
    /// <param name="tuple">A tuple of the triple coordinates x, y, z to convert.</param>
    /// <returns>The converted coordinates into a vector.</returns>
    public static Vector3 tupleToVector3((float x, float y, float z) tuple)
    {
       return new Vector3(tuple.x, tuple.y, tuple.z);
    }

    /// <summary>
    /// Converts an array of vector3 to an array of  3-tuple.
    /// </summary>
    /// <param name="v">The array vector vector to convert.</param>
    /// <returns>The array of tuple of triple coordinates x, y, z.</returns>
    public static (float x, float y, float z)[] vector3ToTuple(Vector3[] v)
    {
        (float x, float y, float z)[] arr = new (float x, float y, float z)[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            arr[i].x = v[i].x;
            arr[i].y = v[i].y;
            arr[i].z = v[i].z;
        }
        return arr;
    }
}
