using UnityEngine;

/// <summary>
/// Class that follows another gameObject by its path
/// </summary>
public class PathFollowing : MonoBehaviour
{
    /// <summary>The object to follow</summary>
    public GameObject objToFollow;

    /// <summary>Array of a 2-tuple containing both position and time stamp of this data. Circular override of values.</summary>
    private (Vector3, float)[] posObjArr;
    /// <summary>Current index in 'posObjArr'.</summary>
    private int indexPosArr;

    /// <summary>Length of the posObjArr. It is the maximum number of concurrent records of position for the object.</summary>
    public const int nbDataMax = 10000;

    /// <summary>The interval of interpoled points between two datas. The higher the number is, the more accurate it is.</summary>
    public int sampling = 10;

    /// <summary>The orientation of the vehicle is defined by the distance on path from the GameObject it follows.</summary>
    public float lookAtDistance = 10f;

    /// <summary>The wanted distance between the two vehicles.</summary>
    public float interDistance = 20f;

    /// <summary>The wanted distance between the two vehicles.</summary>
    public float initializedSegmentLength = 0.1f;

    /// <summary>Variable that has a value of false if the path has not been initialized, true otherwise.</summary>
    private bool isInitialized = false;

    /// <summary>Use a fixed interdistance between the two objects.</summary>
    public bool isUsingInterdistance = true;

    public void FixedUpdate()
    {
        if (!isInitialized)
        {
            posObjArr = new (Vector3, float)[nbDataMax];
            initializeSubpath();
            isInitialized = true;
        }
        Transform goTransformSav = gameObject.transform;
        updateObjectPosition();
        gameObject.transform.position = positionOnPath();
    }

    /// <summary>
    /// Retrieve and store the position of the object to follow inside the 'posObjArr'.
    /// </summary>
    private void updateObjectPosition()
    {
        posObjArr[indexPosArr] = (objToFollow.transform.position, Time.time);
        if (++indexPosArr == nbDataMax)
        {
            indexPosArr = 0; // End of array, cycle
        }
    }

    /// <summary>
    /// Move the vehicle to an interDistance in the path of the gameObject it follows.
    /// </summary>
    private Vector3 positionOnPath()
    {
        float currDistance = 0f;
        Vector3 currPosInPath = new Vector3(0, 0, 0);
        int firstIndex = 0, secondIndex = 0;
        bool isOriented = false;

        //Debug.Log(firstIndex + " : " + secondIndex);
        // Follows on the path
        if (isUsingInterdistance)
        {
            while (currDistance < interDistance)
            {
                // Follows a subpath from a data to its neighboor. It is an intrapoled path (it is a segment).
                (firstIndex, secondIndex) = indexGetSubPathIndexes(firstIndex, secondIndex);
                if (firstIndex == secondIndex)
                {
                    // Error
                    return new Vector3(0, 0, 0);
                }

                Vector3 beginPos = posObjArr[firstIndex].Item1, endPos = posObjArr[secondIndex].Item1;
                double k = 0;
                currPosInPath = beginPos;
                while (currDistance < interDistance && k <= 1f)
                {
                    currDistance += (currPosInPath - (beginPos + (float)k * (endPos - beginPos))).magnitude;
                    currPosInPath = beginPos + (float)k * (endPos - beginPos);
                    k += 1f / sampling;
                    if (isOriented == false && currDistance > lookAtDistance)
                    {
                        gameObject.transform.LookAt(currPosInPath);
                        isOriented = true;
                    }
                }
            }
        }

        return currPosInPath;
    }

    /// <summary>
    /// Function that returns the indexes of subpath given the 'firstIndex' and the 'secondIndex'
    /// The firstItem is point newer than secondIndex.
    /// </summary>
    /// <param name="firstIndex">The given first index</param>
    /// <param name="secondIndex">The given second index</param>
    /// <returns>Returns a 2-tulpe containing the first and second index.</returns>
    private (int firstIndex, int secondIndex) indexGetSubPathIndexes(int firstIndex, int secondIndex)
    {
        if (firstIndex == 0 && secondIndex == 0)
        { // first time, initialize the indexes.
          // Sets firstIndex
            if (indexPosArr - 1 >= 0)
            {
                firstIndex = indexPosArr - 1;
            }
            else
            {
                if (posObjArr[nbDataMax - 1].Item2 != 0f)
                {
                    // It has already done a cycle
                    firstIndex = nbDataMax - 1;
                }
                else
                {
                    Debug.Log("FATAL ERROR : Not enough data to extract a path");
                    return (0, 0);
                }
            }
        }
        else
        {
            // Sets firstIndex
            firstIndex = secondIndex;
        }

        // Sets secondIndex
        if (firstIndex - 1 >= 0)
        {
            secondIndex = firstIndex - 1;
        }
        else
        {
            if (posObjArr[nbDataMax - 1].Item2 != 0f)
            {
                // It has already done a cycle
                secondIndex = nbDataMax - 1;
            }
            else
            {
                Debug.Log("FATAL ERROR : Not enough data to extract a path");
                return (0, 0);
            }
        }

        if (firstIndex == indexPosArr)
        {
            Debug.Log("FATAL ERROR : Path is not long enough to position the vehicle");
            return (0, 0);
        }
        else
        {
            return (firstIndex, secondIndex);
        }
    }

    /// <summary>
    /// Initialize a more segmented initiate path. Each sub-segment has a magnitude of initializedSegmentLength.
    /// </summary>
    /// <returns>Returns true if it can be initialized, false otherwise.</returns>
    private bool initializeSubpath()
    {
        float nbSubsegments = (gameObject.transform.position - objToFollow.transform.position).magnitude / initializedSegmentLength; // n = (B - A) / h. n : number of samples, h the length of each segments

        for (int i = 0; i < nbSubsegments; i++)
        {
            posObjArr[indexPosArr] = (gameObject.transform.position + (float)i / nbSubsegments * (objToFollow.transform.position - gameObject.transform.position), Time.time); // Computes the position of the barycentre.
            if (++indexPosArr >= nbDataMax)
            {
                Debug.Log("Could not initialize the subpath : Too much sub-segments.");
                return false;
            }
        }
        return true;
    }
}
