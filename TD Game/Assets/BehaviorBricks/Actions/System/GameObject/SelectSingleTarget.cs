﻿using System;
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
    [Action("GameObject/SelectSingleTargets")]
    [Help("Finds the main towers")]
    public class SelectSingleTargets : GOAction
    {
        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("TowerType")]
        [Help("The name of the TowerType")]
        public string TowerType;

        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("Formation")] [Help("Formation of the towers")]
        public String Formation;
        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("LargeEnemies")] [Help("number of large enemies to spawn")]
        public int LargeEnemies;
        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("MedEnemies")] [Help("number of medium enemies to spawn")]
        public int MedEnemies;
        ///<value>OutPut Found game object Parameter.</value>
        [OutParam("SmallEnemies")] [Help("number of small enemies to spawn")]
        public int SmallEnemies;

        private float elapsedTime;

        private EnemySpawn spawnScript;
        /// <summary>Initialization Method of FindByName.</summary>
        /// <remarks>Find the GameObject with the given name.</remarks>
        public override void OnStart()
        {
            spawnScript = this.gameObject.GetComponent<EnemySpawn>();
            if (LargeEnemies > 0 || MedEnemies > 0 || SmallEnemies > 0)
            {
            }
            else
            { 
                while (spawnScript.Currency != 0)
                {
                    if (SmallEnemies > 5 && spawnScript.Currency > 5)
                    {
                        MedEnemies++;
                        spawnScript.Currency -= 3;
                    }
                    else
                    {
                        SmallEnemies++;
                        spawnScript.Currency--;
                    }
                }
                Formation = "Group";
            }
        }

        /// <summary>Method of Update of FindByName.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}