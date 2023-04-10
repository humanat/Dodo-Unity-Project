using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_LevelButtonScript : MonoBehaviour
{
    public GameObject AI_level_panel;

    public bool is_showing_AI_level_panel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //
    //  SHOW AI LEVEL PANEL 
    //
    public void showAI_Levels()
    {
        if (is_showing_AI_level_panel)
        {
            AI_level_panel.SetActive(false);

            is_showing_AI_level_panel = false;
        }
        else
        {
            AI_level_panel.SetActive(true);

            is_showing_AI_level_panel = true;
        }
    }
}
