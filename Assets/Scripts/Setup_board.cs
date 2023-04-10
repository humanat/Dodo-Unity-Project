using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Setup_board : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Setup_Board();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Setup_Board()
    {
        GameObject Red_checker_0 = GameObject.Find("Red-checker-container_0");
        GameObject Red_checker_1 = GameObject.Find("Red-checker-container_1");
        GameObject Red_checker_2 = GameObject.Find("Red-checker-container_2");
        GameObject Red_checker_3 = GameObject.Find("Red-checker-container_3");
        GameObject Red_checker_4 = GameObject.Find("Red-checker-container_4");
        GameObject Red_checker_5 = GameObject.Find("Red-checker-container_5");
        GameObject Red_checker_6 = GameObject.Find("Red-checker-container_6");
        GameObject Red_checker_7 = GameObject.Find("Red-checker-container_7");
        GameObject Red_checker_8 = GameObject.Find("Red-checker-container_8");
        GameObject Red_checker_9 = GameObject.Find("Red-checker-container_9");
        GameObject Red_checker_10 = GameObject.Find("Red-checker-container_10");
        GameObject Red_checker_11 = GameObject.Find("Red-checker-container_11");
        GameObject Red_checker_12 = GameObject.Find("Red-checker-container_12");

        GameObject Blue_checker_0 = GameObject.Find("Blue-checker-container_0");
        GameObject Blue_checker_1 = GameObject.Find("Blue-checker-container_1");
        GameObject Blue_checker_2 = GameObject.Find("Blue-checker-container_2");
        GameObject Blue_checker_3 = GameObject.Find("Blue-checker-container_3");
        GameObject Blue_checker_4 = GameObject.Find("Blue-checker-container_4");
        GameObject Blue_checker_5 = GameObject.Find("Blue-checker-container_5");
        GameObject Blue_checker_6 = GameObject.Find("Blue-checker-container_6");
        GameObject Blue_checker_7 = GameObject.Find("Blue-checker-container_7");
        GameObject Blue_checker_8 = GameObject.Find("Blue-checker-container_8");
        GameObject Blue_checker_9 = GameObject.Find("Blue-checker-container_9");
        GameObject Blue_checker_10 = GameObject.Find("Blue-checker-container_10");
        GameObject Blue_checker_11 = GameObject.Find("Blue-checker-container_11");
        GameObject Blue_checker_12 = GameObject.Find("Blue-checker-container_12");

        Red_checker_0.transform.position = getTilePosition(3, 0);
        Red_checker_1.transform.position = getTilePosition(3, 1);
        Red_checker_2.transform.position = getTilePosition(4, 0);
        Red_checker_3.transform.position = getTilePosition(4, 1);
        Red_checker_4.transform.position = getTilePosition(4, 2);
        Red_checker_5.transform.position = getTilePosition(5, 0);
        Red_checker_6.transform.position = getTilePosition(5, 1);
        Red_checker_7.transform.position = getTilePosition(5, 2);
        Red_checker_8.transform.position = getTilePosition(5, 3);
        Red_checker_9.transform.position = getTilePosition(6, 0);
        Red_checker_10.transform.position = getTilePosition(6, 1);
        Red_checker_11.transform.position = getTilePosition(6, 2);
        Red_checker_12.transform.position = getTilePosition(6, 3);

        Blue_checker_0.transform.position = getTilePosition(0, 3);
        Blue_checker_1.transform.position = getTilePosition(0, 4);
        Blue_checker_2.transform.position = getTilePosition(0, 5);
        Blue_checker_3.transform.position = getTilePosition(0, 6);
        Blue_checker_4.transform.position = getTilePosition(1, 3);
        Blue_checker_5.transform.position = getTilePosition(1, 4);
        Blue_checker_6.transform.position = getTilePosition(1, 5);
        Blue_checker_7.transform.position = getTilePosition(1, 6);
        Blue_checker_8.transform.position = getTilePosition(2, 4);
        Blue_checker_9.transform.position = getTilePosition(2, 5);
        Blue_checker_10.transform.position = getTilePosition(2, 6);
        Blue_checker_11.transform.position = getTilePosition(3, 5);
        Blue_checker_12.transform.position = getTilePosition(3, 6);
    }

    Vector3 getTilePosition (int u, int v)
    {
        GameObject tile = GameObject.Find("Tile-container_" + u + "_" + v);

        return tile.transform.position;
    }
}
