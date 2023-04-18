using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_BackManyButtonScript : MonoBehaviour
{
    public Button Recorder_BackManyButton;

    public bool is_button_enabled;

    public HistoryScript history;


    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        history = GameObject.FindObjectOfType<HistoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Sprite sprite;

        //
        //  If it's continously playing backward
        //      Disable button.  Only the Stop button can stop it
        //
        //  Else 
        //      If the history position is not at the first node  AND  a checker isn't moving
        //          Enable button
        //      Else
        //          Disable button
        //
        if (gameManager.playing_backward_many == true)
        {
            is_button_enabled = false;

            sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Running");
        }
        else
        {
            if ( ! (history.replay_node == history.positions.First)
            && ! (gameManager.state == Enum_Types.states.checker_moving) )
            {
                is_button_enabled = true;

                sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Active");
            }
            else
            {
                is_button_enabled = false;

                sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Inactive");
            }
        }

        Recorder_BackManyButton.image.sprite = sprite;
    }


    public void backMany()
    {
        if (is_button_enabled)
        {
            history.backMany();
        }
    }
}
