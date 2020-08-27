using System.Net.Sockets;
using System.Net;
using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Provide an interface to communicate the vehicule control through the network.
///
/// If isListener is enabled, it receives the controls and apply it to the vehicule.
/// Otherwise it sends the informations contained in lastControl.
///
/// Communicate using sockets (Protocol : Udp)
/// </summary>
public class CarRemoteControl : MonoBehaviour
{
    /// <summary>The vehicule control class</summary>
    public class Control
    {
        /// <summary>The horizontal input.</summary>
        public float h;
        /// <summary>The vertical input.</summary>
        public float v;
        /// <summary>The handbrake input.</summary>
        public float hb;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="h">The horizontal input.</param>
        /// <param name="v">The vertical input.</param>
        /// <param name="hb">The handbrake input.</param>
        public Control(float h, float v, float hb)
        {
            setAttributes(h, v, hb);
        }

        /// <summary>
        /// Sets the attributes 
        /// </summary>
        /// <param name="h">The horizontal input.</param>
        /// <param name="v">The vertical input.</param>
        /// <param name="hb">The handbrake input.</param>
        public void setAttributes(float h, float v, float hb)
        {
            this.h = h;
            this.v = v;
            this.hb = hb;
        }

        /// <summary>Convert the class into a string.</summary>
        /// <return>The converted string.</return>
        public override string ToString()
        {
            return "Horizontal : " + h + ", Vertical : " + v + ", HandBrake : " + hb;
        }
    }

    /// <summary>Data received by the isListener object in the socket information exchange.</summary>
    public class State
    {
        /// <summary>The data</summary>
        public byte[] buffer = new byte[1000];
    }


    /// <summary>Source ip address.</summary>
    private static string serverIp = "0.0.0.0";

    /// <summary>Source port</summary>
    private static int port = 55567;

    /// <summary>If isListener, this is the car controller.</summary>
    private CarController m_Car;

    /// <summary>isListener as true if the class has to receive the car control. False if it has to send the controls.</summary>
    public bool isListener;

    /// <summary>The last received control or the next control to send.</summary>
    private Control lastControl;

    /// <summary>Socket used for the communication.</summary>
    private Socket soc;

    public void Start()
    {
        lastControl = new Control(0, 0, 0);
        if (isListener)
        {
            m_Car = gameObject.GetComponent<CarController>();
        }
    }

    public void OnDisable()
    {
        soc.Shutdown(SocketShutdown.Both);
        soc.Close();
    }

    private void connect()
    {
        IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(serverIp), port);
        soc = new Socket(ipe.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
        if (isListener)
        {
            soc.Bind(ipe);
        }
        else
        {
            soc.Connect(ipe);
        }
    }

    /// <summary>The first loop in FixedUpdate (used to initialize the connections).</summary>
    private bool once = true;

    /// <summary>Handle the message reception</summary>
    private AsyncCallback recv = null;

    /// <summary>The data received.</summary>
    private State state = new State();

    /// <summary>The data to read in the socket. Message send must be smaller than the array size.</summary>
    private byte[] bytes = new byte[1000];

    /// <summary>Loop every physical engine update</summary>
    public void FixedUpdate()
    {
        if (once)
        {
            once = false;
            connect();

            // lauch data transfer
            SocketError error;
            soc.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, out error, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int btes = soc.EndReceive(ar);
                soc.BeginReceive(so.buffer, 0, 1000, SocketFlags.None, recv, so);
                    // Debug.Log(Encoding.Default.GetString(so.buffer));
                    JsonSerializerSettings jss = new JsonSerializerSettings();
                jss.CheckAdditionalContent = false; // Really important, avoid crash caused by interpretation of overflowing characters.
                    lastControl = JsonConvert.DeserializeObject<Control>(Encoding.Default.GetString(so.buffer), jss);
                if (lastControl == null)
                {
                    lastControl = new Control(0, 0, 0);
                }
            }, state
            );
        }
        if (isListener)
        {
            // soc.Receive(bytes);
            // Debug.Log("byte " + Encoding.Default.GetString(bytes));
            // JsonSerializerSettings jss = new JsonSerializerSettings();
            // jss.CheckAdditionalContent = false; // Really important, avoid crash caused by interpretation of overflowing characters.
            // lastControl = JsonConvert.DeserializeObject<Control>(Encoding.Default.GetString(bytes), jss);
            


	    Debug.Log("flood " + lastControl.ToString());
	    
	    //m_car.Move(float steering, float accel, float footbrake, float handbrake)
            m_Car.Move(lastControl.h, lastControl.v, lastControl.v, lastControl.hb);
        }
        else
        {
            /*
             lastControl.setAttributes(Mathf.Cos(Time.time % Mathf.PI), Mathf.Sin(Time.time % Mathf.PI), 0);
             byte[] data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(lastControl, Formatting.None));
             byte[] dataNL = new byte[data.Length + Encoding.Default.GetBytes(Environment.NewLine).Length + 1];
             data.CopyTo(dataNL, 0);
             Encoding.Default.GetBytes(Environment.NewLine).CopyTo(dataNL, data.Length);
             soc.Send(dataNL);
             */
        }
    }

    public void send(float h, float v, float hb)
    {
        lastControl.setAttributes(h, v, hb);
        /*
        byte[] data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(lastControl, Formatting.None));
        byte[] dataNL = new byte[data.Length + Encoding.Default.GetBytes(Environment.NewLine).Length + 1];
        data.CopyTo(dataNL, 0);
        Encoding.Default.GetBytes(Environment.NewLine).CopyTo(dataNL, data.Length);
        soc.Send(dataNL);
        */
        soc.Send(Encoding.Default.GetBytes(JsonConvert.SerializeObject(lastControl, Formatting.None)));
    }
}
