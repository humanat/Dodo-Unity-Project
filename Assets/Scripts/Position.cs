using System.Collections.Generic;
using static Position;


public class Position
{

    public class Tile_Data
    {
        public string tile_name;

        public Checker resident_checker;
    }


    public class Checker_Data
    {
        public string checker_name;

        public Tile tile_residence;

        public Tile target_tile;
    }

    public List<Tile_Data> tiles_data;

    public List<Checker_Data> checkers_data;



    public Enum_Types.states state;


    public int player_on_turn;

    public int number_of_turns;

    public bool is_pie_enabled;


    public Enum_Types.play_modes current_play_mode;

    public bool[] player_AIs;


    public int current_AI_level;


    public Enum_Types.colors[] player_colors;


    public int? winning_player_ID;
 
}
