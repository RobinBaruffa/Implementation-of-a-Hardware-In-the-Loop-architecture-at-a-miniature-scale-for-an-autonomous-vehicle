using UnityEngine;

/// <summary>
/// Interface to adapt the size of the Spline.
/// </summary>
public class easySpline : MonoBehaviour
{
   /// <value>Length in x</value>
   public float x = 1;
   /// <value>Length in z</value>
   public float z = 1;

   /// <value>Ratio of x-coordinate borders. Only the bordersFactorX-th will be used.</value>
   public const float bordersFactorX = 0.9f;
   /// <value>Ratio of y-coordinate borders. Only the bordersFactorZ-th will be used.</value>
   public const float bordersFactorZ = 0.9f;

   /// <value>X ratio used to resize the Spline at the requested size. Do not modify</value>
   private const float lengthSplineX = 5.770f;
   /// <value>Z ratio used to resize the Spline at the requested size. Do not modify</value>
   private const float lengthSplineZ = 2.44f;

   /// <summary>
   /// Resize the Spline at the start of the simulation.
   /// </summary>
   private void Start() {
      /* Resize correctly the spline */
      gameObject.transform.localScale = new Vector3(bordersFactorX * x / lengthSplineX,0,bordersFactorZ * z / lengthSplineZ);
   }
}
