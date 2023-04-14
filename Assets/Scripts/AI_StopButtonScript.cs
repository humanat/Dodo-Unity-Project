using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StopButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject AI_StopButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // CAN'T DO UPDATE HERE BECAUSE DOESN'T WORK WHEN BUTTON IS INVISIBLE (NON-ACTIVE)

        // SEE GAME MANAGER UPDATE FUNCTION
    }

    public void AI_Stop()
    {
        gameManager.disengageAI();
        //gameManager.is_AI_engaged = false;
    }
}
