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
     * checks to see if there is any enemies in the scene if not it will allow the user to start the next wave
     * also makes the game end if the player Hp reaches 0
     */
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
     * adds a single target to the tower base if there is no tower already there
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
     * starts the wave from the enemy spawn script
     */
    public void StartWave()
    {
       spawn.StartWave();
    }
    
    /*
    * adds a AOE target to the tower base if there is no tower already there
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
     * removes the tower from the tower base
     */
    public void RemoveTower()
    {
        towerBase = HighlghtedGameObject.GetComponent<TowerBase>();
        Destroy(towerBase.tower);
        towerBase.hasTower = false;
    }
}
