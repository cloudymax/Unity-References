using UnityEngine;
using System.Collections;

// creates a scriptable object based on the structure/schema for the <Spline Profile> profile type

[CreateAssetMenu(fileName = "New Spline", menuName = "New Spline", order = 1)]
[System.Serializable]
public class CreateSplineProfile : ScriptableObject {

    // start and end of the spline
    public Vector3 Source;
    public Vector3 Target;

    // array holding the source/end + control points
    public Vector3[] Points = new Vector3[4];

    // current distance from p0 to p3
    public float SplineLength;

    // length from p0 to p3 upon creation
    public float OriginalLength;

    // splines length as a % of the original length
    public float SplineScale;
    public Vector3 LenghtVector;

    public float ControlPointLength1;
    public Vector3 ControlPointVector1;

    public float ControlPointLength2;
    public Vector3 ControlPointVector2;

    // oscillaion values
    public bool OscillateH;
    public bool OscillateV;
    public float OscillationRangeH;
    public float OscillationRangeV;
    public int OscillationSpeedH;
    public int OscillationSpeedV;

    // orbit values
    public bool Orbit;
    public float OrbitDistance;
    public int OrbitSpeed;

    // enforce tighter adherance to the spline path by agents
    public bool FollowSpline;

    // randomization modifiers
    public float RandomV;
    public float RandomH;
    public float RandomD;
    public bool Reflection;
    public bool Randomize;

    public GameObject Caster;
    public GameObject Victim;

    // animation curves for oscillation animation
    public AnimationCurve OscCurveH;
    public AnimationCurve OscCurveV;

    // animation curves used for Orbit animation
    public AnimationCurve OrbitDistanceCurve;
    public AnimationCurve OrbitSpeedCurve;

    // point along spline
    public float T;


}
