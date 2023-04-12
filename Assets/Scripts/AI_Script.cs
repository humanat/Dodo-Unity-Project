using UnityEngine;
using System.Collections.Generic;

public class AI_Script
{
    GameManager gameManager;

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


    public AI_Script()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }


    //
    //  TAKE TURN
    //  #########
    //
    //  Determine color of player on turn 
    //
    //  Get list of movable checkers
    //
    //
    public void takeTurn()
    {
        Move[] possible_moves = getPossibleMoves();


        Move selected_move = SelectMove(possible_moves);

        //
        //  STATE = CHECKER MOVING
        //
        gameManager.state = Enum_Types.states.checker_moving;

        //
        //  MOVE THE CHECKER
        //
        makeMove(selected_move);
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




        //move.move_checker.is_move_starting = true;
    }


    Move SelectMove(Move[] possible_moves)
    {
        for (int i = 0; i < 50000; ++i)
        {
            for (int j = 0; j < 100000; ++j)
            {

            }
        }

        return possible_moves[Random.Range(0, possible_moves.Length)];
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
