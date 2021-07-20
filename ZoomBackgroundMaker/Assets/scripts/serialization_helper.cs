using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cam_manager;
using System.IO;

public class serialization_helper : MonoBehaviour
{
    public camera_manager cm;


    void Save_Settings()
    {
        string json = JsonUtility.ToJson(cm.working_profile);
    }

    void Load_Settings(string json_string)
    {
        cm.working_profile = JsonUtility.FromJson<profile>(json_string);
    }

    
}
