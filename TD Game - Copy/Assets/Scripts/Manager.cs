using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject HighlghtedGameObject;

    private Vector3 TowerPos;

    public GameObject SingleTower;
    public GameObject AOETower;

    public int HP = 100;
    private TowerBase towerBase;

    public EnemySpawn spawn;

    public Button NextWaveButton;

    public Image GameOver;

    public int Wave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     * removes the start wave button and ends the game
     */
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("Enemy") == false)
        {
            NextWaveButton.gameObject.SetActive(true);
        }

        if (HP <= 0)
        {
            GameOver.gameObject.SetActive(true);
        }
    }
    
    /*
     * adds a single target tower to a towerbase
     */
    public void SingleTarget()
    {
        towerBase = HighlghtedGameObject.GetComponent<TowerBase>();
        if (towerBase.hasTower == false)
        {
            TowerPos = HighlghtedGameObject.transform.position;
            towerBase.tower = Instantiate(SingleTower, TowerPos, Quaternion.identity);
            towerBase.hasTower = true;
        }
    }

    /*
     * starts the wave
     */
    public void StartWave()
    {
        spawn.StartWave();
    }
    
    /*
     * adds an AOE tower to the tower base
     */
    public void AOE()
    {
        towerBase = HighlghtedGameObject.GetComponent<TowerBase>();
            if (towerBase.hasTower == false)
        {
            TowerPos = HighlghtedGameObject.transform.position;
            towerBase.tower = Instantiate(AOETower, TowerPos, Quaternion.identity);
            towerBase.hasTower = true;
        }

    }

    /*
     * restarts the game
     */
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /*
     * removes a tower if it is on the towerbase
     */
    public void RemoveTower()
    {
        towerBase = HighlghtedGameObject.GetComponent<TowerBase>();
        Destroy(towerBase.tower);
        towerBase.hasTower = false;
    }
}
