using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_JumpBeginButtonScript : MonoBehaviour
{
    public Button Recorder_JumpBeginButton;

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
        //  If history is not at the beginning  AND  checker isn't moving 
        //      Enable the button
        //
        //  Else
        //      Disable the button
        //
        if ( ! (history.replay_node == history.positions.First)
            && ! (gameManager.state == Enum_Types.states.checker_moving) )
        {
            is_button_enabled = true;

            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_Begin_Active");
        }
        else
        {
            is_button_enabled = false;

            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_Begin_Inactive");
        }

        Recorder_JumpBeginButton.image.sprite = sprite;
    }


    public void jumpToBeginning()
    {
        if (is_button_enabled)
        {
            history.jumpToBeginning();
        }
    }
}
