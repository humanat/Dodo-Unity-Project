using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_StopButtonScript : MonoBehaviour
{
    public Button Recorder_StopButton;

    public HistoryScript history;


    public GameManager gameManager;

    //public bool is_clickable = false;

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

        if (gameManager.playing_backward_many == true || gameManager.playing_forward_many == true)
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Stop_Active");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Stop_Inactive");
        }

        Recorder_StopButton.image.sprite = sprite;
    }


    public void stop()
    {
        history.stop();
    }
}
