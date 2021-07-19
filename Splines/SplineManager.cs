using UnityEngine;
using System.Collections;

public class SplineManager : MonoBehaviour {

    //public drag n drops
    public CreateSplineProfile Spline;

    //options
    public bool EditorHandles;
    public bool LockOrigins;
    public bool ShowControlPoints;
    public bool ShowDirection;
    public bool OscillateHorizontal;
    public bool OscillateVertical;
    public bool Orbit;
    public bool FollowSpline;
    public bool RandomizerReflection;
    public bool AutoRandom;
    public bool Debug;
    [Range(10,50)]
    public int lineSteps;

    public GameObject Source;
    public GameObject Target;

    //option floats

    [Range(.5f,5f)]
    public float ControlPointSize;

    [Range(.5f, 5f)]
    public float SplineLineWidth;

    public Color ControlPointColor;

    public Color PointColor;

    public int TextSize;

    [Range(.5f, 5f)]
    public float LabelHeight;

    [Range(0, 10)]
    public float DirectionDistance;

    public bool ShowLineSteps;

    [Range(0, 10)]
    public float LineStepDIstance;

    [Range(0, 10)]
    public float OscillationRangeH;

    [Range(0, 10)]
    public float OscillationRangeV;

    [Range(0, 10)]
    public float OrbitRange;

    [Range(0, 10)]
    public int OrbitSpeed;
    //use odd numbers only

    [Range(0, 10)]
    public int OscillationSpeedH;
    //use odd numbers only

    [Range(0, 10)]
    public int OscillationSpeedV;
    //use odd numbers only


    [Range(0, 1)]
    public float RandomH;

    [Range(0, 1)]
    public float RandomV;

    [Range(0, 1)]
    public float RandomD;



    //spline floats
    [Range(0, 1)]
    public float T;

    public float SplineScale;
    public float OriginalLength;
    public float CurrentLength;
    public bool LockScale;

    public AnimationCurve OscCurveH;
    public AnimationCurve OscCurveV;
    public AnimationCurve OrbitDistanceCurve;
    public AnimationCurve OrbitSpeedCurve;

    public Vector3 Point;

}
