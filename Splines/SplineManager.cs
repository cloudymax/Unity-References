using UnityEngine;
using System.Collections;

// structure/schema for the <Spline Profile> profile type which will consume and create scriptable objects
public class SplineManager : MonoBehaviour {

    //public drag n drops
    public CreateSplineProfile Spline;

    //options
    // show editor handles
    public bool EditorHandles;
    // prevent spline scalng
    public bool LockOrigins;
    // display control points in the editor
    public bool ShowControlPoints;
    // show directional indicators for the traversal agent
    public bool ShowDirection;
    // enable horizontal oscillation
    public bool OscillateHorizontal;
    // enable vertical oscillation
    public bool OscillateVertical;
    // enable orbital animations
    public bool Orbit;
    // force strict path following
    public bool FollowSpline;
    // when enabled, randomization will only generate symmetrical patterns
    public bool RandomizerReflection;
    // get a random spline based on configuration on each request
    public bool AutoRandom;
    // show more data
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
