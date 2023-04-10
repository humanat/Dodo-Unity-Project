using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFlagScript : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject WinFlagRed_0;
    public GameObject WinFlagRed_1;
    public GameObject WinFlagBlue_0;
    public GameObject WinFlagBlue_1;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.winning_player_ID != null)
        {
            int winning_player_ID = (int) gameManager.winning_player_ID;

            Enum_Types.colors[] player_colors = gameManager.player_colors;

            Enum_Types.colors player_color = player_colors[winning_player_ID];

            if (player_color == Enum_Types.colors.red)
            {
                WinFlagRed_0.SetActive(true);
                WinFlagRed_1.SetActive(true);
            }
            else
            {
                WinFlagBlue_0.SetActive(true);
                WinFlagBlue_1.SetActive(true);
            }
        }
        else
        {
            WinFlagRed_0.SetActive(false);
            WinFlagRed_1.SetActive(false);
            WinFlagBlue_0.SetActive(false);
            WinFlagBlue_1.SetActive(false);
        }
    }
}
