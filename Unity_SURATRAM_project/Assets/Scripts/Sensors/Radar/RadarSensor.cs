using UnityEngine;
using System;


/// <summary>
/// This class represents a radar
/// </summary>
public class RadarSensor : MonoBehaviour
{
    /// <summary>Offset of the beginning of the ray.</summary>
    public Vector3 rayBeginningOffset;

    /// <summary>Orientation of the ray.</summary>
    public float rayHorizontalAngle;
    public float rayVerticalAngle;

    /// <summary>The vehicle the radar pertains to.</summary>
    public GameObject vehicle;

    /// <summary>The point where the ray hit.</summary>
    private RaycastHit hit;

    /// <summary>Ray max distance</summary>
    public float rayMaxDistance;

    /// <summary>Ray of the radar.</summary>
    private Ray ray;

    /// <summary>Id of the radar.</summary>
    public int id;

    /// <summary>Event when a screenshot is taken.</summary>
    public static event NewPoints OnScanned;
    /// <summary>Event when a screenshot is taken.</summary>
    public delegate void NewPoints(double time, int id, RadarDist data);


    /// <summary>Update function.</summary>
    public void FixedUpdate()
    {
        ray.origin = gameObject.transform.position + rayBeginningOffset;
        ray.direction = gameObject.GetComponent<LineRenderer>().GetPosition(1);
        RadarDist rd;
        if (Physics.Raycast(ray, out hit, rayMaxDistance))
        {
            rd = new RadarDist(true, hit.distance, rayMaxDistance);
        }
        else
        {
            rd = new RadarDist(false, 0f, rayMaxDistance);
        }
        OnScanned((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds), id, rd);
        hit = new RaycastHit();
    }

    /// <summary>Start function.</summary>
    public void Start()
    {
        hit = new RaycastHit();
        ray = new Ray();
        updateLocalRay();
    }

    /// <summary>Update the ray position in global position.</summary>
    public void updateGlobalRay()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, gameObject.transform.position + rayBeginningOffset);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, gameObject.transform.position + rayBeginningOffset + rayMaxDistance * Mathf.Cos(rayVerticalAngle * Mathf.PI / 180) * new Vector3(Mathf.Sin(rayHorizontalAngle * Mathf.PI / 180 + vehicle.transform.eulerAngles.y * Mathf.PI / 180), Mathf.Sin(rayVerticalAngle * Mathf.PI / 180), Mathf.Cos(rayHorizontalAngle * Mathf.PI / 180 + vehicle.transform.eulerAngles.y * Mathf.PI / 180)));
    }

    /// <summary>Update the ray position in local position.</summary>
    public void updateLocalRay()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, rayBeginningOffset);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, rayBeginningOffset + rayMaxDistance * Mathf.Cos(rayVerticalAngle * Mathf.PI / 180) * new Vector3(Mathf.Sin(rayHorizontalAngle * Mathf.PI / 180 + vehicle.transform.eulerAngles.y * Mathf.PI / 180), Mathf.Sin(rayVerticalAngle * Mathf.PI / 180), Mathf.Cos(rayHorizontalAngle * Mathf.PI / 180 + vehicle.transform.eulerAngles.y * Mathf.PI / 180)));
    }
}
