using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class AI_PlayButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject AI_PlayButton;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (gameManager.state == Enum_Types.states.AI_idle &&
            gameManager.player_AIs[gameManager.player_on_turn] == true)
        {
            Debug.Log("yes");

            AI_PlayButton.SetActive(true);
        }
        else
        {
            Debug.Log("no");

            AI_PlayButton.SetActive(false);
        }
        */
    }

    public void AI_Play()
    {
        //gameManager.hideAI_PlayButton();

        gameManager.ai_Script.takeTurn();
    }
}
