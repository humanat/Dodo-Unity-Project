using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//
//  ########
//  
//  POSITION
//
//  ########
//  

public class MCTS_Position 
{
    GameManager gameManager;
    
    //
    //  BOARD
    //
    //  Populated by 0s, 1s, and nulls.
    //
    public int?[ , ] board;

    public int player_on_turn;

    public Enum_Types.colors player_color;

    public int number_of_turns;

    public bool is_pie_enabled;


    //
    //  SOURCE CELL, DESTINATION CELL
    //
    //  For children of the root, keep track of the move that got to this position.
    //  Can be used to recreate checker-to-tile move.
    //
    int[ , ] source_cell;

    int[ , ] destination_cell;

    public MCTS_Position()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        int?[ , ] board;

        player_on_turn = gameManager.player_on_turn;

        player_color = gameManager.player_colors[player_on_turn];


        number_of_turns = gameManager.number_of_turns;

        is_pie_enabled = gameManager.is_pie_enabled;

        bool is_pie_move = false;
    }



    //
    //  INITIALIZE BOARD 
    //
    public void initializeBoard()
    {
        board = new int?[7, 7];

        Tile[] tile_containers = GameObject.FindObjectsOfType<Tile>();

        foreach (Tile tile_container in tile_containers)
        {
            string tile_container_name = tile_container.name;

            string[] syllables = tile_container_name.Split('_');

            int u = int.Parse(syllables[2]);

            int v = int.Parse(syllables[3]);

            //Debug.Log(u + ", " + v);


            Checker resident_checker = tile_container.resident_checker;

            if (resident_checker == null)
            {
                board[u, v] = null;
            }
            else
            {
                int player_ID = resident_checker.player_ID;

                board[u, v] = player_ID;
            }

            //Debug.Log(u + ", " + v + " " + board[u, v]);
        }
    }


    public bool isTerminal()
    {
        if (hasMoves())
        {
            return false;
        }
        else
        { 
            return true; 
        }
    }


    //
    //  Upper left corner is 0,3
    //
    //  Lower left corner is 3,0
    //
    public bool hasMoves()
    {
        //Debug.Log("has moves?");

        for (int u = 0; u < 7; u++)
        {
            for (int v = lowerLimit(u); v < upperLimit(u); v++)
            {
                if (board[u, v] == player_on_turn)
                {
                    if (hasMove(u, v))
                    {
                        //Debug.Log(u + ", " + v + " has move");

                        return true;
                    }
                }
            }
        }

        return false;
    }

    public bool hasMove(int u, int v)
    {
        //Debug.Log("has move?");

        if (player_color == Enum_Types.colors.red)  //  RED PLAYER
        {
            if (isUnoccupiedCell(u - 1, v) ||
                isUnoccupiedCell(u - 1, v + 1) ||
                isUnoccupiedCell(u, v + 1))
            { 
                return true; 
            }
        }
        else                                        //  BLUE PLAYER
        {
            if (isUnoccupiedCell(u + 1, v) ||
                isUnoccupiedCell(u + 1, v - 1) ||
                isUnoccupiedCell(u, v - 1))
            {
                return true;
            }
        }

        return false;
    }


    public bool isUnoccupiedCell(int u, int v)
    {
        if (isOnBoard(u, v))
        {
            if (board[u, v] == null)
            {
                return true;
            }
        }

        return false;
    }


    public bool isOnBoard(int u, int v)
    {
        if ((u >= 0) && (u <= 3))
        {
            if (v >= 3 - u && v <=6) 
            { 
                return true;
            }
        }
        else if ((u >= 4) && (u <= 6))
        {
            if (v >= 0 && v <= 9 - u)
            {
                return true;
            }
        }

        return false;
    }


    public int lowerLimit(int u)
    {
        if (u <= 3)
        {
            return 3 - u;
        }
        else
        {
            return 0;
        }
    }


    public int upperLimit(int u)
    {
        if (u <= 3)
        {
            return 6;
        }
        else
        {
            return 9 - u;
        }
    }

}



//
//  ####
//
//  NODE 
//
//  ####
//
public class MCTS_Node
{
    public MCTS_Position MCTS_position;

    public MCTS_Node parent;

    public List<MCTS_Node> children;

    public int wins;

    public int visits;

    public MCTS_Node(MCTS_Position MCTS_pos, MCTS_Node par)
    {
        MCTS_position = MCTS_pos;

        parent = par;

        children = new List<MCTS_Node>();

        wins = 0;

        visits = 0;
    }
}



//
//  ###########
//  
//  MCTS SCRIPT
//
//  ###########
//
public class MCTS_Script : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public MCTS_Position findBestMovePosition(MCTS_Position MCTS_pos)
    {
        return MCTS_pos;
    }



}
