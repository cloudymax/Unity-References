using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cam_manager
{
    [System.Serializable]
    public class camera_manager : MonoBehaviour
    {
        public profile working_profile;

        //all our pre-made camera looks
        public List<camera_look> looks_book = new List<camera_look>();

        //index that indicates the actice look
        int _active_look;
        public int active_look
        {
            get
            {
                return _active_look;
            }
            set
            {
                var input = value;
                if (input != _active_look)
                {
                    var old = _active_look;

                    if (!lerping)
                    {
                        _active_look = value;
                        look = looks_book[active_look];
                        lerping = true;
                        StartCoroutine(lerp_look(looks_book[old], looks_book[value]));
                    }
                }
            }
        }

        //the current look
        public camera_look look;

        //the camera that does the looking
        public Camera look_camera;

        public Vector3 cam;
        public Vector3 target;

        bool lerping;

        private void Start()
        {
            look = looks_book[active_look];
            working_profile.look = look;
            cam = look.camera_position;
            target = look.camera_target;
        }

        void LateUpdate()
        {
            look_camera.transform.position = cam;
            look_camera.transform.LookAt(target);
        }

        public IEnumerator lerp_look(camera_look old_look, camera_look new_look)
        {
            float journey = 0f;
            while (journey <= .5f)
            {
                journey = journey + Time.deltaTime;
                float percent = Mathf.Clamp01(journey / .5f);

                cam = Vector3.Lerp(old_look.camera_position, new_look.camera_position, percent);
                target = Vector3.Lerp(old_look.camera_target, new_look.camera_target, percent);

                yield return null;
            }

            lerping = false;
        }

    }

    [System.Serializable]
    public class profile
    {
        public camera_look look;

        public float sun_strength;
        public float sun_x;
        public float sun_y;

        public float overhead_strength;
        public float spot_strength;
        public float ambient_strength;

        public float light_color_r;
        public float light_color_g;
        public float light_color_b;
        public float light_color_a;

        public bool bloom_enabled;
        public float bloom_intensity;
        public float bloom_threshold;

        public bool auto_dof;
        public bool dof_enabled;
        public float dof_focal_distance;

        public bool window_video;
        public string window_video_url;

        public bool tv_on;
        public float tv_brightness;
        public string tv_url;

        public string poster_1_url;
        public string poster_2_url;
        public string poster_3_url;
    }
}
