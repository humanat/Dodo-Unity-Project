using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayModePanelButtonScript : MonoBehaviour
{
    GameManager gameManager;


    public Button PlayModeButton;

    public GameObject PlayModePanel;

    public GameObject PlayModePanelItem;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //
    //  SET PLAY MODE 
    //
    public void setPlayMode()
    {
        //
        //  GET CURRENT STATE
        //
        Enum_Types.states state = gameManager.state;


        //
        //  GET PLAYER ON TURN
        //
        int player_on_turn = gameManager.player_on_turn;


        switch (PlayModePanelItem.transform.name)
        {
            case "Human_vs_AI_Button":

                //
                //  SET PLAY MODE 
                //
                gameManager.current_play_mode = Enum_Types.play_modes.human_vs_AI;


                //
                //  SET PLAYER AIs TO {false, true}    //  HUMAN VS AI
                //
                gameManager.player_AIs = new bool[] { false, true };


                //
                //  IF PLAYER 1 ON TURN
                //  
                //      Unhighlight all tiles
                //
                //  ELSE IF PLAYER 0 ON TURN 
                //
                //      Highlight source tiles
                //
                if (player_on_turn == 1)
                {
                    switch (state)
                    {
                        case Enum_Types.states.source_tiles_highlighted:
                        case Enum_Types.states.destination_tiles_highlighted:

                            gameManager.state = Enum_Types.states.AI_idle;

                            gameManager.unhighlightAllTiles();

                            break;
                    }
                }
                else // player_on_turn == 0
                {
                    switch (state)
                    {
                        case Enum_Types.states.AI_idle:

                            gameManager.state = Enum_Types.states.source_tiles_highlighted;

                            gameManager.highlightSourceTiles();

                            break;
                    }
                }

                break;


            case "AI_vs_Human_Button":

                //
                //  SET PLAY MODE 
                //
                gameManager.current_play_mode = Enum_Types.play_modes.AI_vs_human;


                //
                //  SET PLAYER AIs TO {true, false}    //  AI VS HUMAN
                //
                gameManager.player_AIs = new bool[] { true, false };


                //
                //  IF PLAYER 0 ON TURN
                //  
                //      Unhighlight all tiles
                //      Show AI Play Button
                //
                //  ELSE IF PLAYER 1 ON TURN 
                //
                //      Hide AI Play Button
                //      Highlight source tiles
                //
                if (player_on_turn == 0)
                {
                    switch (state)
                    {
                        case Enum_Types.states.source_tiles_highlighted:
                        case Enum_Types.states.destination_tiles_highlighted:

                            gameManager.state = Enum_Types.states.AI_idle;

                            gameManager.unhighlightAllTiles();

                            //gameManager.showAI_PlayButton();
                            //gameManager.disengageAI();

                            break;
                    }
                }
                else if (player_on_turn == 1)
                {
                    switch (state)
                    {
                        case Enum_Types.states.AI_idle:

                            gameManager.state = Enum_Types.states.source_tiles_highlighted;

                            //gameManager.hideAI_PlayButton();

                            gameManager.highlightSourceTiles();

                            break;
                    }
                }

                break;


            case "Human_vs_Human_Button":

                //
                //  SET PLAY MODE 
                //
                gameManager.current_play_mode = Enum_Types.play_modes.human_vs_human;


                //
                //  SET PLAYER AIs TO {false, false}    //  HUMAN VS HUMAN
                //
                gameManager.player_AIs = new bool[] { false, false };


                switch (state)
                {
                    case Enum_Types.states.AI_idle:

                        gameManager.state = Enum_Types.states.source_tiles_highlighted;

                        gameManager.highlightSourceTiles();

                        break;
                }

                break;


            case "AI_vs_AI_Button":

                //
                //  SET PLAY MODE 
                //
                gameManager.current_play_mode = Enum_Types.play_modes.AI_vs_AI;


                //
                //  SET PLAYER AIs TO {true, true}      //  AI VS AI
                //
                gameManager.player_AIs = new bool[] { true, true};


                switch (state)
                {
                    case Enum_Types.states.source_tiles_highlighted:
                    case Enum_Types.states.destination_tiles_highlighted:

                        gameManager.state = Enum_Types.states.AI_idle;

                        gameManager.disengageAI();

                        gameManager.unhighlightAllTiles();

                        break;
                }

                break;
        }


        //
        //  HIDE PLAY MODE PANEL
        //
        PlayModePanel.SetActive(false);

        PlayModeButton.GetComponent<PlayModeButtonScript>().is_showing_pm_panel = false;


        //
        //  SET PLAY MODE BUTTON IMAGE
        //
        gameManager.setPlayModeButtonImage();
    }
}
