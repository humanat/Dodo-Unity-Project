//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using static AI_Script;

public class GameManager : MonoBehaviour
{
    //
    //  CURRENT STATE = INITIAL
    //
    public Enum_Types.states state = Enum_Types.states.source_tiles_highlighted;


    //
    //  PLAYER ON TURN - axiomatic - set at beginning and doesn't change
    //      0 = First player to take turn (move) 
    //      1 = Second player to take turn (move or invoke pie)
    public int player_on_turn = 0;

    public int number_of_turns = 0;


    // 
    //  PIE RULE 
    //
    public bool is_pie_enabled;

    //public bool are_colors_switched;    //  PLAYER 0 STARTS AS RED.  AFTER PIE RULE INVOCATION, HE SWITCHES TO BLUE.

    public GameObject PieButton;


    //
    //  STARTING PLAY MODE = HUMAN VS AI
    //
    public Enum_Types.play_modes current_play_mode = Enum_Types.play_modes.human_vs_AI;

    public Button PlayModeButton;


    //
    //  PLAYER COLORS ARRAY 
    //      Player 0 starts as Red
    //      Player 1 starts as Blue
    //      
    public Enum_Types.colors[] player_colors = new Enum_Types.colors[] { Enum_Types.colors.red, Enum_Types.colors.blue };


    //
    //  TURN VARIABLES 
    //
    //public bool was_destination_selected;

    public bool was_turn_completed;


    //
    //  TILE COLORS
    //
    public Material tile_material;

    public Material highlighted_tile_material;


    //
    //  WIN FLAGS
    //
    public GameObject Win_Flag_Red;
    public GameObject Win_Flag_Blue;


    //
    //  WINNING PLAYER ID
    //
    //      NULL, 0, or 1
    //
    public int? winning_player_ID = null;


    //  ##
    //  ##
    //
    //  AI  ##########################
    //
    //  ##
    //  ##

    //
    //  IS ENGAGED AI
    //
    public bool is_AI_engaged = true;

    //
    //  CURRENT AI LEVEL
    //
    public int current_AI_level = 1;


    //
    //  AI SCRIPT
    //
    public AI_Script ai_Script;


    //
    //  PLAYER AI ARRAY 
    //
    //      false = human player 
    //      true = AI player
    //
    //      Player 0 starts as human
    //      Player 1 starts as AI
    //
    public bool[] player_AIs = new bool[] { false, true };


    //
    //  AI PLAY BUTTON
    //
    public GameObject AI_PlayButton;


    //
    //  AI STOP BUTTON
    //
    public GameObject AI_StopButton;


    //
    //  HISTORY
    //
    HistoryScript history;


    public bool playing_backward_many = false;

    public bool playing_forward_many = false;


    //
    //  PLAY RECORDER STOP BUTTON
    //
    //public GameObject Recorder_StopButton_g_o;

    public Button Recorder_StopButton;




    //  #####
    //
    //  START 
    //
    //  #####
    //
    void Start()
    {
        ai_Script = GameObject.FindObjectOfType<AI_Script>();

        //ai_Script = new AI_Script();

        //
        //  PLAYER 0 STARTS AS HUMAN PLAYING RED 
        //
        //  START FIRT TURN AS SUCH
        //

        //
        //  STATE = SOURCE TILES HIGHLIGHTED
        //
        state = Enum_Types.states.source_tiles_highlighted;

        highlightSourceTiles();


        history = GameObject.FindObjectOfType<HistoryScript>();
    }


    //  ######
    //
    //  UPDATE 
    //
    //  ######
    //
    void Update()
    {
        //
        //  AI PLAY BUTTON
        //
        if (state == Enum_Types.states.AI_idle && player_AIs[player_on_turn] == true)
        {
            AI_PlayButton.SetActive(true);
        }
        else
        {
            AI_PlayButton.SetActive(false);
        }



        //
        //  AI STOP BUTTON
        //
        if (current_play_mode == Enum_Types.play_modes.AI_vs_AI 
            && is_AI_engaged)
        {
            AI_StopButton.SetActive(true);
        }
        else
        {
            AI_StopButton.SetActive(false);
        }



        //
        //  COMPLETED TURN
        //
        if (was_turn_completed)
        {
            was_turn_completed = false;

            //
            //  INCREMENT NUMBER OF TURNS
            //
            ++number_of_turns;


            //
            //  IF WAS HUMAN MOVE
            //
            //      ENGAGE AI
            //
            if (player_AIs[player_on_turn] == false)
            {
                engageAI();
            }


            //
            //  ADVANCE PLAYER ON TURN
            //
            player_on_turn = (player_on_turn + 1) % 2;


            //
            //  PLAY MODE BUTTON - INDICATE PLAYER ON TURN 
            //
            setPlayModeButtonImage();


            if (hasMovesAvailable())
            {
                history.addHistoryNode();

                nextTurn();
            }
            else
            {
                state = Enum_Types.states.end;

                winning_player_ID = player_on_turn;

                history.addHistoryNode();
            }
        }
    }



    //  #########
    //
    //  NEXT TURN 
    //
    //  #########
    //
    //public void nextTurn(bool advance_player_on_turn)
    public void nextTurn()
    {
        if (player_AIs[player_on_turn] == false)    //  HUMAN PLAYER ON TURN
        {
            //
            //  STATE = SOURCE TILES HIGHLIGHTED
            //
            state = Enum_Types.states.source_tiles_highlighted;


            //
            //  HIGHLIGHT SOURCE TILES
            //
            highlightSourceTiles();


            //
            //  ENGAGE AI
            //
            //engageAI();


            //
            //  IF NUMBER OF MOVES == 1  AND  PIE RULE IS ENABLED
            //
            //      SHOW PIE BUTTON
            //
            if (number_of_turns == 1 && is_pie_enabled)
            {
                showPieButton();
            }
            else
            {
                hidePieButton();
            }
        }
        else                                        //  AI PLAYER ON TURN 
        {
            if (is_AI_engaged)
            {
                ai_Script.takeTurn();
            }
            else
            {
                state = Enum_Types.states.AI_idle;
            }
        }
    }


    public bool hasMovesAvailable()
    {
        Enum_Types.colors player_color = player_colors[player_on_turn];


        Checker[] all_checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in all_checkers)
        {
            if (checker.player_ID == player_on_turn)
            {
                Tile tile_residence = checker.tile_residence;

                Tile[] next_tiles;

                if (player_color == Enum_Types.colors.red)
                {
                    next_tiles = tile_residence.NextTiles_red;
                }
                else
                {
                    next_tiles = tile_residence.NextTiles_blue;
                }


                foreach (Tile tile_cont in next_tiles)
                {
                    Checker resident_checker = tile_cont.resident_checker;

                    if (resident_checker == null)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }



    //
    //  SHOW PIE BUTTON 
    //
    public void showPieButton()
    {
        PieButton.SetActive(true);
    }



    //
    //  HIDE PIE BUTTON 
    //
    public void hidePieButton()
    {
        PieButton.SetActive(false);
    }



    //
    //  SET PLAY MODE BUTTON IMAGE 
    //
    public void setPlayModeButtonImage()
    {
        //
        //  GET PLAYER COLORS 
        //
        string p0_color;
        string p1_color;


        if (player_colors[0] == Enum_Types.colors.red)
        {
            p0_color = "R";     //  Red
            p1_color = "B";     //  Blue
        }
        else
        {
            p0_color = "B";     //  Blue
            p1_color = "R";     //  Red
        }


        switch (current_play_mode)
        {
            case Enum_Types.play_modes.human_vs_AI:

                Sprite sprite = Resources.Load<Sprite>("Play_Mode/Dodo_Hum_" + p0_color + "_AI_" + p1_color + "_Act_" + player_on_turn);

                PlayModeButton.image.sprite = sprite;

                break;


            case Enum_Types.play_modes.AI_vs_human:

                sprite = Resources.Load<Sprite>("Play_Mode/Dodo_AI_" + p0_color + "_Hum_" + p1_color + "_Act_" + player_on_turn);

                PlayModeButton.image.sprite = sprite;

                break;


            case Enum_Types.play_modes.human_vs_human:

                sprite = Resources.Load<Sprite>("Play_Mode/Dodo_Hum_" + p0_color + "_Hum_" + p1_color + "_Act_" + player_on_turn);

                PlayModeButton.image.sprite = sprite;

                break;


            case Enum_Types.play_modes.AI_vs_AI:

                sprite = Resources.Load<Sprite>("Play_Mode/Dodo_AI_" + p0_color + "_AI_" + p1_color + "_Act_" + player_on_turn);

                PlayModeButton.image.sprite = sprite;

                break;
        }
    }


    //
    //  HIGHLIGHT SOURCE TILES 
    //
    //  Determine color of player on turn.
    //
    //  For each tile
    //      If resident_checker belongs to player on turn 
    //          If any of the tiles it could move to are unoccupied 
    //              Set checker isSelectable = true
    //              Highlight the tile 
    //          
    //
    //public bool highlightSourceTiles()
    public void highlightSourceTiles()
    {
        //
        //  Determine color of player on turn.
        //
        Enum_Types.colors checker_color = player_colors[player_on_turn];


        Checker[] checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in checkers)
        {
            if (checker.player_ID == player_on_turn)
            {
                Tile tile_residence = checker.tile_residence;

                if (hasMoves(tile_residence, checker_color))
                {
                    checker.is_checker_selectable = true;

                    highlightTile(tile_residence);
                }
            }
        }
    }



    //
    //  HIGHLIGHT POTENTIAL DESTINATION TILES OF SELECTED CHECKER 
    //
    //  Set selected checker's isSelected = true
    //
    //  Determine selected checker's tile residence
    //
    //  Determine color of player on turn.
    //
    //  Set is_selectable_destination = true for selectable destination tiles
    //
    //  Highlight selectable destination tiles
    //          
    //
    public void highlightDestinationTiles(Checker selected_checker)
    {
        //
        //  SET CHECKER is_checker_selected = true
        //
        selected_checker.is_checker_selected = true;


        //
        //  DETERMINE CHECKER'S TILE RESIDENCE
        //
        Tile tile_residence = selected_checker.tile_residence;

        highlightTile(tile_residence);


        //
        //  HIGHLIGHT SELECTED CHECKER TILE AND SELECTABLE DESTINATION TILES
        //
        Tile[] next_tiles;

        Enum_Types.colors player_color = player_colors[player_on_turn];

        if (player_color == Enum_Types.colors.red)
        {
            next_tiles = tile_residence.NextTiles_red;
        }
        else
        {
            next_tiles = tile_residence.NextTiles_blue;
        }


        foreach (Tile tile_container in next_tiles)
        {
            Checker resident_checker = tile_container.resident_checker;

            if (resident_checker == null)
            {
                tile_container.is_selectable_destination = true;

                highlightTile(tile_container);
            }
        }
    }



    //
    //  UNHIGHLIGHT ALL TILES
    //  #####################
    //
    //      Set is_selectable_destination = false
    //      Set is_checker_selected = false
    //
    public void unhighlightAllTiles()
    {
        Tile[] tile_containers = GameObject.FindObjectsOfType<Tile>();

        foreach (Tile tile_container in tile_containers)
        {
            tile_container.is_selectable_destination = false;

            unhighlightTile(tile_container);

            Checker resident_checker = tile_container.resident_checker;

            if (resident_checker != null)
            {
                resident_checker.is_checker_selectable = false;

                resident_checker.is_checker_selected = false;
            }
        }
    }



    //
    //  CHECKER CLICKED
    //  ###############
    //
    //  Unhighlight all tiles
    //
    //  If clicked checker is a selectable source checker
    //      Highlight potential destination tiles 
    //
    //  Else 
    //      Highlight source tiles
    //
    public void checkerClicked(Checker clicked_checker)
    {
        bool is_checker_selectable = clicked_checker.is_checker_selectable;

        unhighlightAllTiles();

        if (is_checker_selectable)
        {
            //
            //  STATE = DESTINATION TILES HIGHLIGHTED
            //
            state = Enum_Types.states.destination_tiles_highlighted;
            
            highlightDestinationTiles(clicked_checker);
        }
        else
        {
            //
            //  STATE = SOURCE TILES HIGHLIGHTED
            //
            state = Enum_Types.states.source_tiles_highlighted;

            highlightSourceTiles();
        }
    }



    //
    //  TILE CLICKED 
    //  ############
    //
    //  If clicked tile is selectable destination 
    //      Move the checker
    //
    //  Else 
    //      Unhighlight all tiles   
    //      Highlight source tiles
    //
    public void tileClicked(Tile clicked_tile)
    {
        bool is_selectable_destination = clicked_tile.is_selectable_destination;

        if (is_selectable_destination)
        {
            //
            //  ############
            //
            //  MOVE CHECKER 
            //
            //  ############
            //
            //  ##########################################################################
            //  ##########################################################################
            //
            //  I think checkers continue to drift after they were supposedly settled.  So don't make was_turn_completed 
            //  dependent on checkers settling.  Instead, in checker, set is_turn_starting = true, and after small delay,
            //  checker sets was_turn_completed to true in GameManager
            //
            //  ##########################################################################
            //  ##########################################################################
            //

            //
            //  STATE = CHECKER MOVING
            //
            state = Enum_Types.states.checker_moving;


            //
            // FIND THE SELECTED CHECKER
            //
            Checker selected_checker = new Checker();


            Checker[] all_checkers = GameObject.FindObjectsOfType<Checker>();

            foreach (Checker checker in all_checkers)
            {
                bool is_checker_selected = checker.is_checker_selected;

                if (is_checker_selected == true)
                {
                    selected_checker = checker;

                    break;
                }
            }


            //
            //  UNSET RESIDENT CHECKER ON PREVIOUS CLICKED TILE
            //
            Tile prev_tile_residence = selected_checker.tile_residence;

            prev_tile_residence.resident_checker = null;

            //
            //  SET RESIDENT CHECKER ON CLICKED TILE
            //
            clicked_tile.resident_checker = selected_checker;

            //
            //  SET TILE RESIDENCE ON MOVABLE CHECKER TO CLICKED TILE
            //
            selected_checker.tile_residence = clicked_tile;

            //
            //  UNSELECT SELECTED CHECKER
            //
            selected_checker.is_checker_selected = false;


            //
            //  UNHIGHLIGHT ALL TILES
            //
            unhighlightAllTiles();


            //
            //  MOVE CHECKER 
            //
            selected_checker.is_checker_moving = true;

            selected_checker.target_tile = clicked_tile;
        }
        else
        {
            //
            //  STATE = SOURCE TILES HIGHLIGHTED
            //
            state = Enum_Types.states.source_tiles_highlighted;

            unhighlightAllTiles();

            highlightSourceTiles();
        }
    }



    //
    //  HAS MOVES
    //
    //  Does the tile_container (occupied by a checker) have any forward moves available?
    //
    bool hasMoves(Tile tile_container, Enum_Types.colors player_color)
    {
        Tile[] next_tiles;

        if ( player_color == Enum_Types.colors.red )
        {
            next_tiles = tile_container.NextTiles_red;
        }
        else
        {
            next_tiles = tile_container.NextTiles_blue;
        }

        //Debug.Log("TILE CONTAINER: " + tile_container);


        foreach (Tile tile_cont in next_tiles)
        {
            //Debug.Log("next tile: " + tile_cont);

            Checker resident_checker = tile_cont.resident_checker;

            if (resident_checker == null)
            {
                return true;
            }
        }

        return false;
    }


    void highlightTile(Tile tile_container)
    {
        GameObject inner_tile = tile_container.transform.GetChild(0).gameObject; 

        inner_tile.GetComponent<MeshRenderer>().material = highlighted_tile_material;
    }


    void unhighlightTile(Tile tile_container)
    {
        GameObject inner_tile = tile_container.transform.GetChild(0).gameObject;

        inner_tile.GetComponent<MeshRenderer>().material = tile_material;
    }


    public void engageAI()
    {
        //Debug.Log("Engage AI");

        is_AI_engaged = true;
    }


    public void disengageAI()
    {
        //Debug.Log("Disengage AI");

        is_AI_engaged = false;
    }



    public void setCheckersPlayerID()
    {
        Checker[] checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in checkers)
        {
            //
            //  SET PLAYER IDs
            //
            string checker_name = checker.name;

            string[] syllables = checker_name.Split('_');

            if (syllables[0] == "Red")                          //  RED CHECKER
            {
                if (player_colors[0] == Enum_Types.colors.red)  //      PLAYER 0 IS RED
                {
                    checker.player_ID = 0;
                }
                else                                            //      PLAYER 1 IS RED
                {
                    checker.player_ID = 1;
                }
            }
            else                                                //  BLUE CHECKER
            {
                if (player_colors[0] == Enum_Types.colors.blue) //      PLAYER 0 IS BLUE
                {
                    checker.player_ID = 0;
                }
                else                                            //      PLAYER 1 IS BLUE
                {
                    checker.player_ID = 1;
                }
            }
        }
    }
}
