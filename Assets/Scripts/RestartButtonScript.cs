using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButtonScript : MonoBehaviour
{
    public GameManager gameManager;

    public Checker Red_checker_container_0;
    public Checker Red_checker_container_1;
    public Checker Red_checker_container_2;
    public Checker Red_checker_container_3;
    public Checker Red_checker_container_4;
    public Checker Red_checker_container_5;
    public Checker Red_checker_container_6;
    public Checker Red_checker_container_7;
    public Checker Red_checker_container_8;
    public Checker Red_checker_container_9;
    public Checker Red_checker_container_10;
    public Checker Red_checker_container_11;
    public Checker Red_checker_container_12;

    public Checker Blue_checker_container_0;
    public Checker Blue_checker_container_1;
    public Checker Blue_checker_container_2;
    public Checker Blue_checker_container_3;
    public Checker Blue_checker_container_4;
    public Checker Blue_checker_container_5;
    public Checker Blue_checker_container_6;
    public Checker Blue_checker_container_7;
    public Checker Blue_checker_container_8;
    public Checker Blue_checker_container_9;
    public Checker Blue_checker_container_10;
    public Checker Blue_checker_container_11;
    public Checker Blue_checker_container_12;

    public Tile Tile_container_0_3;
    public Tile Tile_container_0_4;
    public Tile Tile_container_0_5;
    public Tile Tile_container_0_6;
    public Tile Tile_container_1_2;
    public Tile Tile_container_1_3;
    public Tile Tile_container_1_4;
    public Tile Tile_container_1_5;
    public Tile Tile_container_1_6;
    public Tile Tile_container_2_1;
    public Tile Tile_container_2_2;
    public Tile Tile_container_2_3;
    public Tile Tile_container_2_4;
    public Tile Tile_container_2_5;
    public Tile Tile_container_2_6;
    public Tile Tile_container_3_0;
    public Tile Tile_container_3_1;
    public Tile Tile_container_3_2;
    public Tile Tile_container_3_3;
    public Tile Tile_container_3_4;
    public Tile Tile_container_3_5;
    public Tile Tile_container_3_6;
    public Tile Tile_container_4_0;
    public Tile Tile_container_4_1;
    public Tile Tile_container_4_2;
    public Tile Tile_container_4_3;
    public Tile Tile_container_4_4;
    public Tile Tile_container_4_5;
    public Tile Tile_container_5_0;
    public Tile Tile_container_5_1;
    public Tile Tile_container_5_2;
    public Tile Tile_container_5_3;
    public Tile Tile_container_5_4;
    public Tile Tile_container_6_0;
    public Tile Tile_container_6_1;
    public Tile Tile_container_6_2;
    public Tile Tile_container_6_3;


    //
    //  PLAY MODE
    //
    public Button PlayModeButton;

    public GameObject PlayModePanel;


    //
    //  AI LEVEL
    //
    public Button AI_LevelButton;

    public GameObject AI_LevelPanel;


    //
    //  MENU
    //
    public Button MenuButton;

    public GameObject MenuPanel;

    public Button MenuPieButton;

    public HistoryScript history;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        history = GameObject.FindObjectOfType<HistoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()
    {
        gameManager.unhighlightAllTiles();

        
        gameManager.state = Enum_Types.states.initial;

        gameManager.player_on_turn = 0;

        gameManager.number_of_turns = 0;

        gameManager.is_pie_enabled = false;


        //
        //  PLAY MODE BUTTON
        //
        Sprite sprite = Resources.Load<Sprite>("Play_Mode/Dodo_Hum_R_AI_B_Act_0");
        PlayModeButton.image.sprite = sprite;

        gameManager.current_play_mode = Enum_Types.play_modes.human_vs_AI;


        //
        //  PLAY MODE PANEL
        //
        PlayModePanel.SetActive(false);
        PlayModeButton.GetComponent<PlayModeButtonScript>().is_showing_pm_panel = false;


        //
        //  AI LEVEL BUTTON
        //
        sprite = Resources.Load<Sprite>("AI_Level/Dodo_AI_Level_1");
        AI_LevelButton.image.sprite = sprite;

        gameManager.current_AI_level = 1;


        //
        //  AI LEVEL PANEL
        //
        AI_LevelPanel.SetActive(false);
        AI_LevelButton.GetComponent<AI_LevelButtonScript>().is_showing_AI_level_panel = false;


        //
        //  MENU PANEL
        //
        MenuPanel.SetActive(false);
        MenuButton.GetComponent<MenuButtonScript>().is_showing_menu_panel = false;


        //
        //  MENU PIE
        //
        sprite = Resources.Load<Sprite>("Menu/Dodo_Pie_Gray");
        MenuPieButton.image.sprite = sprite;


        //
        //  PIE
        //
        gameManager.PieButton.SetActive(false);



        //
        //  VARIABLES
        //
        gameManager.player_colors = new Enum_Types.colors[] { Enum_Types.colors.red, Enum_Types.colors.blue };

        gameManager.was_turn_completed = false;

        gameManager.winning_player_ID = null;

        gameManager.current_AI_level = 1;

        gameManager.player_AIs = new bool[] { false, true };


        Tile[] tiles = GameObject.FindObjectsOfType<Tile>();


        foreach (Tile tile in tiles)
        {
            tile.resident_checker = null;
        }


        Red_checker_container_0.transform.position = Tile_container_3_0.transform.position;
        Red_checker_container_0.tile_residence = Tile_container_3_0;
        Red_checker_container_0.target_tile = Tile_container_3_0;
        Tile_container_3_0.resident_checker = Red_checker_container_0;

        Red_checker_container_1.transform.position = Tile_container_3_1.transform.position;
        Red_checker_container_1.tile_residence = Tile_container_3_1;
        Red_checker_container_1.target_tile = Tile_container_3_1;
        Tile_container_3_1.resident_checker = Red_checker_container_1;

        Red_checker_container_2.transform.position = Tile_container_4_0.transform.position;
        Red_checker_container_2.tile_residence = Tile_container_4_0;
        Red_checker_container_2.target_tile = Tile_container_4_0;
        Tile_container_4_0.resident_checker = Red_checker_container_2;

        Red_checker_container_3.transform.position = Tile_container_4_1.transform.position;
        Red_checker_container_3.tile_residence = Tile_container_4_1;
        Red_checker_container_3.target_tile = Tile_container_4_1;
        Tile_container_4_1.resident_checker = Red_checker_container_3;

        Red_checker_container_4.transform.position = Tile_container_4_2.transform.position;
        Red_checker_container_4.tile_residence = Tile_container_4_2;
        Red_checker_container_4.target_tile = Tile_container_4_2;
        Tile_container_4_2.resident_checker = Red_checker_container_4;

        Red_checker_container_5.transform.position = Tile_container_5_0.transform.position;
        Red_checker_container_5.tile_residence = Tile_container_5_0;
        Red_checker_container_5.target_tile = Tile_container_5_0;
        Tile_container_5_0.resident_checker = Red_checker_container_5;

        Red_checker_container_6.transform.position = Tile_container_5_1.transform.position;
        Red_checker_container_6.tile_residence = Tile_container_5_1;
        Red_checker_container_6.target_tile = Tile_container_5_1;
        Tile_container_5_1.resident_checker = Red_checker_container_6;

        Red_checker_container_7.transform.position = Tile_container_5_2.transform.position;
        Red_checker_container_7.tile_residence = Tile_container_5_2;
        Red_checker_container_7.target_tile = Tile_container_5_2;
        Tile_container_5_2.resident_checker = Red_checker_container_7;

        Red_checker_container_8.transform.position = Tile_container_5_3.transform.position;
        Red_checker_container_8.tile_residence = Tile_container_5_3;
        Red_checker_container_8.target_tile = Tile_container_5_3;
        Tile_container_5_3.resident_checker = Red_checker_container_8;

        Red_checker_container_9.transform.position = Tile_container_6_0.transform.position;
        Red_checker_container_9.tile_residence = Tile_container_6_0;
        Red_checker_container_9.target_tile = Tile_container_6_0;
        Tile_container_6_0.resident_checker = Red_checker_container_9;

        Red_checker_container_10.transform.position = Tile_container_6_1.transform.position;
        Red_checker_container_10.tile_residence = Tile_container_6_1;
        Red_checker_container_10.target_tile = Tile_container_6_1;
        Tile_container_6_1.resident_checker = Red_checker_container_10;

        Red_checker_container_11.transform.position = Tile_container_6_2.transform.position;
        Red_checker_container_11.tile_residence = Tile_container_6_2;
        Red_checker_container_11.target_tile = Tile_container_6_2;
        Tile_container_6_2.resident_checker = Red_checker_container_11;

        Red_checker_container_12.transform.position = Tile_container_6_3.transform.position;
        Red_checker_container_12.tile_residence = Tile_container_6_3;
        Red_checker_container_12.target_tile = Tile_container_6_3;
        Tile_container_6_3.resident_checker = Red_checker_container_12;


        Blue_checker_container_0.transform.position = Tile_container_0_3.transform.position;
        Blue_checker_container_0.tile_residence = Tile_container_0_3;
        Blue_checker_container_0.target_tile = Tile_container_0_3;
        Tile_container_0_3.resident_checker = Blue_checker_container_0;

        Blue_checker_container_1.transform.position = Tile_container_0_4.transform.position;
        Blue_checker_container_1.tile_residence = Tile_container_0_4;
        Blue_checker_container_1.target_tile = Tile_container_0_4;
        Tile_container_0_4.resident_checker = Blue_checker_container_1;

        Blue_checker_container_2.transform.position = Tile_container_0_5.transform.position;
        Blue_checker_container_2.tile_residence = Tile_container_0_5;
        Blue_checker_container_2.target_tile = Tile_container_0_5;
        Tile_container_0_5.resident_checker = Blue_checker_container_2;

        Blue_checker_container_3.transform.position = Tile_container_0_6.transform.position;
        Blue_checker_container_3.tile_residence = Tile_container_0_6;
        Blue_checker_container_3.target_tile = Tile_container_0_6;
        Tile_container_0_6.resident_checker = Blue_checker_container_3;

        Blue_checker_container_4.transform.position = Tile_container_1_3.transform.position;
        Blue_checker_container_4.tile_residence = Tile_container_1_3;
        Blue_checker_container_4.target_tile = Tile_container_1_3;
        Tile_container_1_3.resident_checker = Blue_checker_container_4;

        Blue_checker_container_5.transform.position = Tile_container_1_4.transform.position;
        Blue_checker_container_5.tile_residence = Tile_container_1_4;
        Blue_checker_container_5.target_tile = Tile_container_1_4;
        Tile_container_1_4.resident_checker = Blue_checker_container_5;

        Blue_checker_container_6.transform.position = Tile_container_1_5.transform.position;
        Blue_checker_container_6.tile_residence = Tile_container_1_5;
        Blue_checker_container_6.target_tile = Tile_container_1_5;
        Tile_container_1_5.resident_checker = Blue_checker_container_6;

        Blue_checker_container_7.transform.position = Tile_container_1_6.transform.position;
        Blue_checker_container_7.tile_residence = Tile_container_1_6;
        Blue_checker_container_7.target_tile = Tile_container_1_6;
        Tile_container_1_6.resident_checker = Blue_checker_container_7;

        Blue_checker_container_8.transform.position = Tile_container_2_4.transform.position;
        Blue_checker_container_8.tile_residence = Tile_container_2_4;
        Blue_checker_container_8.target_tile = Tile_container_2_4;
        Tile_container_2_4.resident_checker = Blue_checker_container_8;

        Blue_checker_container_9.transform.position = Tile_container_2_5.transform.position;
        Blue_checker_container_9.tile_residence = Tile_container_2_5;
        Blue_checker_container_9.target_tile = Tile_container_2_5;
        Tile_container_2_5.resident_checker = Blue_checker_container_9;

        Blue_checker_container_10.transform.position = Tile_container_2_6.transform.position;
        Blue_checker_container_10.tile_residence = Tile_container_2_6;
        Blue_checker_container_10.target_tile = Tile_container_2_6;
        Tile_container_2_6.resident_checker = Blue_checker_container_10;

        Blue_checker_container_11.transform.position = Tile_container_3_5.transform.position;
        Blue_checker_container_11.tile_residence = Tile_container_3_5;
        Blue_checker_container_11.target_tile = Tile_container_3_5;
        Tile_container_3_5.resident_checker = Blue_checker_container_11;

        Blue_checker_container_12.transform.position = Tile_container_3_6.transform.position;
        Blue_checker_container_12.tile_residence = Tile_container_3_6;
        Blue_checker_container_12.target_tile = Tile_container_3_6;
        Tile_container_3_6.resident_checker = Blue_checker_container_12;


        //
        //  STATE = SOURCE TILES HIGHLIGHTED
        //
        gameManager.state = Enum_Types.states.source_tiles_highlighted;

        gameManager.highlightSourceTiles();


        //
        //  HISTORY
        //
        history.initializePositions();
    }
}
