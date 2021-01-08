using System;
using System.Collections.Generic;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    ///It is an action to find a GameObject by its name.
    /// </summary>
    [Action("GameObject/FindTowers")]
    [Help("Finds the main towers")]
    public class FindByNames : GOAction
    {
        
        public List<GameObject> AOETowers = new List<GameObject>();

        public List<GameObject> SingleTowers = new List<GameObject>();
        
        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("MainTT")] [Help("List of main towers")]
        public List<GameObject> MainTowers;
        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("TowerType")] [Help("Name of tower type")]
        public String TowerName;

        /// <summary>Initialization Method of FindByNames.</summary>
        /// <remarks>Find the GameObject with the given name.</remarks>
        public override void OnStart()
        { 
            AOETowers.AddRange(GameObject.FindGameObjectsWithTag("AOE"));
            SingleTowers.AddRange(GameObject.FindGameObjectsWithTag("SingleTarget"));
            if (AOETowers.Count > SingleTowers.Count)
            {
                MainTowers = AOETowers;
                TowerName = "AOETowers";
            }
            else
            {
                MainTowers = SingleTowers;
                TowerName = "SingleTarget";
            }

        }

        /// <summary>Method of Update of FindByNames.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}