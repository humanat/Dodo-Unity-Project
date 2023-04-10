using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject PieButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void invokePie()
    {
        //
        //  CHANGE PLAYER ID's
        //      FROM 0 TO 1
        //      FROM 1 TO 0
        //
        Checker[] all_checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in all_checkers)
        {
            if (checker.player_ID == 0)
            {
                checker.player_ID = 1;
            }
            else
            {
                checker.player_ID = 0;
            }
        }


        //
        //  CHANGE PLAYER COLORS
        //      FROM RED TO BLUE
        //      FROM BLUE TO RED
        //
        gameManager.player_colors = new Enum_Types.colors[] { Enum_Types.colors.blue, Enum_Types.colors.red };


        //
        //  HIDE PIE BUTTON
        //
        gameManager.hidePieButton();


        //
        //  UNHIGHLIGHT ALL TILES
        //
        gameManager.unhighlightAllTiles();


        //
        //  INCREMENT NUMBER OF TURNS
        //
        ++gameManager.number_of_turns;


        //
        //  NEXT TURN 
        //
        gameManager.was_turn_completed = true;
    }
}
