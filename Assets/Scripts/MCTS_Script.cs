using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    public Enum_Types.colors[] player_colors;

    //public Enum_Types.colors player_color;

    public int number_of_turns;

    public bool is_pie_enabled;

    public bool is_pie_move;

    //
    //  SOURCE CELL, DESTINATION CELL
    //
    //  For children of the root, keep track of the move that got to this position.
    //  Can be used to recreate checker-to-tile move.
    //
    public int[ ] source_cell;

    public int[ ] dest_cell;


    //
    //  CONSTRUCTOR FOR NEW, BLANK POSITION
    //
    public MCTS_Position()
    {
        
    }


    //
    //  CONSTRUCTOR TO COPY ANOTHER POSITION
    //
    public MCTS_Position(MCTS_Position position)
    {
        board = position.board.Clone() as int?[,];

        player_on_turn = position.player_on_turn;

        player_colors = position.player_colors;

        number_of_turns = position.number_of_turns;

        is_pie_enabled = position.is_pie_enabled;

        //source_cell = position.source_cell.Clone() as int[];  //  COPIED POSITION ONLY USED IN SIMULATED PLAYOUT

        //dest_cell = position.dest_cell.Clone() as int[];      //  SOURCE CELL AND DESTINATION CELL AREN'T NECESSARY IN SIMULATED PLAYOUT
    }


    //
    //  INITIALIZE ROOT POSITION 
    //
    public void initializeRootPosition()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //
        //  INITIALIZE BOARD
        //
        board = new int?[7, 7];

        Tile[] tile_containers = GameObject.FindObjectsOfType<Tile>();

        foreach (Tile tile_container in tile_containers)
        {
            string tile_container_name = tile_container.name;

            string[] syllables = tile_container_name.Split('_');

            int u = int.Parse(syllables[2]);

            int v = int.Parse(syllables[3]);


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
        }


        //
        //  INITIALIZE VARIABLES
        //
        player_on_turn = gameManager.player_on_turn;

        player_colors = gameManager.player_colors;

        //player_color = gameManager.player_colors[player_on_turn];

        number_of_turns = gameManager.number_of_turns;

        is_pie_enabled = gameManager.is_pie_enabled;

        is_pie_move = false;
    }


    public List<MCTS_Position> getLegalPositions()
    {
        //Debug.Log("get legal positions  ########################################################");


        List<MCTS_Position> legalPositions = new List<MCTS_Position>();


        Enum_Types.colors player_color = player_colors[player_on_turn];

 
        for (int u = 0; u <= 6; u++)
        {
            for (int v = lowerLimit(u); v <= upperLimit(u); v++)
            {
                if (board[u, v] == player_on_turn)
                {
                    //Debug.Log(u + ", " + v + " player on turn " + player_on_turn);

                    if (player_color == Enum_Types.colors.red)  //  RED PLAYER
                    {
                        if (isUnoccupiedCell(u - 1, v))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u - 1, v);

                            legalPositions.Add(legal_position);
                        }

                        if (isUnoccupiedCell(u - 1, v + 1))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u - 1, v + 1);

                            legalPositions.Add(legal_position);
                        }

                        if (isUnoccupiedCell(u, v + 1))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u, v + 1);

                            legalPositions.Add(legal_position);
                        }
                    }
                    else                                        //  BLUE PLAYER
                    {
                        if (isUnoccupiedCell(u + 1, v))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u + 1, v);

                            legalPositions.Add(legal_position);
                        }

                        if (isUnoccupiedCell(u + 1, v - 1))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u + 1, v - 1);

                            legalPositions.Add(legal_position);
                        }

                        if (isUnoccupiedCell(u, v - 1))
                        {
                            MCTS_Position legal_position = createPosition(u, v, u, v - 1);

                            legalPositions.Add(legal_position);
                        }
                    }
                }
            }
        }

        return legalPositions;
    }



    public MCTS_Position getRandomNextPosition()
    {
        List<MCTS_Position> legalPositions = getLegalPositions();

        return legalPositions[Random.Range(0, legalPositions.Count)];
    }



    MCTS_Position createPosition(int source_u, int source_v, int dest_u, int dest_v)
    {
        MCTS_Position new_position = new MCTS_Position();

        new_position.board = board.Clone() as int?[ , ];

        new_position.board[source_u, source_v] = null;          //  VACATE SOURCE CELL

        new_position.board[dest_u, dest_v] = player_on_turn;    //  OCCUPY DEST CELL


        new_position.player_on_turn = (player_on_turn + 1) % 2; //  ADVANCE PLAYER ON TURN

        new_position.player_colors = player_colors;     // Colors will reverse if pie move

        new_position.number_of_turns = number_of_turns + 1;     // INCREMENT NUMBER OF TURNS

        new_position.is_pie_enabled = is_pie_enabled;

        new_position.is_pie_move = is_pie_move;


        new_position.source_cell = new int[] { source_u, source_v };

        new_position.dest_cell = new int[] { dest_u, dest_v };

        //new_position.showPosition();

        return new_position;
    }



    public void showPosition()
    {
        Debug.Log("Position  #################################");


        for (int u = 0; u <= 6; u++)
        {
            for (int v = lowerLimit(u); v <= upperLimit(u); v++)
            {
                Debug.Log(u + ", " + v + ": " + board[u, v]);
            }
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
            //Debug.Log("is terminal ############################");

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

        for (int u = 0; u <= 6; u++)
        {
            for (int v = lowerLimit(u); v <= upperLimit(u); v++)
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

        Enum_Types.colors player_color = player_colors[player_on_turn];

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
//  #########
//
//  MCTS NODE 
//
//  #########
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

        visits = 1;
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
    //int max_iterations = 1;
    //int max_iterations = 100;
    //int max_iterations = 500;
    //int max_iterations = 1000;
    int max_iterations = 5000;
    //int max_iterations = 10000;
    //int max_iterations = 100000;

    //GameManager gameManager;

    public MCTS_Position findBestMovePosition(MCTS_Position MCTS_root_pos)
    {
        MCTS_Node root_node = new MCTS_Node(MCTS_root_pos, null);


        for (int i = 0; i < max_iterations; i++)
        {
            //Debug.Log("iteration: " + i);


            MCTS_Node selected_node = selectNode(root_node);

            //selected_node.MCTS_position.showPosition();

            //selected_node.MCTS_position.getLegalPositions();

            MCTS_Node child_node = expandNodeAndSelectChild(selected_node);

            int winning_player = simulate(child_node);
            bool win = winning_player == root_node.player_on_turn;

            backPropagate(child_node, win);
        }




        return getBestChild(root_node).MCTS_position;





        //return MCTS_root_pos;
    }



    private MCTS_Node selectNode (MCTS_Node root_node)
    {
        MCTS_Node node = root_node;

        float tunable_bias_parameter = Mathf.Sqrt(2f);

        while (node.children.Count > 0)
        {
            float bestScore = float.MinValue;

            MCTS_Node bestChild = null;

            foreach (MCTS_Node child in node.children)
            {
                float UCB = 0f;
                if (node.MCTS_position.player_on_turn == root_node.player_on_turn)
                {
                    UCB = (float)child.wins / (float)child.visits;
                }
                else
                {
                    UCB = ((float)child.visits - (float)child.wins - 1f) / (float)child.visits;
                }
                UCB += tunable_bias_parameter * Mathf.Sqrt(Mathf.Log((float)node.visits) / (float)child.visits);

                if (UCB > bestScore)
                {
                    bestScore = UCB;

                    bestChild = child;
                }
            }

            node = bestChild;
        }

        return node;
    }


    private MCTS_Node expandNodeAndSelectChild(MCTS_Node node)
    {
        // ManaT: Needs to handle terminal case
        if (node.MCTS_position.isTerminal()) return node;

        List<MCTS_Position> positions = node.MCTS_position.getLegalPositions();

        foreach (MCTS_Position position in positions)
        {
            MCTS_Node child = new MCTS_Node(position, node);

            node.children.Add(child);
        }

        return node.children[Random.Range(0, node.children.Count)];
    }


    private int simulate(MCTS_Node node)
    {
        //Debug.Log("simulate #####################################");

        MCTS_Position current_position = new MCTS_Position (node.MCTS_position);

        //currentPosition.showPosition();

        while ( ! current_position.isTerminal() )
        {
            current_position = current_position.getRandomNextPosition();
        }

        // IF POSITION IS TERMINAL, PLAYER ON TURN IS THE WINNER
        return current_position.player_on_turn;
    }



    private void backPropagate(MCTS_Node node, bool win)
    {
        while (node != null)
        {
            node.visits++;
            if (win) node.wins++;
            node = node.parent;
        }
    }


    private MCTS_Node getBestChild(MCTS_Node rootNode)
    {
        int maxVisits = -1;

        MCTS_Node bestChild = null;

        foreach (MCTS_Node child in rootNode.children)
        {
            if (child.visits > maxVisits)
            {
                maxVisits = child.visits;

                bestChild = child;
            }
        }

        return bestChild;
    }



    /*
    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    */


}
