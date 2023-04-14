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
        // CAN'T DO UPDATE HERE BECAUSE DOESN'T WORK WHEN BUTTON IS INVISIBLE (NON-ACTIVE)
    }

    public void AI_Play()
    {
        gameManager.engageAI();
        //gameManager.is_AI_engaged = true;

        gameManager.ai_Script.takeTurn();
    }
}
