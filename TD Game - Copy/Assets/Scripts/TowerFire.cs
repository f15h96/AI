using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerFire : MonoBehaviour
{
    private float time;

    public List<GameObject> towers;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*
     * if the enemy enters the tower radius then it adds it to the towers list 
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            towers.Add(other.transform.parent.gameObject);
        }
    }

    /*
     * if the enemy leaves the tower radius then it removes it to the towers list
     */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            towers.Remove(other.transform.parent.gameObject);
        }
    }

    /*
     * if an enemy is in the radius then the towers fire at the enemies 
     */
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.CompareTag("AOE"))
        {
            time = time + Time.deltaTime;
            if (time > .5f)
            {
                if (towers[0] == null)
                {
                    towers.Remove(towers[0]);
                }
                other.transform.parent.gameObject.GetComponent<Enemy>().Health--;
                time = 0f;
            }
        }
        else if (gameObject.CompareTag("SingleTarget"))
        {
            time = time + Time.deltaTime;
            if (time > 1f)
            {
                if (towers[0] == null)
                {
                    towers.Remove(towers[0]);
                }
                towers[0].transform.GetComponent<Enemy>().Health =
                    towers[0].transform.GetComponent<Enemy>().Health - 2;
                time = 0;
            }
        }
    }
}
