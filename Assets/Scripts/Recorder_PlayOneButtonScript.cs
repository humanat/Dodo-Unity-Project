using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_PlayOneButtonScript : MonoBehaviour
{
    public Button Recorder_PlayOneButton;

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

        if (history.replay_node == history.positions.Last)
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Play_One_Inactive");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Play_One_Active");
        }

        Recorder_PlayOneButton.image.sprite = sprite;
    }


    public void playOne()
    {
        history.playOne();
    }
}
