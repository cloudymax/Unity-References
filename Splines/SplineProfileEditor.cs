using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineManager))]
public class SplineProfileEditor : Editor
{
    // Distance along a line: (Origin - Distance * (Origin - (Vector + Origin)))
    [SerializeField]
    bool EditorOptions;
    [SerializeField]
    bool TextOptions;
    [SerializeField]
    bool ControlPointOptions;
    [SerializeField]
    bool DirectionOptions;
    [SerializeField]
    bool LineStepOptions;
    [SerializeField]
    bool RandomizerOptions;
    [SerializeField]
    bool OscOptions;
    [SerializeField]
    bool OrbitOptions;
    [SerializeField]
    bool InfoOptions;

    public bool OscH = false;
    public bool OscV = false;

    private void OnSceneGUI()
    {
        SplineManager Spline = (SplineManager)target;

        GUIStyle style = new GUIStyle(GUI.skin.GetStyle("label"));
        style.normal.textColor = Spline.PointColor;
        style.fontSize = Spline.TextSize;

        if(Spline.Source != null && Spline.Target != null && Spline.Spline != null)
        {
            #region stuff
            Spline.Spline.OscillationRangeH = Spline.OscillationRangeH;
            Spline.Spline.OscillationSpeedH = Spline.OscillationSpeedH;
            Spline.Spline.OscillationRangeV = Spline.OscillationRangeV;
            Spline.Spline.OscillationSpeedV = Spline.OscillationSpeedV;

            Spline.Spline.OscCurveH = Spline.OscCurveH;
            Spline.Spline.OscCurveV = Spline.OscCurveV;
            Spline.Spline.OrbitDistanceCurve = Spline.OrbitDistanceCurve;
            Spline.Spline.OrbitSpeedCurve = Spline.OrbitSpeedCurve;

            Spline.Spline.OrbitDistance = Spline.OrbitRange;
            Spline.Spline.OrbitSpeed = Spline.OrbitSpeed;

            Spline.Spline.Points[0] = Spline.Source.transform.position;
            Spline.Spline.Points[3] = Spline.Target.transform.position;
            Spline.Spline.Victim = Spline.Target;
            Spline.Spline.Caster = Spline.Source;

            if (Spline.AutoRandom != true)
            {
                Spline.Spline.Points[1] = (Spline.Spline.Points[0] - (Spline.Spline.ControlPointLength1 * Spline.Spline.SplineScale) * (Spline.Spline.Points[0] - (Spline.Spline.ControlPointVector1 + Spline.Spline.Points[0])));
                Spline.Spline.Points[2] = (Spline.Spline.Points[3] + (Spline.Spline.ControlPointLength2 * Spline.Spline.SplineScale) * (Spline.Spline.Points[3] - (Spline.Spline.ControlPointVector2 + Spline.Spline.Points[3])));
                Spline.SplineScale = Spline.CurrentLength / Spline.OriginalLength;
                Spline.Spline.SplineScale = Spline.SplineScale;
                Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Caster.transform.position, Spline.Spline.Victim.transform.position);
            }

            Spline.Spline.RandomD = Spline.RandomD;
            Spline.Spline.RandomH = Spline.RandomH;
            Spline.Spline.RandomV = Spline.RandomV;

            Spline.Spline.Randomize = Spline.AutoRandom;
            Spline.Spline.Reflection = Spline.RandomizerReflection;

            Vector3 Source = Spline.Source.transform.position;
            Vector3 Target = Spline.Target.transform.position;

            Vector3 Center = SplineMaker2.GetPoint(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], Spline.T);
            Vector3 Tangent = SplineMaker2.GetTangent(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], Spline.T);

            Vector3 TangentVector = SplineMaker2.GetDirection(Spline.T, Spline.Spline, Spline.Source.transform);
            Vector3 TangentPoint = Center - Spline.DirectionDistance * (Center - (TangentVector + Center));

            Vector3 BiNormal = SplineMaker2.GetBiNormal(TangentVector);
            Vector3 BiNormalPoint = Center - Spline.DirectionDistance * (Center - (BiNormal + Center));

            Vector3 Normal = SplineMaker2.GetNormal(TangentVector, BiNormal);
            Vector3 NormalPoint = Center - Spline.DirectionDistance * (Center - (Normal + Center));

            Spline.CurrentLength = Vector3.Distance(Spline.Source.transform.position, Spline.Target.transform.position);

            if (Spline.LockScale == false)
            {
                Spline.Spline.OriginalLength = Spline.OriginalLength;
                Spline.OriginalLength = Vector3.Distance(Spline.Source.transform.position, Spline.Target.transform.position);
            }

            // create the handles to edit the points

            if (Spline.EditorHandles == true)
            {
                if (Spline.LockOrigins != true)
                {
                    Quaternion handleRotation0 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[0], handleRotation0);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p0 = Handles.DoPositionHandle(Source, handleRotation0);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[0] = p0;
                        Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                        Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                    }

                    Quaternion handleRotation3 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[3], handleRotation3);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p3 = Handles.DoPositionHandle(Target, handleRotation3);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[3] = p3;
                        Spline.Spline.SplineLength = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[3]);
                        Spline.Spline.LenghtVector = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[0]);
                    }
                }

                if (Spline.ShowControlPoints)
                {
                    Quaternion handleRotation1 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p1 = Handles.DoPositionHandle(Spline.Spline.Points[1], handleRotation1);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[1] = p1;
                        Spline.Spline.ControlPointLength1 = Vector3.Distance(Spline.Spline.Points[0], Spline.Spline.Points[1]);
                        Spline.Spline.ControlPointVector1 = Vector3.Normalize(Spline.Spline.Points[1] - Spline.Spline.Points[0]);
                    }


                    Quaternion handleRotation2 = Tools.pivotRotation == PivotRotation.Local ? Quaternion.identity : Quaternion.identity; ;
                    Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
                    EditorGUI.BeginChangeCheck();
                    Vector3 p2 = Handles.DoPositionHandle(Spline.Spline.Points[2], handleRotation2);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Spline.Spline.Points[2] = p2;
                        Spline.Spline.ControlPointLength2 = Vector3.Distance(Spline.Spline.Points[2], Spline.Spline.Points[3]);
                        Spline.Spline.ControlPointVector2 = Vector3.Normalize(Spline.Spline.Points[3] - Spline.Spline.Points[2]);
                    }
                }


            }


            // draw stuff here
            //control points
            Handles.color = Spline.ControlPointColor;
            Handles.SphereCap(0, Spline.Spline.Points[0], Quaternion.identity, Spline.ControlPointSize * 1.5f);
            Handles.Label(Spline.Spline.Points[0] + Vector3.up * Spline.LabelHeight, "Start", style);
            if (Spline.ShowControlPoints)
            {
                Handles.SphereCap(1, Spline.Spline.Points[1], Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.Label(Spline.Spline.Points[1] + Vector3.up * Spline.LabelHeight, "Control Point 1", style);
                Handles.SphereCap(2, Spline.Spline.Points[2], Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.Label(Spline.Spline.Points[2] + Vector3.up * Spline.LabelHeight, "Control Point 2", style);
                Handles.DrawLine(Spline.Spline.Points[0], Spline.Spline.Points[1]);
                Handles.DrawLine(Spline.Spline.Points[2], Spline.Spline.Points[3]);
            }


            Handles.SphereCap(3, Spline.Spline.Points[3], Quaternion.identity, Spline.ControlPointSize * 1.5f);
            Handles.Label(Spline.Spline.Points[3] + Vector3.up * Spline.LabelHeight, "End", style);

            Handles.DrawBezier(Spline.Spline.Points[0], Spline.Spline.Points[3], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.ControlPointColor, null, Spline.SplineLineWidth);

            if (Spline.OscillateHorizontal)
            {
                Spline.Spline.OscillateH = true;
                Handles.SphereCap(5, SplineMaker2.OscillateH(Center, BiNormal, Spline.OscillationRangeH, Spline.Spline, Spline.T), Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Vector3 OscP1 = Center - Spline.OscillationRangeH / 2 * (Center - (BiNormal + Center));

                Vector3 OscP2 = Center + Spline.OscillationRangeH / 2 * (Center - (BiNormal + Center));
                Handles.DrawLine(OscP1, OscP2);

            }
            else
            {
                Spline.Spline.OscillateH = false;
            }

            if (Spline.OscillateVertical)
            {
                Spline.Spline.OscillateV = true;
                Handles.SphereCap(5, SplineMaker2.OscillateV(Center, Normal, Spline.OscillationRangeH, Spline.Spline, Spline.T), Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Vector3 OscP1 = Center - Spline.OscillationRangeV / 2 * (Center - (Normal + Center));

                Vector3 OscP2 = Center + Spline.OscillationRangeV / 2 * (Center - (Normal + Center));
                Handles.DrawLine(OscP1, OscP2);

            }
            else
            {
                Spline.Spline.OscillateV = false;
            }

            if (Spline.Orbit)
            {
                Spline.Spline.Orbit = true;
                Vector3 OrbitPoint = SplineMaker2.GetOrbitPoint(Center, TangentVector, BiNormal, Spline.Spline, Spline.T);
                Handles.SphereCap(5, OrbitPoint, Quaternion.identity, Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(OrbitPoint, Center);

            }
            else
            {
                Spline.Spline.Orbit = false;
            }

            if (Spline.FollowSpline)
            {
                Spline.Spline.FollowSpline = true;
                Handles.SphereCap(4, Center, Quaternion.identity, Spline.ControlPointSize * 1.5f);
            }
            else
            {
                Spline.Spline.FollowSpline = false;
            }

            if (Spline.ShowDirection)
            {
                Handles.ConeCap(1, TangentPoint, Quaternion.FromToRotation(Vector3.forward, TangentVector), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, TangentPoint);
                Handles.Label(TangentPoint + Vector3.up * Spline.LabelHeight, "Tangent", style);

                Handles.ConeCap(2, BiNormalPoint, Quaternion.FromToRotation(Vector3.forward, BiNormal), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, BiNormalPoint);
                Handles.Label(BiNormalPoint + Vector3.up * Spline.LabelHeight, "BiNormal", style);


                Handles.ConeCap(2, NormalPoint, Quaternion.FromToRotation(Vector3.forward, Normal), Spline.ControlPointSize * 1.5f);
                Handles.DrawLine(Center, NormalPoint);
                Handles.Label(NormalPoint + Vector3.up * Spline.LabelHeight, "Normal", style);

                Vector3 lineStart = Spline.Source.transform.position;
                Handles.color = Color.green;
                Handles.DrawLine(lineStart, lineStart + SplineMaker2.GetDirection(0f, Spline.Spline, Spline.Source.transform));

                if (Spline.ShowLineSteps)
                {
                    for (int i = 1; i <= Spline.lineSteps; i++)
                    {
                        Vector3 lineEnd = SplineMaker2.GetPoint(Spline.Spline.Points[0], Spline.Spline.Points[1], Spline.Spline.Points[2], Spline.Spline.Points[3], i / (float)Spline.lineSteps);
                        Handles.color = Color.white;
                        Handles.DrawLine(lineStart, lineEnd);
                        Handles.color = Spline.ControlPointColor;

                        Handles.DrawLine(lineEnd, lineEnd + SplineMaker2.GetDirection(i / (float)Spline.lineSteps, Spline.Spline, Spline.Source.transform) * Spline.LineStepDIstance);
                        lineStart = lineEnd;
                    }
                }
            }
            #endregion

            if (GUI.changed)
            {
                EditorUtility.SetDirty(Spline.Spline);
                EditorUtility.SetDirty(Spline);
            }
        }
    }


    public override void OnInspectorGUI()
    {
        #region temp
        SplineManager Spline = (SplineManager)target;

        if (Spline.Source != null && Spline.Target != null && Spline.Spline != null)
        {
            Vector3 Source = Spline.Source.transform.position;
            Vector3 Target = Spline.Target.transform.position;

            if (GUILayout.Button("Reset"))
            {
                Spline.Spline.Points[0] = Source;
                Spline.Spline.Points[1] = new Vector3(-5, 0, 0);
                Spline.Spline.Points[2] = new Vector3(0, 0, 0);
                Spline.Spline.Points[3] = Target;
            }

        }
        #endregion

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Spline Profile");
        Spline.Spline = (CreateSplineProfile)EditorGUILayout.ObjectField(Spline.Spline, typeof(CreateSplineProfile), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Source Object");
        Spline.Source = (GameObject)EditorGUILayout.ObjectField(Spline.Source, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Target Object");
        Spline.Target = (GameObject)EditorGUILayout.ObjectField(Spline.Target, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Strict Follow Spline");
        Spline.FollowSpline = EditorGUILayout.Toggle(Spline.FollowSpline);
        EditorGUILayout.EndHorizontal();

        if (Spline.FollowSpline)
        {
            Spline.Orbit = false;
            Spline.OscillateHorizontal = false;
            Spline.OscillateVertical = false;
        }

        #region Editor Options
        EditorOptions = EditorGUILayout.Foldout(EditorOptions, "Editor Options");
        if (EditorOptions)
        {
            #region Spline Width
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spline Width");
            Spline.SplineLineWidth = EditorGUILayout.Slider(Spline.SplineLineWidth, .01f, 10f);
            EditorGUILayout.EndHorizontal();
            #endregion

            #region Show Control Points
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Show Control Points in Editor");
            Spline.ShowControlPoints = EditorGUILayout.Toggle(Spline.ShowControlPoints);
            EditorGUILayout.EndHorizontal();

            if (Spline.ShowControlPoints)
            {
                #region Show Handles
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Show Handles in Editor");
                Spline.EditorHandles = EditorGUILayout.Toggle(Spline.EditorHandles);
                EditorGUILayout.EndHorizontal();

                if (Spline.EditorHandles)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Show Control Point Handles Only");
                    Spline.LockOrigins = EditorGUILayout.Toggle(Spline.LockOrigins);
                    EditorGUILayout.EndHorizontal();
                }
                #endregion

                ControlPointOptions = EditorGUILayout.Foldout(ControlPointOptions, "Control Point Options");
                if (ControlPointOptions)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Control Point Color");
                    Spline.ControlPointColor = EditorGUILayout.ColorField(Spline.ControlPointColor);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Control Point Size");
                    Spline.ControlPointSize = EditorGUILayout.Slider(Spline.ControlPointSize, .01f, 10f);
                    EditorGUILayout.EndHorizontal();
                }
            }
            #endregion

            #region Text Options
            TextOptions = EditorGUILayout.Foldout(TextOptions, "Text Options");
            if (TextOptions)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Text Color");
                Spline.PointColor = EditorGUILayout.ColorField(Spline.PointColor);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Text Size");
                Spline.TextSize = EditorGUILayout.IntField(Spline.TextSize);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Text Height");
                Spline.LabelHeight = EditorGUILayout.Slider(Spline.LabelHeight, 0, 10);
                EditorGUILayout.EndHorizontal();
            }
            #endregion

            #region Direction Options
            DirectionOptions = EditorGUILayout.Foldout(DirectionOptions, "Direction Options");
            if (DirectionOptions)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Show Direction Information");
                Spline.ShowDirection = EditorGUILayout.Toggle(Spline.ShowDirection);
                EditorGUILayout.EndHorizontal();

                if (Spline.ShowDirection)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Direction Length");
                    Spline.DirectionDistance = EditorGUILayout.Slider(Spline.DirectionDistance, 1, 10);
                    EditorGUILayout.EndHorizontal();


                    LineStepOptions = EditorGUILayout.Foldout(LineStepOptions, "Line Step Options");
                    if (LineStepOptions)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Show Line Steps");
                        Spline.ShowLineSteps = EditorGUILayout.Toggle(Spline.ShowLineSteps);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Line Step Length");
                        Spline.LineStepDIstance = EditorGUILayout.Slider(Spline.LineStepDIstance, 1, 10);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Line Step Length");
                        Spline.lineSteps = EditorGUILayout.IntField(Spline.lineSteps);
                        EditorGUILayout.EndHorizontal();
                    }
                }


            }
            #endregion

        }

        #endregion

        #region Randomization Options
        RandomizerOptions = EditorGUILayout.Foldout(RandomizerOptions, "Randomizer Options");
        if (RandomizerOptions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Enable Randomizer Functions");
            Spline.AutoRandom = EditorGUILayout.Toggle(Spline.AutoRandom);
            EditorGUILayout.EndHorizontal();

            if (Spline.AutoRandom)
            {
                if (GUILayout.Button("Generate Randomized Spline"))
                {
                    SplineMaker2.RandomControlPoints(Spline.Source, Spline.Target, Spline.Spline);
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Symmetrical Randomization");
                Spline.RandomizerReflection = EditorGUILayout.Toggle(Spline.RandomizerReflection);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Vertical Influence (%)");
                Spline.RandomV = EditorGUILayout.Slider(Spline.RandomV, 0, 1);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Horizontal Influence (%)");
                Spline.RandomH = EditorGUILayout.Slider(Spline.RandomH, 0, 1);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Distance (%)");
                Spline.RandomD = EditorGUILayout.Slider(Spline.RandomD, 0, 1);
                EditorGUILayout.EndHorizontal();


            }


        }
        #endregion

        #region Oscillation
        OscOptions = EditorGUILayout.Foldout(OscOptions, "Oscillation Options");
        if (OscOptions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Horizontal On/Off");
            OscH = EditorGUILayout.Toggle(OscH);
            EditorGUILayout.EndHorizontal();

            Spline.OscillateHorizontal = OscH;
            

            if (Spline.OscillateHorizontal)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Horizontal Osc Range");
                Spline.OscillationRangeH = EditorGUILayout.Slider(Spline.OscillationRangeH, 0.1f, 10);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Oscillation Distance Curve");
                Spline.OscCurveH = EditorGUILayout.CurveField(Spline.OscCurveH);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Vertical On/Off");
            OscV = EditorGUILayout.Toggle(OscV);
            EditorGUILayout.EndHorizontal();

            Spline.OscillateVertical = OscV;
            

            if (Spline.OscillateVertical)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Vertical Osc Range");
                Spline.OscillationRangeV = EditorGUILayout.Slider(Spline.OscillationRangeV, 0.1f, 10);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Oscillation Distance Curve");
                Spline.OscCurveV = EditorGUILayout.CurveField(Spline.OscCurveV);
                EditorGUILayout.EndHorizontal();
            }
        }
        #endregion

        #region Orbit
        OrbitOptions = EditorGUILayout.Foldout(OrbitOptions, "Orbit Options");
        if (OrbitOptions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Enable Orbit");
            Spline.Orbit = EditorGUILayout.Toggle(Spline.Orbit);
            EditorGUILayout.EndHorizontal();

            if (Spline.Orbit)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Orbit Max Range");
                Spline.OrbitRange = EditorGUILayout.Slider(Spline.OrbitRange, 0.1f, 10);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Orbit Max Speed");
                Spline.OrbitSpeed = EditorGUILayout.IntField(Spline.OrbitSpeed);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Orbit Distance Curve");
                Spline.OrbitDistanceCurve = EditorGUILayout.CurveField(Spline.OrbitDistanceCurve);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Orbit Speed Curve");
                Spline.OrbitSpeedCurve = EditorGUILayout.CurveField(Spline.OrbitSpeedCurve);
                EditorGUILayout.EndHorizontal();
            }
        }
        #endregion

        #region Information
        InfoOptions = EditorGUILayout.Foldout(InfoOptions, "Spline Information");
        if (InfoOptions)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spline Original Length");
            EditorGUILayout.FloatField(Spline.OriginalLength);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spline Current Length");
            EditorGUILayout.FloatField(Spline.CurrentLength);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spline Scale");
            EditorGUILayout.FloatField(Spline.SplineScale);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Lock Scale");
            Spline.LockScale = EditorGUILayout.Toggle(Spline.LockScale);
            EditorGUILayout.EndHorizontal();

        }

        #endregion

        #region T 


            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(" T Value");
            EditorGUILayout.EndHorizontal();
            Spline.T = EditorGUILayout.Slider(Spline.T, 0, 1);


        #endregion


       // base.OnInspectorGUI();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(Spline.Spline);
            EditorUtility.SetDirty(Spline);
        }
    }


    }
