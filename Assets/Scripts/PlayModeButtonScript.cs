using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeButtonScript : MonoBehaviour
{
    public GameObject play_mode_panel;

    public bool is_showing_pm_panel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    //  SHOW PLAY MODE PANEL 
    //
    public void showPlayModes()
    {
        if (is_showing_pm_panel)
        {
            play_mode_panel.SetActive(false);

            is_showing_pm_panel = false;
        }
        else
        {
            play_mode_panel.SetActive(true);

            is_showing_pm_panel = true;
        }
    }
}
