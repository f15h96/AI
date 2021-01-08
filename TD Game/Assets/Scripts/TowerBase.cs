using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    private Color startcolor;
    public GameObject TowerUi;
    public Manager Manager;
    public bool hasTower;
    public GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * changes the colour of the tower base if the tower base gets clicked and sets the managers highlighted tower base to the one that has been clicked
     */
    private void OnMouseDown()
    {
        startcolor = GetComponent<Renderer>().material.color;
        if (Manager.HighlghtedGameObject == null) 
        {
        } else if (Manager.HighlghtedGameObject != gameObject)
        {
            Manager.HighlghtedGameObject.GetComponent<Renderer>().material.color = startcolor;
        }
        Manager.HighlghtedGameObject = gameObject;
        GetComponent<Renderer>().material.color = Color.yellow;
        TowerUi.SetActive(true);
    }
    
}
