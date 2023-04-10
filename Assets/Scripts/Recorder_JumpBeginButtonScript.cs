using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_JumpBeginButtonScript : MonoBehaviour
{
    public Button Recorder_JumpBeginButton;

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

        if (history.replay_node == history.positions.First)
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_Begin_Inactive");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Jump_Begin_Active");
        }

        Recorder_JumpBeginButton.image.sprite = sprite;
    }


    public void jumpToBeginning()
    {
        history.jumpToBeginning();
    }
}
