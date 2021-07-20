using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Look", menuName = "New Look", order = 1)]
[System.Serializable]
public class camera_look : ScriptableObject
{
    public Vector3 camera_position;
    public Vector3 camera_target;
    public string description;
}
