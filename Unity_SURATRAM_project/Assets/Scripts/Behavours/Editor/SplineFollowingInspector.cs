using UnityEditor;
using UnityEngine;

/// <summary>
/// Class that format the SplineWalker settings to a more user-friendly one.
/// </summary>
[CustomEditor(typeof(SplineFollowing))]
public class SplineFollowingInspector : Editor
{

    /// <value>The SplineWalker that will be modified with the GUI.</value>
    private SplineFollowing splineFollowing;

    /// <summary>
    /// Updates the GUI
    /// </summary>
    public override void OnInspectorGUI()
    {
        splineFollowing = (SplineFollowing)target;

        GUILayout.Label("Global links to environment", EditorStyles.boldLabel);

        splineFollowing.spline = (BezierSpline)EditorGUILayout.ObjectField("Spline", splineFollowing.spline, typeof(BezierSpline), true);

        GUILayout.Space(10f);

        GUILayout.Label("Behaviour", EditorStyles.boldLabel);

        splineFollowing.isFollowing = EditorGUILayout.Toggle("Is following", splineFollowing.isFollowing);

        if (splineFollowing.isFollowing)
        {
            splineFollowing.followsGameObject = (GameObject)EditorGUILayout.ObjectField("follows GameObject", splineFollowing.followsGameObject, typeof(GameObject), true);
            splineFollowing.distanceFollowing = EditorGUILayout.FloatField("Distance between the objects", splineFollowing.distanceFollowing);
        }
        else
        {
            splineFollowing.isDriving = EditorGUILayout.Toggle("Smooth driving", splineFollowing.isDriving);
            if (!splineFollowing.isDriving)
            {
                splineFollowing.velocity = EditorGUILayout.FloatField("Velocity of the GameObject", splineFollowing.velocity);
                splineFollowing.curveSpeed = EditorGUILayout.Toggle("Adaptative speed in Spline", splineFollowing.curveSpeed);
            }
        }
    }
}
