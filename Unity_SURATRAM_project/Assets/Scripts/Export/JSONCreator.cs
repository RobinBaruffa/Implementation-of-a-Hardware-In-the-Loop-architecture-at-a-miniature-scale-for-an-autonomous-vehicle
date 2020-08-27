using System.Collections.Generic;
using System;
using SimpleJSON;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

/// <summary>
/// Class that creates a JSON ready to be send to the real vehicle
/// </summary>
public static class JSONCreator
{
    /// <summary>Keep trace of the version of the JSON (to avoid versions conflicts).</summary>
    public const string version = "0.0";

    /// <summary>The list of sensors that will send data.</summary>
    private static List<SensorExport> sensors;

    /// <summary>Destination ip address.</summary>
    private static string serverIp = "192.168.1.45";	//When running on the raspberry
    //private static string serverIp = "127.0.0.1";	//When running locally

    /// <summary>Destination port</summary>
    private static int port = 55566;
    // private static int port = 11311;

    // private static LinkedList<Thread> threads;

    /// <summary>Constructor</summary>
    static JSONCreator()
    {
        sensors = new List<SensorExport>();
        //  threads = new LinkedList<Thread>();

        // Add lidar sensor to JSON export
        SensorLidarExport s = new SensorLidarExport("Lidar", 0);
        JSONCreator.addSensor(s);
        LidarSensor.OnScanned += s.Save;

        // Add camera sensor to JSON export
        SensorCameraExport c = new SensorCameraExport("CameraLeft", 1);
        JSONCreator.addSensor(c);
        CameraSensor.OnScanned += c.Save;

        // Add camera sensor to JSON export
        c = new SensorCameraExport("CameraRight", 2);
        JSONCreator.addSensor(c);
        CameraSensor.OnScanned += c.Save;

        // Add radar sensor to JSON export
        SensorRadarExport r = new SensorRadarExport("Radar", 3);
        JSONCreator.addSensor(r);
        RadarSensor.OnScanned += r.Save;

        // Add truth sensor to JSON export
        SensorTruthExport t = new SensorTruthExport("Truth", 4);
        JSONCreator.addSensor(t);
        TruthSensor.OnScanned += t.Save;

        // Add truth sensor to JSON export
        SensorSpeedExport speed = new SensorSpeedExport("Speed", 5);
        JSONCreator.addSensor(speed);
        SpeedSensor.OnScanned += speed.Save;
    }

    public static void connect()
    {
        // Tcp connection initialisation
        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(serverIp), port);
        Socket soc = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        soc.Connect(ipe);
        networkStream = new NetworkStream(soc);
    }

    /// <summary>
    /// Adds a sensor to the list of sensors
    /// </summary>
    /// <param name="sensor">The sensor to add.</param>
    public static void addSensor(SensorExport sensor)
    {
        sensors.Add(sensor);
    }

    /// <summary>The network stream to send data.</summary>
    private static NetworkStream networkStream;

    /// <summary>
    /// Method that generates the JSON file.
    /// </summary>
    /// <returns>Returns the JSON.</returns>
    public static JSONNode generateJSON()
    {
        string json = JsonConvert.SerializeObject(sensors, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        JSONNode N = JSON.Parse("{}");
        N["version"] = version;
        // N["timestamp"] = DateTime.Now.Ticks / 1e7;
        N["timestamp"] = "42";
        N["sensor"] = json;
        // SensorsWrapper sw = new SensorsWrapper(version, "42", sensors);
        // return JSON.Parse(JsonConvert.SerializeObject(sw, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All }));
        return N;
    }

    /// <summary>
    /// Method that generates the JSON file.
    /// </summary>
    /// <param name="sensors">Give a custom sensor list to parse to JSON.</param>
    /// <returns>Returns the JSON.</returns>
    public static JSONNode generateJSON(List<SensorExport> sensors)
    {
        string json = JsonConvert.SerializeObject(sensors, Formatting.None);
        JSONNode N = JSON.Parse("{}");
        N["version"] = version;
        N["timestamp"] = DateTime.Now.Ticks / 1e7;
        N["sensor"] = json;
        return N;
    }

    public static string generateROSJSON()
    {
        SensorsWrapper sw = new SensorsWrapper(version, (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString(), sensors);
        // Debug.Log((DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds).ToString());
        return JsonConvert.SerializeObject(sw, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
    }

    /// <summary>
    /// This function send data to the given ip and port and then clears the data. Synchrone function.
    /// </summary>
    public static void sendROSData()
    {
        string s = generateROSJSON();
        networkStream.Write(Encoding.ASCII.GetBytes(s), 0, s.Length);
        networkStream.Write(Encoding.ASCII.GetBytes("\n"), 0, 1);
        clearSensorsData();
    }

    /*
    /// <summary>
    /// This function send data to the given ip and port and then clears the data.
    /// </summary>
    public static async void sendDataAsync()
    {
        List<SensorExport> oldSensors = sensors;
        sensors = new List<SensorExport>();

        // Add lidar sensor to JSON export
        SensorLidarExport s = new SensorLidarExport("Lidar", 1);
        JSONCreator.addSensor(s);
        LidarSensor.OnScanned += s.Save;

        // Add camera sensor to JSON export
        SensorCameraExport c = new SensorCameraExport("CameraLeft", 2);
        JSONCreator.addSensor(c);
        CameraSensor.OnScanned += c.Save;

        // Add camera sensor to JSON export
        c = new SensorCameraExport("CameraRight", 3);
        JSONCreator.addSensor(c);
        CameraSensor.OnScanned += c.Save;

        Debug.Log("Ici");

        //test(oldSensors);
        //await test(oldSensors);
        Task<JSONNode> t = new Task<JSONNode>(() => { Debug.Log("Create JSON"); return generateJSON(oldSensors); });
        Debug.Log("MidLa");
        test(oldSensors);
        //N.SaveToBinaryStream(networkStream);
        Debug.Log("Là");
        t.Start();
        t.Wait();//.SaveToBinaryStream(networkStream);
    }

    */
    /*
    public static void test(List<SensorExport> l)
    {
        Action action = () =>
                         {
                             Console.WriteLine("Task=begin");
                             Console.WriteLine("Task=end");
                         };
        Debug.Log("Start");
        Task<JSONNode> t = new Task<JSONNode>(() => { return generateJSON(l); });
        t.Start();
        JSONNode N = t.Result;
        N.SaveToBinaryStream(networkStream);

        // Create a task but do not start it.
        // Task t1 = new Task(action);
        // t1.Start();
        Debug.Log("End");
    }
    */

    /*
    /// <summary>
    /// This function send data to the given ip and port and then clears the data.
    /// </summary>
    public static void sendDataAsync()
    {
        List<SensorExport> oldSensors = sensors;
        sensors = new List<SensorExport>();

        // Add lidar sensor to JSON export
        SensorLidarExport s = new SensorLidarExport("Lidar", 1);
        JSONCreator.addSensor(s);
        LidarSensor.OnScanned += s.Save;

        Thread t = new Thread(new ThreadStart(delegate ()
        {
            generateJSON(oldSensors).SaveToBinaryStream(networkStream);
        }
                 ));

        threads.AddLast(t);
        t.Start();
        //t.Join();
        // Problem... If we exit the scope without Joining the thread, there is an error.
    }
    */

    /*
    /// <summary>Do not use it</summary>
    public static void joinThreads()
    {
        LinkedListNode<Thread> i = threads.First;
        while (i != null)
        {
            if (i.Value.Join(10))
            {
                LinkedListNode<Thread> old = i;
                i = i.Next;
                threads.Remove(old);
            }
            else
            {
                i = i.Next;
            }
        }
    }
    */

    /// <summary>
    /// This function send data to the given ip and port and then clears the data. Synchrone function.
    /// </summary>
    public static void sendData()
    {
        generateJSON().SaveToBinaryStream(networkStream);
        clearSensorsData();
    }

    /// <summary>
    /// Clear data from every sensor.
    /// </summary>
    public static void clearSensorsData()
    {
        foreach (SensorExport s in sensors)
        {
            s.clearData();
        }
    }

    /// <summary>
    /// Clear the sensors list.
    /// </summary>
    public static void clearSensors()
    {
        sensors.Clear();
    }

    /// <summary>
    /// Clear the data from a single given sensor of id 's.id'.
    /// </summary>
    /// <param name="s">Sensor to clear data</param>
    public static void clearSensorData(SensorExport s)
    {
        int i = 0;
        while (i < sensors.Count && sensors[i].id != s.id)
        {
            i++;
        }
        if (i < sensors.Count)
        {
            sensors[i].clearData();
        }
    }

    /// <summary>
    /// Merge already stored data with the new data in sensor.
    /// </summary>
    /// <param name="nom"><+param+></param>
    /// <returns><+returns+></returns>
    /*
    public static void mergeData<T>(T sensor) where T : SensorExport,new()
    {
        int i = 0;
        while (sensors[i]?.id != sensor.id)
        {
            i++;
        }
        if (sensors[i] != null) {
           sensors[i].data.AddRange(sensor.data);
        } else {
           sensors.Add((SensorExport) Activator.CreateInstance(typeof(T), new object[] { sensor }));
        }
    }
    */
    public class SensorsWrapper
    {
        /// <summary>Keep trace of the version of the JSON (to avoid versions conflicts).</summary>
        public string version;

        /// <summary>Timestamp of the sending message.</summary>
        public string timestamp;

        /// <summary>The list of sensors that will send data.</summary>
        public List<SensorExport> sensors;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="version">Version of the json message.</param>
        /// <param name="timestamp">Timestamp of the json message.</param>
        /// <param name="sensors">Sensor data.</param>
        public SensorsWrapper(string version, string timestamp, List<SensorExport> sensors)
        {
            this.version = version;
            this.timestamp = timestamp;
            this.sensors = sensors;
        }
    }
}
