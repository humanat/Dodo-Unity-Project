using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelButtonScript : MonoBehaviour
{
    public Button menu_button;

    public GameObject menu_panel;

    //public bool is_showing_menu_panel;


    public Button menu_panel_item_button;

    public GameManager gameManager;

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
    //  MENU ACTION
    //
    public void menuAction()
    {
        switch (menu_panel_item_button.transform.name)
        {
            case "MenuPieButton":                                     //  TOGGLE PIE RULE ENABLEMENT

                Sprite sprite;

                //
                //  TOGGLE IS PIE ENABLED 
                //
                bool is_pie_enabled = gameManager.GetComponent<GameManager>().is_pie_enabled;

                //
                //  ONLY FUNCTIONAL ON TURN 0
                //
                if (gameManager.number_of_turns == 0)
                {
                    if (is_pie_enabled)
                    {
                        gameManager.GetComponent<GameManager>().is_pie_enabled = false;

                        //
                        //  Set menu pie button image to gray
                        //
                        sprite = Resources.Load<Sprite>("Menu/Dodo_Pie_Gray");
                    }
                    else
                    {
                        gameManager.GetComponent<GameManager>().is_pie_enabled = true;

                        //
                        //  Set menu pie button image to green
                        //
                        sprite = Resources.Load<Sprite>("Menu/Dodo_Pie");
                    }

                    menu_panel_item_button.image.sprite = sprite;
                }


                StartCoroutine(delayHideMenuPanel(.63f));

                break;
        }
    }


    //
    //  Delay hide menu panel 
    //
    IEnumerator delayHideMenuPanel(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        menu_panel.SetActive(false);

        menu_button.GetComponent<MenuButtonScript>().is_showing_menu_panel = false;
    }
}
