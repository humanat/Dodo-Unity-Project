using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public GameObject menu_panel;

    public bool is_showing_menu_panel;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //
    //  SHOW MENU PANEL 
    //
    public void showMenu()
    {
        if (is_showing_menu_panel)
        {
            menu_panel.SetActive(false);

            is_showing_menu_panel = false;
        }
        else
        {
            menu_panel.SetActive(true);

            is_showing_menu_panel = true;
        }
    }
}
