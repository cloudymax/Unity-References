using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_driver : MonoBehaviour
{
    public GameObject Toggles;
    public GameObject Lights;
    public GameObject Bloom;
    public GameObject DOF;
    public GameObject Window;
    public GameObject Art;
    public GameObject ColorPicker;
    public GameObject TV;
    public GameObject Camera;
    public GameObject Rendering;

    public bool show_toggles;
    public bool show_lights;
    public bool show_bloom;
    public bool show_dof;
    public bool show_window;
    public bool show_art;
    public bool show_colorpicker;
    public bool show_tv;
    public bool show_camera;
    public bool show_rendering;

    public void light_toggle()
    {
        if (!show_lights)
        {
            show_lights = true;
        }
        else
        {
            show_lights = false;
        }

        toggle_toggle();
        Lights.SetActive(show_lights);
    }

    public void bloom_toggle()
    {
        if (!show_bloom)
        {
            show_bloom = true;
        }
        else
        {
            show_bloom = false;
        }

        toggle_toggle();
        Bloom.SetActive(show_bloom);
    }

    public void dof_toggle()
    {
        if (!show_dof)
        {
            show_dof = true;
        }
        else
        {
            show_dof = false;
        }

        toggle_toggle();
        DOF.SetActive(show_dof);
    }

    public void window_toggle()
    {
        if (!show_window)
        {
            show_window = true;
        }
        else
        {
            show_window = false;
        }

        toggle_toggle();
        Window.SetActive(show_window);
    }

    public void art_toggle()
    {
        if (!show_art)
        {
            show_art = true;
        }
        else
        {
            show_art = false;
        }

        toggle_toggle();
        Art.SetActive(show_art);
    }

    public void colorpicker_toggle()
    {
        if (!show_colorpicker)
        {
            show_colorpicker = true;
        }
        else
        {
            show_colorpicker = false;
        }

        toggle_toggle();
        ColorPicker.SetActive(show_colorpicker);
    }

    public void tv_toggle()
    {
        if (!show_tv)
        {
            show_tv = true;
        }
        else
        {
            show_tv = false;
        }

        toggle_toggle();
        TV.SetActive(show_tv);
    }
    
    public void camera_toggle()
    {
        if (!show_camera)
        {
            show_camera = true;
        }
        else
        {
            show_camera = false;
        }

        toggle_toggle();
        Camera.SetActive(show_camera);
    }

    public void rendering_toggle()
    {
        if (!show_rendering)
        {
            show_rendering = true;
        }
        else
        {
            show_rendering = false;
        }

        toggle_toggle();
        Rendering.SetActive(show_rendering);
    }

    public void toggle_toggle()
    {
        if (!show_toggles)
        {
            show_toggles = true;
        }
        else
        {
            show_toggles = false;
        }

        Toggles.SetActive(show_toggles);
    }

    private void Start()
    {
        toggle_toggle();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Toggles.activeSelf)
            {
                Toggles.SetActive(false);
            }
            else
            {
                Toggles.SetActive(true);
            }
        }
    }
}
