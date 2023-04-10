public class Enum_Types
//public class Enum_Types : MonoBehaviour
{
    //
    //  GAME MODES 
    //
    public enum play_modes
    {
        human_vs_AI,
        AI_vs_human,
        human_vs_human,
        AI_vs_AI
    }


    //
    //  GAME STATES 
    //
    public enum states
    {
        initial,
        source_tiles_highlighted,
        destination_tiles_highlighted,
        AI_idle,
        AI_thinking,
        checker_moving,
        end
    }


    //
    //  COLORS 
    //
    public enum colors
    {
        red,
        blue
    }
}
