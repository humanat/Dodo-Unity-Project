using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_X_ButtonScript : MonoBehaviour
{
    public GameObject menu_button;

    public GameObject menu_panel;

    public GameObject about_panel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeAboutPanel()
    {
        about_panel.SetActive(false);

        menu_button.GetComponent<MenuButtonScript>().is_showing_about_panel = false;
    }
}
