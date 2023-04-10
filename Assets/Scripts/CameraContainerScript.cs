using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainerScript : MonoBehaviour
{
    public GameManager gameManager;

    //float revolveAngle = 0; // RED SIDE 0, BLUE SIDE 180

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    float x_angle = -25f;

    float targetRevolveAngle;

    float revolveVelocity;

    float smooth_time = .18f;

    // Update is called once per frame
    void Update()
    {
        int player_on_turn = gameManager.player_on_turn;

        bool[] player_AIs = gameManager.player_AIs;

        bool is_player_human = ! player_AIs[player_on_turn];

        if (is_player_human
            && gameManager.playing_backward_many == false
            && gameManager.playing_forward_many == false)  //  DON'T REVOLVE CAMERA IF IN MIDDLE OF FAST REPLAY
        {
            float revolveAngle = this.transform.rotation.eulerAngles.y;

            Enum_Types.colors[] player_colors = gameManager.player_colors;

            Enum_Types.colors player_color = player_colors[player_on_turn];

            if (player_color == Enum_Types.colors.red)
            {
                targetRevolveAngle = 0;
            }
            else
            {
                targetRevolveAngle = 180;
            }

            revolveAngle = Mathf.SmoothDamp(revolveAngle, 
                                            targetRevolveAngle, 
                                            ref revolveVelocity, 
                                            smooth_time);

            this.transform.rotation = Quaternion.Euler(new Vector3(x_angle, revolveAngle, 0));
        }
    }
}
