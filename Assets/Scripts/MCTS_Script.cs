using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class MCTS_Position
{
    int[][] board;

    int player_on_turn;

    int player_color;

    int number_of_turns;

    bool is_pie_enabled;
}



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


public class MCTS_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
