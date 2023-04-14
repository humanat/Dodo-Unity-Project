using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HistoryScript : MonoBehaviour
{
    public GameManager gameManager;

    public HistoryScript history; 
    
    public LinkedList<Position> positions = new LinkedList<Position>();

    public LinkedListNode<Position> replay_node;


    float replay_time_period = .35f;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        initializePositions();
    }


    public void initializePositions()
    {
        positions.Clear();

        Position position = getLivePosition();

        positions.AddFirst(position);

        replay_node = positions.First;
    }


    // Update is called once per frame
    void Update()
    {

    }


    //  ################
    //
    //  ADD HISTORY NODE
    //
    //  ################
    //
    //  IF REPLAY NODE NOT LAST NODE 
    //
    //      REMOVE ALL NODES AFTER REPLAY NODE
    //      
    //  ADD HISTORY NODE TO END
    //
    public void addHistoryNode()
    {
        while (replay_node != positions.Last)
        {
            positions.RemoveLast();
        }

        positions.AddLast(getLivePosition());

        replay_node = positions.Last;
    }


    //
    //  JUMP TO BEGINNING
    //
    public void jumpToBeginning()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = false;

        gameManager.unhighlightAllTiles();
        
        replay_node = positions.First;

        copyReplayNodeToBoard();
    }


    //
    //  BACK MANY - REPLAY MANY NODES BACKWARD
    //
    public void backMany()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_forward_many = false;

        gameManager.playing_backward_many = true;

        gameManager.unhighlightAllTiles();

        StartCoroutine(replayNodesBackward(replay_time_period));
    }


    //
    //  BACK MANY - REPLAY MANY NODES BACKWARD - COROUTINE
    //
    IEnumerator replayNodesBackward(float replay_time_period)
    {
        while (gameManager.playing_backward_many == true)
        {
            yield return new WaitForSeconds(replay_time_period);

            if (gameManager.playing_backward_many == true)
            {
                if (replay_node.Previous != null)   //  IF REPLAY NODE NOT ALREADY THE FIRST NODE
                {
                    replay_node = replay_node.Previous;

                    if (replay_node.Previous == null)
                    {
                        gameManager.playing_backward_many = false;
                    }

                    copyReplayNodeToBoard();
                }
            }
        }
    }


    //
    //  BACK ONE
    //
    public void backOne()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = false;

        gameManager.unhighlightAllTiles();

        if (replay_node.Previous != null)   //  IF REPLAY NODE NOT ALREADY THE FIRST NODE
        {
            replay_node = replay_node.Previous;
        }

        copyReplayNodeToBoard();
    }


    //
    //  STOP
    //
    public void stop()
    {
        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = false;
    }


    //
    //  PLAY ONE
    //
    public void playOne()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = false;

        gameManager.unhighlightAllTiles();

        if (replay_node.Next != null)   //  IF REPLAY NODE NOT ALREADY THE LAST NODE
        {
            replay_node = replay_node.Next;
        }

        copyReplayNodeToBoard();
    }


    //
    //  PLAY MANY
    //
    public void playMany()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = true;

        gameManager.unhighlightAllTiles();

        StartCoroutine(replayNodesForward(replay_time_period));
    }


    //
    //  REPLAY MANY NODES FORWARD
    //
    IEnumerator replayNodesForward(float replay_time_period)
    {
        while (gameManager.playing_forward_many == true)
        {
            yield return new WaitForSeconds(replay_time_period);

            if (gameManager.playing_forward_many == true)
            {
                if (replay_node.Next != null)   //  IF REPLAY NODE NOT ALREADY THE LAST NODE
                {
                    replay_node = replay_node.Next;

                    if (replay_node.Next == null)
                    {
                        gameManager.playing_forward_many = false;
                    }

                    copyReplayNodeToBoard();
                }
            }
        }
    }


    //
    //  JUMP TO END
    //

    public void jumpToEnd()
    {
        //
        //  DISENGAGE AI 
        //
        gameManager.disengageAI();


        gameManager.playing_backward_many = false;

        gameManager.playing_forward_many = false;

        gameManager.unhighlightAllTiles();

        replay_node = positions.Last;

        copyReplayNodeToBoard();
    }


    //
    //  GET CURRENT POSITION
    //
    public Position getLivePosition()
    {
        Position position = new Position();

        position.state = gameManager.state;


        position.player_on_turn = gameManager.player_on_turn;

        position.number_of_turns = gameManager.number_of_turns;

        position.is_pie_enabled = gameManager.is_pie_enabled;


        position.current_play_mode = gameManager.current_play_mode;

        position.player_AIs = gameManager.player_AIs;


        position.current_AI_level = gameManager.current_AI_level;



        position.player_colors = gameManager.player_colors;


        position.winning_player_ID = gameManager.winning_player_ID;


        position.tiles_data = new List<Position.Tile_Data>();

        position.checkers_data = new List<Position.Checker_Data>();


        Tile[] tiles = GameObject.FindObjectsOfType<Tile>();

        foreach (Tile tile in tiles)
        {
            Position.Tile_Data tile_data = new Position.Tile_Data();

            tile_data.tile_name = tile.name;

            tile_data.resident_checker = tile.resident_checker;

            position.tiles_data.Add(tile_data);
        }


        Checker[] checkers = GameObject.FindObjectsOfType<Checker>();

        foreach (Checker checker in checkers)
        {
            Position.Checker_Data checker_data = new Position.Checker_Data();

            checker_data.checker_name = checker.name;

            checker_data.tile_residence = checker.tile_residence;

            checker_data.target_tile = checker.tile_residence;  // TARGET TILE INITIALLY NULL.  USE TILE RESIDENCE INSTEAD.

            position.checkers_data.Add(checker_data);
        }

        return position;
    }


    public void copyReplayNodeToBoard()
    {
        Position replay_position = replay_node.Value;


        gameManager.unhighlightAllTiles();


        //
        //  VARIABLES
        //
        gameManager.state = replay_position.state;


        gameManager.player_on_turn = replay_position.player_on_turn;

        gameManager.number_of_turns = replay_position.number_of_turns;

        gameManager.is_pie_enabled = replay_position.is_pie_enabled;


        gameManager.current_play_mode = replay_position.current_play_mode;

        gameManager.player_AIs = replay_position.player_AIs;


        gameManager.current_AI_level = replay_position.current_AI_level;


        gameManager.player_colors = replay_position.player_colors;


        gameManager.winning_player_ID = replay_position.winning_player_ID;



        //
        //  BUTTONS 
        //
        gameManager.setPlayModeButtonImage();



        //
        //  TILES
        //
        List<Position.Tile_Data> position_tiles_data = replay_position.tiles_data;

        Position.Tile_Data[] position_tiles_data_array = position_tiles_data.ToArray();

        foreach (Position.Tile_Data position_tile_data in position_tiles_data_array)
        {
            string position_tile_name = position_tile_data.tile_name;

            Tile tile = GameObject.Find(position_tile_name).GetComponent<Tile>();

            tile.resident_checker = position_tile_data.resident_checker;
        }


        //
        //  CHECKERS
        //
        List<Position.Checker_Data> position_checkers_data = replay_position.checkers_data;

        Position.Checker_Data[] position_checkers_data_array = position_checkers_data.ToArray();

        foreach (Position.Checker_Data position_checker_data in position_checkers_data_array)
        {
            string position_checker_name = position_checker_data.checker_name;

            Checker checker = GameObject.Find(position_checker_name).GetComponent<Checker>();

            checker.target_tile = position_checker_data.target_tile;

            checker.tile_residence = position_checker_data.tile_residence;

            checker.transform.position = checker.tile_residence.transform.position;
        }



        //
        //  IF GAME HAS NOT ENDED
        //
        //      HIGHLIGHT TILES OR SHOW AI PLAY BUTTON 
        //
        if (gameManager.state != Enum_Types.states.end)
        {
            if (gameManager.player_AIs[gameManager.player_on_turn] == false)    //  HUMAN PLAYER
            {
                gameManager.state = Enum_Types.states.source_tiles_highlighted;

                gameManager.highlightSourceTiles();
            }
            else                                                                //  AI PLAYER
            {
                gameManager.state = Enum_Types.states.AI_idle;
            }
        }
    }
}
