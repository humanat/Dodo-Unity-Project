using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_LevelButtonPanelScript : MonoBehaviour
{
    public Button AI_level_button;

    public GameObject AI_level_panel;


    public GameObject AI_level_panel_item;


    GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //
    //  SET AI LEVEL 
    //
    public void setAI_Level()
    {
        switch (AI_level_panel_item.transform.name)
        {
            case "AI_Level_1_Button":

                //
                //  SET AI LEVEL 
                //
                gameManager.current_AI_level = 1;


                //
                //  SET AI LEVEL BUTTON IMAGE
                //
                Sprite sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_1");

                AI_level_button.image.sprite = sprite;

                AI_level_panel.SetActive(false);

                AI_level_button.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;

                break;


            case "AI_Level_2_Button":

                //
                //  SET AI LEVEL 
                //
                gameManager.current_AI_level = 2;


                //
                //  SET AI LEVEL BUTTON IMAGE
                //
                sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_2");

                AI_level_button.image.sprite = sprite;

                AI_level_panel.SetActive(false);

                AI_level_button.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;

                break;


            case "AI_Level_3_Button":

                //
                //  SET AI LEVEL 
                //
                gameManager.current_AI_level = 3;


                //
                //  SET AI LEVEL BUTTON IMAGE
                //
                sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_3");

                AI_level_button.image.sprite = sprite;

                AI_level_panel.SetActive(false);

                AI_level_button.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;

                break;


            case "AI_Level_4_Button":

                //
                //  SET AI LEVEL 
                //
                gameManager.current_AI_level = 4;


                //
                //  SET AI LEVEL BUTTON IMAGE
                //
                sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_4");

                AI_level_button.image.sprite = sprite;

                AI_level_panel.SetActive(false);

                AI_level_button.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;

                break;


            case "AI_Level_5_Button":

                //
                //  SET AI LEVEL 
                //
                gameManager.current_AI_level = 5;


                //
                //  SET AI LEVEL BUTTON IMAGE
                //
                sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_5");

                AI_level_button.image.sprite = sprite;

                AI_level_panel.SetActive(false);

                AI_level_button.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;

                break;


        }
    }
}
