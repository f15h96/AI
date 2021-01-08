using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /*
     * prevents the enemies from hitting into each other so that they dont go off course
     */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy") {
            Physics.IgnoreCollision(other.collider, gameObject.transform.GetChild(0).GetComponent<Collider>());
        }
    }
}
