using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Class that handles the movements a Vehicle on a Spline.
/// </summary>
/// <seealso>https://catlikecoding.com/unity/tutorials/curves-and-splines/</seealso>
public class SplineFollowing : MonoBehaviour
{

    [Header("Global links to environment")]
    /// <value>The Spline to follow</value>
    public BezierSpline spline;

    /// <value>The realistic controller.</value>
    private CarController m_Car; // the car controller we want to use

    [Header("Behaviour")]
    /// <value>Is the GameObject following another one.</value>
    public bool isFollowing = false;

    /// <value>Is the GameObject using a realistic method to move.</value>
    public bool isDriving;

    /// <value>Speed of the GameObject when controls are not handled by the driving car controller.</value>
    public float velocity;

    /// <value>The progress of the GameObject over the Spline. Range from 0 to 1 in float.</value>
    public float progress { get; private set; }

    /// <value>Discretisation of intervals.</value>
    private float genericSampling = 100;

    /// <value>Length of spline</value>
    private float splineLength;

    /// <value>If isFollowing is enabled, this is the GameObject to follow.</value>
    public GameObject followsGameObject;

    /// <value>The wanted interdistance between the two GameObject. Only needed for the follower.</value>
    public float distanceFollowing = 15f;

    /// <summary>Define the maximum speed of the vehicle depending of the curve.</summary>
    public bool curveSpeed = false;

    /// <summary>
    /// Initialization of the class
    /// </summary>
    private void Start()
    {
        if (gameObject.GetComponent<CarController>())
        {
            m_Car = gameObject.GetComponent<CarController>();
        }
        if (isFollowing)
        {
            gameObject.transform.position = spline.GetPoint(1);
        }
        else
        {
            gameObject.transform.position = spline.GetPoint(0);
        }
        splineLength = spline.getSplineLength();
    }

    /// <summary>
    /// Updates the path every frame.
    /// </summary>
    private void FixedUpdate()
    {
#if !MOBILE_INPUT
        float handbrake = CrossPlatformInputManager.GetAxis("Jump");
#endif

        float currDist = 0;
        if (isFollowing)
        { // Case following vehicle
            progress = findNearestPointInSpline();
            while (currDist < distanceFollowing)
            {
                if (progress - Time.fixedDeltaTime / genericSampling < 0f)
                {
                    currDist += (spline.GetPoint(progress) - spline.GetPoint(1)).magnitude;
                    progress = 1f;
                }
                else
                {
                    progress -= Time.fixedDeltaTime / genericSampling;
                    currDist += (spline.GetPoint(progress) - spline.GetPoint(progress + Time.fixedDeltaTime / genericSampling)).magnitude;
                }
            }
            //gameObject.transform.position = new Vector3 (spline.GetPoint(progress).x, 0f, spline.GetPoint(progress).z);
            gameObject.transform.position = spline.GetPoint(progress);
            lookForward(10f);
        }
        else
        {
            if (isDriving)
            { // Case leading vehicle with smooth driving
                while (progress < 1f && (gameObject.transform.position - spline.GetPoint(progress)).magnitude < 15f)
                {
                    progress += Time.fixedDeltaTime / genericSampling;
                }
                if (progress > 1f)
                {
                    progress = 0;
                }
                transform.LookAt(spline.GetPoint(progress) + spline.GetDirection(progress));
            }
            else
            { // Case leading vehicle without smooth driving
                if (!curveSpeed)
                {
                    moveOnSpline(velocity);
                }
                else
                {
                    Debug.Log(spline.getDeltaSlope(progress, progress + 0.05f));
                    float maxSpeed = 30 * (Mathf.Pow(45f, 1.2f) - Mathf.Pow(spline.getDeltaSlope(progress, progress + 0.05f), 1.2f)) / Mathf.Pow(45f, 1.2f); // Hardcoded formula to major the speed of the vehicle, depending the curve

                    // newSpeed = wantedAcceleration * DeltaTime + currentSpeed
                    float wantedSpeed = getAcceleration(Monitoring.getCurrentCarVelocity(), CrossPlatformInputManager.GetAxis("Vertical"))
                       * Time.fixedDeltaTime + Monitoring.getCurrentCarVelocity();
                    moveOnSpline(Mathf.Min(maxSpeed, wantedSpeed));
                }
                gameObject.transform.position = spline.GetPoint(progress);
                lookForward(10f);
            }
        }

        // Movement of vehicles. If driving is not enabled, this has no effect on the position but still let the particles, sound, ...
#if !MOBILE_INPUT
        if (isFollowing)
        {
            if ((followsGameObject.transform.position - gameObject.transform.position).magnitude < distanceFollowing)
            {
                m_Car.Move(0f, -1f, -1f, handbrake);
            }
            else
            {
                m_Car.Move(0f, 1f, 1f, handbrake);
            }
        }
        else
        {
            m_Car.Move(0f, 1f, 1f, handbrake);
        }
#else
      if (isFollowing) {
         if ((followsGameObject.transform.position - gameObject.transform.position).magnitude < distanceFollowing) {
            m_Car.Move(0f, -1f, -1f, 0f);
         } else {
            m_Car.Move(0f, 1f, 1f, 0f);
         }
      } else {
         m_Car.Move(0f, 1f, 1f, 0f);
      }
#endif
    }

    /// <summary>
    /// Find the nearest progress point from the leader GameObject in the spline.
    /// </summary>
    /// <returns>The value of progress.</returns>
    private float findNearestPointInSpline()
    {
        float progressTmp = 0f;
        float minInterDistance = float.PositiveInfinity;
        float minProgress = 0f;
        while (progressTmp < 1f)
        {
            if ((spline.GetPoint(progressTmp) - followsGameObject.transform.position).magnitude < minInterDistance)
            {
                minInterDistance = (spline.GetPoint(progressTmp) - followsGameObject.transform.position).magnitude;
                minProgress = progressTmp;
            }
            progressTmp += Time.fixedDeltaTime / genericSampling;
        }
        return minProgress;
    }

    /// <summary>
    /// Force the GameObject to look forward the Spline.
    /// </summary>
    /// <param name="lookingDistance">The distance the GameObject will look next on the spline. Can't be higher than the two extremities distance.</param>
    /// <returns>The value of progress.</returns>
    private void lookForward(float lookingDistance)
    {
        float progressTmp = progress;
        while ((gameObject.transform.position - spline.GetPoint(progressTmp)).magnitude < lookingDistance)
        {
            if (progressTmp > 1f)
            {
                progressTmp = 0f;
            }
            progressTmp += Time.fixedDeltaTime / genericSampling;
        }
        transform.LookAt(spline.GetPoint(progressTmp) + spline.GetDirection(progressTmp));
    }

    /// <summary>
    /// Function that sets the progress one frame ahead with a given 'speed'
    /// </summary>
    /// <param name="speed">The speed</param>
    private void moveOnSpline(float speed)
    {
        float currDist = 0;
        float sampling = 100f * splineLength / (Time.fixedDeltaTime);
        while (currDist < speed * Time.fixedDeltaTime)
        {
            float progressSav = progress;
            if (progress + 1f / sampling > 1f)
            {
                currDist += (spline.GetPoint(progress) - spline.GetPoint(0)).magnitude;
                progress = 0;
            }
            else
            {
                progress += 1f / sampling;
                currDist += (spline.GetPoint(progress) - spline.GetPoint(progress - 1f / sampling)).magnitude;
            }
            if (progressSav == progress)
            {
                sampling /= 10; // Reduce the sampling if it's too high
                Debug.Log("ERRRRRRRROR");
            }
        }
    }

    /// <summary>
    /// Function that contains the longitudunal model of the car. It returns the acceleration of the car for a given speed and gas/brake pressure.
    /// </summary>
    /// <param name="speed">The given speed of the vehicle.</param>
    /// <param name="gas">The given pressure on the gas/braking. Number between -1 (maximal braking) to 1 (maximal gas).</param>
    /// <returns>Returns the wanted acceleration of the car</returns>
    private static float getAcceleration(float speed, float gas)
    {
        if (gas > 0)
        {
            return 4f; // Acceleration
        }
        else if (gas != 0)
        {
            return -8f; // Deceleration
        }
        else
        {
            return -1f; // Engine braking
        }
    }
}
