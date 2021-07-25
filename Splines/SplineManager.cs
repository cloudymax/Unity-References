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

    // change the size of the control point gizmos
    [Range(.5f,5f)]
    public float ControlPointSize;

    // control the width of the line in the editor representing the spline
    [Range(.5f, 5f)]
    public float SplineLineWidth;

    // change the color of the control points
    public Color ControlPointColor;

    // hange the color of the points p0-3
    public Color PointColor;

    // size of the tool-tip text in the editor
    public int TextSize;

    // distance above the gizmo at which its tooltip displays
    [Range(.5f, 5f)]
    public float LabelHeight;

    // the length of the lines shown to represent the direction of a point on the slpine
    [Range(0, 10)]
    public float DirectionDistance;

    // display the dirction of a series of points on the spline for debugging
    public bool ShowLineSteps;

    // the length of the line-steps rendered in the editor
    [Range(0, 10)]
    public float LineStepDIstance;

    // the range to allow oscillation from center out horizontally.
    [Range(0, 10)]
    public float OscillationRangeH;

    // the range to allow oscillation from center up vertically.
    [Range(0, 10)]
    public float OscillationRangeV;

    // the radius length of the orbit around the spline
    [Range(0, 10)]
    public float OrbitRange;

    // speed at which the point orbits the spline
    [Range(0, 10)]
    public int OrbitSpeed;
    //use odd numbers only

    // speed at which the point horizontally oscillates as it traverses the spline
    [Range(0, 10)]
    public int OscillationSpeedH;
    //use odd numbers only

    // speed at which the point vertically oscillates as it traverses the spline
    [Range(0, 10)]
    public int OscillationSpeedV;
    //use odd numbers only

    // Horizontal Randomization weight
    [Range(0, 1)]
    public float RandomH;

    // Vertical Randomization Weight
    [Range(0, 1)]
    public float RandomV;

    // intensity of randomized spline bend
    [Range(0, 1)]
    public float RandomD;



    //spline floats

    // point along the spline
    [Range(0, 1)]
    public float T;

    // scale of the splne relative to it creation size.
    // allows us to make animations and spells behave consistantly over
    //long or changing distances
    public float SplineScale;

    // length of the spline at creation
    public float OriginalLength;

    // current length of the spline
    public float CurrentLength;

    // dont scale the spline
    public bool LockScale;

    // use an animation curve to control the horizontal oscillation behavior
    public AnimationCurve OscCurveH;

    // use an animation curve to control the vertical oscillation behavior
    public AnimationCurve OscCurveV;

    // use an animation curve to control the points orbit radius
    public AnimationCurve OrbitDistanceCurve;

    // use an animation curve to control the orbit speed
    public AnimationCurve OrbitSpeedCurve;

    public Vector3 Point;

}
