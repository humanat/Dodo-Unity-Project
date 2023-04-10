using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //
    // TILE 
    //      THIS TILE - Set in Unity editor
    //
    public Tile tile;

    //
    // RESIDENT CHECKER 
    //      The checker that currently resides on this tile, if any
    //
    public Checker resident_checker;

    //
    //  IS SELECTABLE DESTINATION
    //
    public bool is_selectable_destination = false;


    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    //
    //  Next tiles a stone could move to from current tile, both from Red's and Blue's perspective 
    //
    public Tile[] NextTiles_red;

    public Tile[] NextTiles_blue;


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        gameManager.tileClicked(tile);
    }
}
