using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//
//  MONTE CARLO TREE SEARCH 
//
//  If number_of_turns == 0  AND is_pie_enabled 
//      Make average value move
//  
//  If number_of_turns == 1  AND is_pie_enabled
//      Make the mirror of the root node one of its child nodes and mark it as the pie move
//      If it turns out to be the best move
//          Execute pie
//
//
//
//
//
//
//
//
//
//
//
public class AI_Script : MonoBehaviour
{
    //
    //  MCTS
    //
    public MCTS_Script MCTS_Algorithm;

    public MCTS_Position current_position;

    public MCTS_Position best_move_position;


    //
    //  MOVE
    //
    public struct Move
    {
        public Move(Checker checker, Tile tile)
        {
            move_checker = checker;
            move_tile = tile;
        }

        public Checker move_checker { get; }

        public Tile move_tile { get; }
    }


    GameManager gameManager;

    public GameObject AI_Thinking_Image;

    bool was_AI_move_selected = false;

    Move selected_AI_move;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (was_AI_move_selected)
        {
            was_AI_move_selected = false;

            AI_Thinking_Image.SetActive(false);

            if (gameManager.is_AI_engaged)  //  AI MIGHT HAVE BEEN DISENGAGED BY A RECORDER BUTTON PRESS
            {
                makeMove(selected_AI_move);
            }
        }
    }


    public void takeTurn()
    {
        Move[] possible_moves = getPossibleMoves();

        StartCoroutine(SelectMove(possible_moves));
    }


    //
    //  SELECT MOVE
    //
    IEnumerator SelectMove(Move[] possible_moves)
    {
        AI_Thinking_Image.SetActive(true);

        //Debug.Log("AI thinking start");

        gameManager.state = Enum_Types.states.AI_thinking;

        yield return null;



        //
        //  ###############
        //
        //  MCTS EVALUATION
        //
        //  ###############
        //
        current_position = new MCTS_Position();

        current_position.initializeBoard();

        best_move_position = MCTS_Algorithm.findBestMovePosition(current_position);


        best_move_position.isTerminal();




        //Debug.Log("color: " + best_move_position.player_color);

        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < 100000; j++)
            {

            }
        }


        yield return null;


        selected_AI_move = possible_moves[Random.Range(0, possible_moves.Length)];

        was_AI_move_selected = true;

        //Debug.Log("AI move selected");
    }

    void makeMove(Move move)
    {
        //
        //  UNSET RESIDENT CHECKER ON PREVIOUS TILE RESIDENCE
        //
        Tile prev_tile_residence = move.move_checker.tile_residence;

        prev_tile_residence.resident_checker = null;


        //
        //  SET RESIDENT CHECKER ON MOVE TILE
        //
        move.move_tile.resident_checker = move.move_checker;


        //
        //  SET TILE RESIDENCE ON MOVE CHECKER TO MOVE TILE
        //
        move.move_checker.tile_residence = move.move_tile;


        //
        //  SET TARGET TILE
        //
        move.move_checker.target_tile = move.move_tile;


        //
        //  START MOVE
        //
        move.move_checker.is_checker_moving = true;

        gameManager.state = Enum_Types.states.checker_moving;
    }


    Move[] getPossibleMoves()
    {
        List<Move> possibleMoves = new List<Move>();

        //
        //  Determine color of player on turn.
        //
        Enum_Types.colors player_color = gameManager.player_colors[gameManager.player_on_turn];


        Checker[] all_checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in all_checkers)
        {
            if (checker.player_ID == gameManager.player_on_turn)
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
                        possibleMoves.Add(new Move(checker, tile_cont));
                    }
                }
            }
        }

        return possibleMoves.ToArray();
    }
}
