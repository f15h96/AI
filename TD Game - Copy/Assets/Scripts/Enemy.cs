using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public GameObject Waypoints;
    public float Speed;
    public int waypointNumber = 0;
    public GameCopy GameCopy;
    
    // Start is called before the first frame update
    void Start()
    {
        Waypoints = GameObject.Find("Waypoints");
        GameCopy = GameObject.Find("GameManager").GetComponent<GameCopy>();
    }

    /*
     * makes the enemies move around the enviroment
     */
    void Update()
    {
        Transform Waypoint = Waypoints.transform.GetChild(waypointNumber);
        Vector3 newPos = Vector3.MoveTowards(transform.position, Waypoint.position, Speed);
        transform.position = newPos;
        if (Health <= 0)
        {
            GameCopy.furthestList.Add(waypointNumber);
            Destroy(gameObject);
        }
    }
    
}
