using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class displaying the informations from Monitoring to the Unity View by a Text object.
/// </summary>
public class Informations : MonoBehaviour
{
    /// <summary>The time between each sceen informations update.</summary>
    public float periodUpdate = 1f;
    /// <summary>The current time elapesed since the last screen information update/refresh.</summary>
    private float timeElapsed;

    /// <summary>
    /// Initialisation
    /// </summary>
    void Start()
    {
        timeElapsed = 0;
        display(0, 0, 0, 0, 0);
        JSONCreator.connect();
    }

    private int count = 0;

    void Update()
    {
        Shortcuts.Update();
    }

    /// <summary>
    /// Function that is called every frame and updates the screen every 'periodUpdate'.
    /// </summary>
    void FixedUpdate()
    {
        Monitoring.UpdateOnFrame(Time.fixedDeltaTime);
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed > periodUpdate)
        {
            JSONCreator.sendROSData();
            display(Monitoring.velocityBus, Monitoring.accelerationBus, Monitoring.velocityCar, Monitoring.accelerationCar, Monitoring.interDistance);
            timeElapsed = 0;
        }
    }

    /// <summary>
    /// Print to the screen
    /// </summary>
    /// <param name="vb">The velocity of the bus.</param>
    /// <param name="ab">The acceleration of the bus.</param>
    /// <param name="vc">The velocity of the car.</param>
    /// <param name="ac">The acceleration of the car.</param>
    /// <param name="interDist">The distance between the two vehicles.</param>
    private void display(float vb, float ab, float vc, float ac, float interDist)
    {
        gameObject.GetComponent<Text>().text =
           "Vitesse Bus : " + vb.ToString("0.00") + " m\n" +
           "Acceler Bus : " + ab.ToString("0.00") + " m\n" +
           "Vitesse Car : " + vc.ToString("0.00") + " m\n" +
           "Acceler Car : " + ac.ToString("0.00") + " m\n" +
           "Inter Dist  : " + interDist.ToString("0.00") + " m";
    }
}
