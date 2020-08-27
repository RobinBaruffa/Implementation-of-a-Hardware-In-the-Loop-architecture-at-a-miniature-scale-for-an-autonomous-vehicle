using System.Collections.Generic;
using System.Collections;
using System;
using SimpleJSON;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text;
using System.Threading;
using System.Diagnostics;

public class Listener : MonoBehaviour
{
    /// <summary>Destination ip address.</summary>
    private static string serverIp = "127.0.0.1";

    /// <summary>Destination port</summary>
    private static int port = 55566;

    /// <summary>The network stream to send data.</summary>
    private static NetworkStream networkStream;

    /// <summary>Socket to receive data after accepting the connection.</summary>
    private static Socket handler;

   /// <summary>The event when the data is received.</summary>
   public static event ReceivedData OnReceived;

   /// <summary>Data received by the event.</summary>
   public delegate void ReceivedData(List<SensorExport> sensors);

   /// <summary>Extracted JSON into a sensorList.</summary>
   private List<SensorExport> sensors;

    public void Awake()
    {
        Connect();
    }

    public void FixedUpdate() {
        // Stopwatch stopWatch = new Stopwatch();
        // stopWatch.Start();
        // while (stopWatch.Elapsed.Seconds < 40)
        while (handler.Poll(0, SelectMode.SelectRead))
        {
            /*
             int nbBytes = handler.Receive(bytes);
             data += Encoding.ASCII.GetString(bytes, 0, nbBytes);
             */
            networkStream = new NetworkStream(handler);
            JSONNode N = JSONNode.LoadFromBinaryStream(networkStream);
            sensors = JsonConvert.DeserializeObject<List<SensorExport>>(N["sensor"].Value, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            OnReceived(sensors);
            // print(((LidarData)sensors[0].data).data.Count);
            /*
            string json = JsonConvert.SerializeObject(sensors, Formatting.None, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            JSONNode n = JSON.Parse("{}");
            n["version"] = "0.0";
            n["timestamp"] = DateTime.Now.Ticks / 1e7;
            n["sensor"] = json;
            n.SaveToBinaryStream(new NetworkStream(soc2));
            */
        }
        // delayedClose(handler, SocketShutdown.Both);
    }

    private void Connect()
    {
        // Tcp connection initialisation
        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(serverIp), port);
        // IPEndPoint ipe2 = new IPEndPoint(IPAddress.Parse(serverIp), 55568);
        Socket soc = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // soc2 = new Socket(ipe2.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // soc2.Connect(ipe2);
        soc.Bind(ipe);
        soc.Listen(1);
        print("Waiting for a connection...");
        handler = soc.Accept();
        print("Accepted connection...");
        gameObject.GetComponent<CarRemoteControl>().enabled = true;
    }

    private void delayedClose(Socket socket, SocketShutdown type)
    {
        socket.Shutdown(type);
        socket.Close();
        print("Closed");
    }
}
