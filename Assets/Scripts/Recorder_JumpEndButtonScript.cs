using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_JumpEndButtonScript : MonoBehaviour
{
    public Button Recorder_JumpEndButton;

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
        //  If history is not at the end  AND  checker isn't moving 
        //      Enable the button
        //
        //  Else
        //      Disable the button
        //
        if (!(history.replay_node == history.positions.Last)
            && !(gameManager.state == Enum_Types.states.checker_moving))
        {
            is_button_enabled = true;

            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_End_Active");
        }
        else
        {
            is_button_enabled = false;

            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_End_Inactive");
        }

        /*
        if (history.replay_node == history.positions.Last)
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_End_Inactive");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_End_Active");
        }
        */

        Recorder_JumpEndButton.image.sprite = sprite;
    }


    public void jumpToEnd()
    {
        if (is_button_enabled)
        {
            history.jumpToEnd();
        }
        //history.jumpToEnd();
    }
}
