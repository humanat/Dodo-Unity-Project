using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public GameManager gameManager;

    //
    // CHECKER 
    //      THIS CHECKER - Set in Unity editor
    //
    public Checker checker;


    //
    //  PLAYER ID 
    //      ID of player who owns this checker.  0 or 1.
    //
    public int player_ID;


    //
    //  TILE RESIDENCE
    //
    //      Tile this checker currently resides at - set in Unity editor
    //
    public Tile tile_residence;


    public bool is_checker_selectable;

    public bool is_checker_selected;


    //
    //  TARGET TILE TO MOVE CHECKER TO
    //
    public Tile target_tile;

    Vector3 velocity = Vector3.zero;


    //
    //  FAST MOVE
    //
    float smooth_time = .1f;
    float turn_completed_time = .1f;

    //
    //  SLOW MOVE
    //
    //float smooth_time = .3f;
    //float turn_completed_time = .5f;


    //
    //  IS TURN STARTING 
    //
    //  I think checkers continue to drift after they were supposedly settled.  So don't make GameManager.was_turn_completed 
    //  dependent on checkers settling.  Instead, have GameManager set is_move_starting = true here.  Once detected in Update,
    //  set is_move_starting to false right away, and after a short delay, set was_turn_completed to true in GameManager.
    //
    public bool is_move_starting;

    //
    //  ALLOWABLE DIST FROM TARG
    //
    //  Allowable distance when moving checker to target position,
    //  so it can stop moving instead of endlessly ooching microscopically closer to the target position
    //
    float allowable_dist_from_targ = .01f;




    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        //
        //  PLACE CHECKERS WHERE THEY GO INITIALLY
        //
        transform.position = tile_residence.transform.position;


        //
        //  SET TARGET TILE TO TILE RESIDENCE
        //
        target_tile = tile_residence;
    }


    // Update is called once per frame
    void Update()
    {
        if (is_move_starting == true)
        {
            is_move_starting = false;

            StartCoroutine(markTurnCompleted(turn_completed_time));
        }

        // 
        //  MOVE CHECKER CLOSER TO TARGET TILE POSITION 
        //
        if (    Mathf.Abs(  (transform.position - target_tile.transform.position).magnitude  ) > allowable_dist_from_targ    )
        {
            transform.position = Vector3.SmoothDamp(transform.position, target_tile.transform.position, ref velocity, smooth_time);
        }
    }


    //
    //  DELAY BEFORE MARKING TURN COMPLETED IN GAME MANAGER
    //
    IEnumerator markTurnCompleted(float turn_completed_time)
    {
        yield return new WaitForSeconds(turn_completed_time);

        gameManager.was_turn_completed = true;
    }


    void OnMouseDown()
    {
        gameManager.checkerClicked(checker);
    }
}
