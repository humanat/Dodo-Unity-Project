using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_BackManyButtonScript : MonoBehaviour
{
    public Button Recorder_BackManyButton;

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

        if (gameManager.playing_backward_many == true)
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Running");
        }
        else
        {
            if (history.replay_node == history.positions.First)
            {
                sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Inactive");
            }
            else
            {
                sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_Many_Active");
            }
        }

        Recorder_BackManyButton.image.sprite = sprite;
    }


    public void backMany()
    {
        history.backMany();
    }
}
