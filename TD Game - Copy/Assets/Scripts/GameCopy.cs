using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCopy : MonoBehaviour
{
    
    
    public GameObject Spawn;
    
    public List<GameObject> Towers;

    public List<GameObject> LargeEnemies;
    public List<GameObject> MedEnemies;
    public List<GameObject> SmallEnemies;

    public List<GameObject> totEnemies;
    public int furthest;

    public List<int> furthestList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * resets the game copy so that it can be used in the next simulation
     */
    public void Reset()
    {
        Towers = new List<GameObject>();
        LargeEnemies = new List<GameObject>();
        MedEnemies = new List<GameObject>();
        SmallEnemies = new List<GameObject>();
        totEnemies = new List<GameObject>();
        furthest = 0;
    }

    /*
     * adds the enemies to the enemies lists
     */
    public void startWave(List<GameObject> enemies)
    {
        findTowers();
        totEnemies = enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = Spawn.transform.position;
            if (enemies[i].transform.name.Contains("FakeL"))
            {
                LargeEnemies.Add(enemies[i]);
            }
            else if(enemies[i].transform.name.Contains("FakeM"))
            {
                MedEnemies.Add(enemies[i]);
            } else if (enemies[i].transform.name.Contains("FakeS"))
            {
                SmallEnemies.Add(enemies[i]);
            }
        }
    }
    
    
    /*
     * finds the towers to towers list
     */
    public void findTowers()
    {
        Towers.AddRange(GameObject.FindGameObjectsWithTag("SingleTarget"));
        Towers.AddRange(GameObject.FindGameObjectsWithTag("AOE"));
    }
    
}
