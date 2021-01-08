using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using BBCore.Conditions;
using BBUnity;
using BBUnity.Actions;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyS;
    public GameObject enemyM;
    public GameObject enemyL;
    public int Currency;
    public BehaviorExecutor AI;
    private int WaveNo = 0;
    public Button StartWaveButton;


    public String TowerType;
    public List<GameObject> s;
    
    // Start is called before the first frame update
    void Start()
    {
        // SpawnL();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * begins the wave on the start button being pressed by adding an amount to the currency
     */
    public void StartWave()
    {
        StartWaveButton.gameObject.SetActive(false);
        WaveNo++;
        Currency = 10 + WaveNo;
    }
    
    /*
     * spawns the Small Enemy
     */
    public void SpawnS()
    {
        Instantiate(enemyS, transform.GetChild(0).position, Quaternion.identity);
    }
    /*
     * spawns the Medium Enemy
     */
    public void SpawnM()
    {
        Instantiate(enemyM, transform.GetChild(1).position, Quaternion.identity);
    }
    /*
     * spawns the Large Enemy
     */
    public void SpawnL()
    {
        Instantiate(enemyL, transform.GetChild(2).position, Quaternion.identity);
    }
}
