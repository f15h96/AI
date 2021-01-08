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
    
    /*
     * Finds the waypoints gameobject to use for the upadate method
     */
    void Start()
    {
        Waypoints = GameObject.Find("Waypoints");
    }

    /*
     * gets the next child of the waypoints and makes the enemy move towards it changing to the next one when it reaches it
     * also destroys the enemy when the health reaches 0
     */
    void Update()
    {
        Transform Waypoint = Waypoints.transform.GetChild(waypointNumber);
        Vector3 newPos = Vector3.MoveTowards(transform.position, Waypoint.position, Speed);
        transform.position = newPos;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
