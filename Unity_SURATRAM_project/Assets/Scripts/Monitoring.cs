using UnityEngine;

/// <summary>
/// This class monitors, by retrieving differents informations in the scene.
/// </summary>
public static class Monitoring
{
    /// <summary>The max number of data informations that can be stored.</summary>
    private const int nbDataMax = 10000;
    /// <summary>The time for every value update</summary>
    public static float samplingPeriod = 1f;

    /// <summary>Variable to store the elapsed time since the last data merge.</summary>
    private static float timeStartSampling;

    /// <summary>The current number of data sample that has been gathered but not merged.</summary>
    private static int indexSampling;

    /// <summary>The velocity of the bus obtained after the data has been filtered.</summary>
    public static float velocityBus { get; private set; }
    /// <summary>The velocity of the car obtained after the data has been filtered.</summary>
    public static float velocityCar { get; private set; }

    /// <summary>The acceleration of the bus obtained after the data has been filtered.</summary>
    public static float accelerationBus { get; private set; }
    /// <summary>The acceleration of the car obtained after the data has been filtered.</summary>
    public static float accelerationCar { get; private set; }

    /// <summary>Array of a 2-tuple containing both position and time stamp of this data. Circular override of values.</summary>
    public static (Vector3, float)[] posBusArr { get; private set; }
    /// <summary>Current index in 'posBusArr'.</summary>
    public static int indexPosBusArr { get; private set; }

    /// <summary>The inter-distance obtained after the data has been filtered.</summary>
    public static float interDistance { get; private set; }

    /// <summary>The raw data of bus positon. Filled over time.</summary>
    private static Vector3[] tmpPosBusArr;
    /// <summary>The raw data of car positon. Filled over time.</summary>
    private static Vector3[] tmpPosCarArr;

    /// <summary>The raw data of bus velocity. Filled over time.</summary>
    private static float[] velocityBusArr;
    /// <summary>The raw data of car velocity. Filled over time.</summary>
    private static float[] velocityCarArr;

    /// <summary>The raw data of bus acceleration. Filled over time.</summary>
    private static float[] accelerationBusArr;
    /// <summary>The raw data of car acceleration. Filled over time.</summary>
    private static float[] accelerationCarArr;

    /// <summary>The raw data of the inter-distance. Filled over time.</summary>
    private static float[] interDistanceArr;

    /// <summary>The raw data of the time elapsed between every sample. Filled over time.</summary>
    private static double[] arrayDeltaTime;

    /// <summary>
    /// Initialisation of the class.
    /// </summary>
    static Monitoring()
    {
        velocityBus = 0f;
        velocityCar = 0f;

        accelerationBus = 0f;
        accelerationCar = 0f;

        interDistance = 0f;

        posBusArr = new (Vector3, float)[nbDataMax];

        tmpPosBusArr = new Vector3[nbDataMax];
        tmpPosCarArr = new Vector3[nbDataMax];

        velocityBusArr = new float[nbDataMax];
        velocityCarArr = new float[nbDataMax];

        accelerationBusArr = new float[nbDataMax];
        accelerationCarArr = new float[nbDataMax];

        arrayDeltaTime = new double[nbDataMax];

        interDistanceArr = new float[nbDataMax];

        tmpPosBusArr[0] = GameObject.Find("Bus").transform.position;
        tmpPosCarArr[0] = GameObject.Find("Car").transform.position;

        posBusArr[0] = (GameObject.Find("Bus").transform.position, Time.time);

        interDistanceArr[0] = (tmpPosBusArr[0] - tmpPosCarArr[0]).magnitude;

        indexSampling = 1;
        indexPosBusArr = 1;

        timeStartSampling = 0;
    }

    // Update is called once per frame
    /// <summary>
    /// Function that updates every frame the data. And every 'samplingPeriod' it publishs the filtered data in public variables.
    /// </summary>
    public static void UpdateOnFrame(float delta)
    {
        if (indexSampling == nbDataMax)
        {
            Debug.Log("FATAL ERROR : Array out of range. Resulting in inconsistent monitoring.");
            timeStartSampling = samplingPeriod + 1f;
        }

        timeStartSampling += delta;
        if (timeStartSampling <= samplingPeriod)
        {
            tmpPosBusArr[indexSampling] = GameObject.Find("Bus").transform.position;
            tmpPosCarArr[indexSampling] = GameObject.Find("Car").transform.position;

            posBusArr[indexPosBusArr] = (GameObject.Find("Bus").transform.position, Time.time);

            arrayDeltaTime[indexSampling] = delta;

            interDistanceArr[indexSampling] = (tmpPosBusArr[indexSampling] - tmpPosCarArr[indexSampling]).magnitude;

            velocityBusArr[indexSampling] = (tmpPosBusArr[indexSampling - 1] - tmpPosBusArr[indexSampling]).magnitude / delta;

            velocityCarArr[indexSampling] = (tmpPosCarArr[indexSampling - 1] - tmpPosCarArr[indexSampling]).magnitude / delta;

            accelerationBusArr[indexSampling] = (velocityBusArr[indexSampling] - velocityBusArr[indexSampling - 1]) / delta;
            accelerationCarArr[indexSampling] = (velocityCarArr[indexSampling] - velocityCarArr[indexSampling - 1]) / delta;

            indexSampling++;
        }
        else
        {
            velocityBus = average(velocityBusArr, indexSampling);
            velocityCar = average(velocityCarArr, indexSampling);
            accelerationBus = average(accelerationBusArr, indexSampling);
            accelerationCar = average(accelerationCarArr, indexSampling);
            interDistance = average(interDistanceArr, indexSampling);

            tmpPosBusArr[0] = GameObject.Find("Bus").transform.position;
            tmpPosCarArr[0] = GameObject.Find("Car").transform.position;

            interDistanceArr[0] = (tmpPosBusArr[0] - tmpPosCarArr[0]).magnitude;

            velocityBusArr[0] = velocityBusArr[indexSampling - 1];
            velocityCarArr[0] = velocityCarArr[indexSampling - 1];

            accelerationBusArr[0] = accelerationBusArr[indexSampling - 1];
            accelerationCarArr[0] = accelerationCarArr[indexSampling - 1];

            indexSampling = 1;
            timeStartSampling = 0;
        }

        posBusArr[indexPosBusArr] = (GameObject.Find("Bus").transform.position, Time.time);
        if (++indexPosBusArr == nbDataMax)
        {
            indexPosBusArr = 0; // End of array, cycle
        }
    }

    /// <summary>
    /// Function that computes the average of the <c>indexSampling</c>-th elements of a float array.
    /// </summary>
    /// <param name="arr">The float array source</param>
    /// <returns>The average value</returns>
    private static float average(float[] arr, int indexSampling)
    {
        float f = 0f;
        for (int i = 0; i < indexSampling; i++)
        {
            f += arr[i];
        }
        return f / indexSampling;
    }

    /// <summary>
    /// Function that returns the current speed of the car.
    /// </summary>
    /// <returns>The velocity of the car.</returns>
    public static float getCurrentCarVelocity() {
       return indexSampling > 0 ? velocityCarArr[indexSampling - 1] : 0f;
    }

    /// <summary>
    /// Function that computes the average of the <c>indexSampling</c>-th elements of a <c>Vector3</c> array.
    /// </summary>
    /// <param name="arr">The Vector3 array source</param>
    /// <returns>The average value, stored in a <c>Vector3</c></returns>
    private static Vector3 average(Vector3[] arr, int indexSampling)
    {
        float x = 0, y = 0, z = 0;
        Vector3 v;
        for (int i = 0; i < indexSampling; i++)
        {
            v = arr[i];
            x += v.x;
            y += v.y;
            z += v.z;
        }
        return new Vector3(x / indexSampling, y / indexSampling, z / indexSampling);
    }

    /// <summary>
    /// Function that shifts every element of a array to the right.
    /// </summary>
    /// <param name="arr">Source array of elements, type of template <c>T</c></param>
    /// <remarks>Initialize the <c>0-th</c> element with the default <c>T</c> value.</remarks>
    private static void arrayShiftRight<T>(T[] arr)
    {
        if (nbDataMax >= 2)
        {
            for (int i = nbDataMax; i > 1; i--)
            {
                arr[i - 1] = arr[i - 2];
            }
        }
        arr[0] = default(T);
    }
}
