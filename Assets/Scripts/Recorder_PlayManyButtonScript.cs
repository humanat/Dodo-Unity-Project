using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recorder_PlayManyButtonScript : MonoBehaviour
{
    public Button Recorder_PlayManyButton;

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
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Play_Many_Inactive");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Recorder/Dodo_Play_Many_Active");
        }

        Recorder_PlayManyButton.image.sprite = sprite;
    }


    public void playMany()
    {
        gameManager.playing_forward_many = true;

        history.playMany();
    }
}
