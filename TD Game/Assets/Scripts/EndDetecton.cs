using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDetecton : MonoBehaviour
{

    public Manager Manager;
    public Text HpText;

    private int enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*
     * lowers the amount of hp the player has depending on the enemy that reaches the end of the maze and removes the enemy
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.name.Contains("S"))
            {
                enemy = 1;
            }else if (other.name.Contains("M"))
            {
                enemy = 2;
            }else if (other.name.Contains("L"))
            {
                enemy = 3;
            }
            switch (enemy)
            {
                case 1:
                    Manager.HP --;
                    break;
                case 2:
                    Manager.HP -= 3;
                    break;
                case 3:
                    Manager.HP -= 5;
                    break;

            }

            HpText.text = "HP:" + Manager.HP;
            Destroy(other.gameObject);
        }
    }
}
