using UnityEngine;
using System.Collections;

public class SplineMaker2 : MonoBehaviour {

    // todo: change the functions to just take the point arrays instead of the spline profile
    // that should make it more portable and functional for API usage.
    //


    //takes a start and end position, returns an array of points that create a spline
    public static Vector3[] Points (GameObject Source, GameObject Target, CreateSplineProfile Profile)
    {
        Vector3[] newPoints = new Vector3[4];
        Vector3 Start = Source.transform.position;
        Vector3 End = Target.transform.position;

        newPoints[0] = Start;
        newPoints[1] = Start - Profile.ControlPointLength1 * (Start - (Profile.ControlPointVector1 + Start));
        newPoints[2] = End - Profile.ControlPointLength2 * (End - (Profile.ControlPointVector2 + End));
        newPoints[3] = End;

        return newPoints;
    }

    //  returns a vector3 point at <t> position on a spline
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    // returns the tangent vector of a spline point
    public static Vector3 GetTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }

    //returns the bi-normal vector of a point on a spline
    public static Vector3 GetBiNormal(Vector3 Tangent)
    {
        Vector3 BiNormal = Vector3.Cross(Tangent, Vector3.up);
        return BiNormal;
    }

    // returns the normal vector of a point on a spline
    public static Vector3 GetNormal(Vector3 Tangent, Vector3 BiNormal)
    {
        Vector3 Normal = Vector3.Cross(BiNormal, Tangent);
        return Normal;
    }

    // returns the velocity of a spline point
    public static Vector3 GetVelocity(float t, CreateSplineProfile Spline, Transform Source)
    {
        Vector3[] points = new Vector3[4];
        points[0] = Spline.Points[0];
        points[1] = Spline.Points[1];
        points[2] = Spline.Points[2];
        points[3] = Spline.Points[3];

        // return Source.TransformPoint(SplineMaker2.GetTangent(points[0], points[1], points[2], points[3], t)) -
        //     Source.transform.position;
        return SplineMaker2.GetTangent(points[0], points[1], points[2], points[3], t) - Source.transform.position;
    }

    // returns a point along a spline, wraps GetPoint to use a spline profile
    public static Vector3 GetPoint(float t, CreateSplineProfile Spline, Transform Source)
    {
        Vector3[] points = new Vector3[4];
        points[0] = Spline.Points[0];
        points[1] = Spline.Points[1];
        points[2] = Spline.Points[2];
        points[3] = Spline.Points[3];

        return Source.TransformPoint(SplineMaker2.GetPoint(points[0], points[1], points[2], points[3], t));
    }

    // get the direction of a point on a spline
    public static Vector3 GetDirection(float t, CreateSplineProfile Spline, Transform Source)
    {
        return SplineMaker2.GetVelocity(t, Spline, Source).normalized;
    }

    // return a point that is in orbit around point <t> on a spline
    public static Vector3 GetOrbitPoint(Vector3 Center, Vector3 TangentVector, Vector3 BiNormalVector, CreateSplineProfile Spline, float T)
    {
        float OrbitDistance = Spline.OrbitDistanceCurve.Evaluate(T) * Spline.OrbitDistance;
        float Degrees = Spline.OrbitSpeedCurve.Evaluate(T) * 360 * Spline.OrbitSpeed;
        Vector3 NewPoint = Center + Quaternion.AngleAxis(Degrees, TangentVector) * (BiNormalVector * OrbitDistance);
        return NewPoint;
    }

    // return a ponit that is oscillating between 2 points horizontally at point <t>
    public static Vector3 OscillateH(Vector3 Origin, Vector3 Binormal, float Distance, CreateSplineProfile Spline, float T)
    {
        float OscDist = Spline.OscCurveH.Evaluate(T) * Distance;
        Vector3 Point = (Origin - OscDist * (Origin - (Binormal + Origin)));
        return Point;
    }

    // return a ponit that is oscillating between 2 points vertically at point <t>
    public static Vector3 OscillateV(Vector3 Origin, Vector3 Binormal, float Distance, CreateSplineProfile Spline, float T)
    {
        float OscDist = Spline.OscCurveV.Evaluate(T) * Distance;
        Vector3 Point = (Origin - OscDist * (Origin - (Binormal + Origin)));
        return Point;
    }

    // update the scale of the spline to reflect changes int he position of its members
    public static void UpdateSpline(CreateSplineProfile Spline, GameObject Source, GameObject Target)
    {
        Spline.Caster = Source;
        Spline.Victim = Target;

        Spline.Source = Spline.Caster.transform.position;
        Spline.Target = Spline.Victim.transform.position;

        Spline.SplineLength = Vector3.Distance(Spline.Source, Spline.Target);
        float Scale = Spline.SplineLength / Spline.OriginalLength;

        Spline.Source = Spline.Points[0];
        Spline.Target = Spline.Points[3];
        Spline.Points[0] = Spline.Caster.transform.position;
        Spline.Points[3] = Spline.Victim.transform.position;
        Spline.Points[1] = (Spline.Points[0] - (Spline.ControlPointLength1 * Spline.SplineScale) * (Spline.Points[0] - (Spline.ControlPointVector1 + Spline.Points[0])));
        Spline.Points[2] = (Spline.Points[3] + (Spline.ControlPointLength2 * Spline.SplineScale) * (Spline.Points[3] - (Spline.ControlPointVector2 + Spline.Points[3])));
    }

    // travers a spline from point 0t to 1t towards the target
    public static void TraverseSpline(GameObject Source, GameObject Target, CreateSplineProfile Spline, GameObject ObjectToMove, float T)
    {

        if (Spline.Orbit) // for oribiting the spline
        {
            Vector3 Center = GetPoint(Spline.Points[0], Spline.Points[1], Spline.Points[2], Spline.Points[3], T);
            Vector3 TangentVector = GetDirection(T, Spline, Source.transform);
            Vector3 Binormal = GetBiNormal(TangentVector);
            Vector3 NewPoint = GetOrbitPoint(Center, TangentVector, Binormal, Spline, T);
            ObjectToMove.transform.position = NewPoint;
        }

        if (Spline.OscillateH) // horizontal Oscillation
        {
            Vector3 Center = GetPoint(Spline.Points[0], Spline.Points[1], Spline.Points[2], Spline.Points[3], T);
            Vector3 TangentVector = GetDirection(T, Spline, Source.transform);
            Vector3 Binormal = GetBiNormal(TangentVector);
            Vector3 NewPoint = OscillateH(Center, Binormal, Spline.OscillationRangeH, Spline, T);
            ObjectToMove.transform.position = NewPoint;
        }

        if (Spline.OscillateV) // Vertical Oscillation
        {
            Vector3 Center = GetPoint(Spline.Points[0], Spline.Points[1], Spline.Points[2], Spline.Points[3], T);
            Vector3 TangentVector = GetDirection(T, Spline, Source.transform);
            Vector3 Binormal = GetBiNormal(TangentVector);
            Vector3 Normal = GetNormal(TangentVector, Binormal);
            Vector3 NewPoint = OscillateV(Center, Normal, Spline.OscillationRangeV, Spline, T);
            ObjectToMove.transform.position = NewPoint;
        }

        if (Spline.FollowSpline) // follows the Spline Exactly
        {
            Vector3 NewPoint = GetPoint(Spline.Points[0], Spline.Points[1], Spline.Points[2], Spline.Points[3], T);
            ObjectToMove.transform.position = NewPoint;
        }

        UpdateSpline(Spline, Source, Target);

    }

    // randomly generate control points given a start and end point, creatng a randomized spline
    public static void RandomControlPoints(GameObject Caster, GameObject Target, CreateSplineProfile Spline)
    {

        Vector3 CasterPOS = Caster.transform.position;
        Vector3 TargetPOS = Target.transform.position;

        Spline.OriginalLength = Vector3.Distance(CasterPOS, TargetPOS);
        Spline.SplineLength = Vector3.Distance(CasterPOS, TargetPOS);
        Spline.SplineScale = 1;

        if (Spline.Reflection)
        {
            float RandomH = Random.Range(-Spline.RandomH * Spline.SplineLength, Spline.RandomH * Spline.SplineLength);
            float RandomV = Random.Range(0, Spline.RandomV) * Spline.SplineLength;
            float RandomD = Random.Range(0, Spline.RandomD) * Spline.SplineLength;

            Vector3 CP1a = (CasterPOS - RandomD * (CasterPOS - (Caster.transform.forward + CasterPOS)));
            Vector3 CP1b = (CP1a - RandomH * (CP1a - (Caster.transform.right + CP1a)));
            Vector3 CP1c = (CP1b - RandomV * (CP1b - (Caster.transform.up + CP1b)));

            Vector3 CP2a = (TargetPOS + RandomD * (TargetPOS - (Caster.transform.forward + TargetPOS)));
            Vector3 CP2b = (CP2a + RandomH * (CP2a - (Caster.transform.right + CP2a)));
            Vector3 CP2c = (CP2b - RandomV * (CP2b - (Caster.transform.up + CP2b)));

            Spline.Points[0] = CasterPOS;
            Spline.Points[1] = CP1c;
            Spline.Points[2] = CP2c;
            Spline.Points[3] = TargetPOS;
        }
        else
        {
            float RandomH = Random.Range(-Spline.RandomH * Spline.SplineLength, Spline.RandomH * Spline.SplineLength);
            float RandomV = Random.Range(0, Spline.RandomV) * Spline.SplineLength;
            float RandomD = Random.Range(0, Spline.RandomD) * Spline.SplineLength;

            Vector3 CP1a = (CasterPOS - RandomD * (CasterPOS - (Caster.transform.forward + CasterPOS)));
            Vector3 CP1b = (CP1a - RandomH * (CP1a - (Caster.transform.right + CP1a)));
            Vector3 CP1c = (CP1b - RandomV * (CP1b - (Caster.transform.up + CP1b)));

            RandomH = Random.Range(-Spline.RandomH * Spline.SplineLength, Spline.RandomH * Spline.SplineLength);
            RandomV = Random.Range(0, Spline.RandomV) * Spline.SplineLength;
            RandomD = Random.Range(0, Spline.RandomD) * Spline.SplineLength;

            Vector3 CP2a = (TargetPOS + RandomD * (TargetPOS - (Caster.transform.forward + TargetPOS)));
            Vector3 CP2b = (CP2a + RandomH * (CP2a - (Caster.transform.right + CP2a)));
            Vector3 CP2c = (CP2b - RandomV * (CP2b - (Caster.transform.up + CP2b)));

            Spline.Points[0] = CasterPOS;
            Spline.Points[1] = CP1c;
            Spline.Points[2] = CP2c;
            Spline.Points[3] = TargetPOS;
        }

        Spline.ControlPointLength1 = Vector3.Distance(Spline.Points[0], Spline.Points[1]);
        Spline.ControlPointVector1 = Vector3.Normalize(Spline.Points[1] - Spline.Points[0]);
        Spline.ControlPointLength2 = Vector3.Distance(Spline.Points[2], Spline.Points[3]);
        Spline.ControlPointVector2 = Vector3.Normalize(Spline.Points[3] - Spline.Points[2]);

    }

    // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))

}
