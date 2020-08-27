using UnityEngine;
using System.Text;
using System;

/// <summary>
/// Class of the camera sensor
/// </summary>
public class CameraSensor : MonoBehaviour
{
    /// <summary>Field of View of the camera.</summary>
    public float fov = 60f;

    /// <summary>Id of the camera.</summary>
    public int id;

    /// <summary>Image resolution width.</summary>
    public int resWidth = 1024;

    /// <summary>Image resolution height.</summary>
    public int resHeight = 600;

    /// <summary>The time between each sceenshot.</summary>
    public float periodUpdate = 1f;

    /// <summary>The current time elapesed since the last screenshot.</summary>
    private float timeElapsed;

    /// <summary>Event when a screenshot is taken.</summary>
    public static event NewPoints OnScanned;
    /// <summary>Event when a screenshot is taken.</summary>
    public delegate void NewPoints(double time, int id, byte[] data);

    private Camera cam;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        cam.fieldOfView = fov;
    }


    /// <summary>Take sceenshot at intervals</summary>
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > periodUpdate)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            screenShot.Apply();
            // was there
            byte[] bytes = screenShot.EncodeToPNG();
            //System.IO.File.WriteAllBytes("/home/tiboiser/Documents/Projets/TO52/mrbot-ws/MRbot-ws/src/mr_sensor_handler/src/image_file_from_cameraSIM.png", bytes);
            OnScanned((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds), id, bytes);
            //here
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            Destroy(screenShot);
            timeElapsed = 0;
        }
    }
}
