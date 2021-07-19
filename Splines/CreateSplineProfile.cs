using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Spline", menuName = "New Spline", order = 1)]
[System.Serializable]
public class CreateSplineProfile : ScriptableObject {

    public Vector3 Source;
    public Vector3 Target;

    public Vector3[] Points = new Vector3[4];

    public float SplineLength;
    public float OriginalLength;
    public float SplineScale;
    public Vector3 LenghtVector;

    public float ControlPointLength1;
    public Vector3 ControlPointVector1;

    public float ControlPointLength2;
    public Vector3 ControlPointVector2;

    public bool OscillateH;
    public bool OscillateV;
    public float OscillationRangeH;
    public float OscillationRangeV;
    public int OscillationSpeedH;
    public int OscillationSpeedV;

    public bool Orbit;
    public float OrbitDistance;
    public int OrbitSpeed;

    public bool FollowSpline;

    public float RandomV;
    public float RandomH;
    public float RandomD;
    public bool Reflection;
    public bool Randomize;

    public GameObject Caster;
    public GameObject Victim;

    public AnimationCurve OscCurveH;
    public AnimationCurve OscCurveV;

    public AnimationCurve OrbitDistanceCurve;
    public AnimationCurve OrbitSpeedCurve;

    public float T;


}
