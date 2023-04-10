using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_BackOneButtonScript : MonoBehaviour
{
    public Button Recorder_BackOneButton;

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
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_One_Inactive");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Back_One_Active");
        }

        Recorder_BackOneButton.image.sprite = sprite;
    }


    public void backOne()
    {
        history.backOne();
    }
}
