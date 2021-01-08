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
    [Action("GameObject/SpawnEnemies")]
    [Help("Finds the main towers")]
    public class SpawnEnemies : GOAction
    {
        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("Formation")] [Help("formation of enemies")]
        public String Formation;

        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("LargeEnemies")] [Help("Amount of medium Enemy Tokens")]
        public int LargeEnemies;

        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("MedEnemies")] [Help("Amount of medium Enemy Tokens")]
        public int MedEnemies;

        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("SmallEnemies")] [Help("Amount of small Enemy Tokens")]
        public int SmallEnemies;

        private int delay = 1000;
        private int elapsed = 0;

        /// <summary>Initialization Method of FindByName.</summary>
        /// <remarks>Find the GameObject with the given name.</remarks>
        public override void OnStart()
        {
            /* if (Formation == "Group")
             {
                 while (SmallEnemies > 0)
                 {
                     gameObject.GetComponent<EnemySpawn>().SpawnS();
                     SmallEnemies--;
                 }
 
                 while (MedEnemies > 0)
                 {
                     gameObject.GetComponent<EnemySpawn>().SpawnM();
                     MedEnemies--;
                 }
 
                 while (LargeEnemies > 0)
                 {
                     gameObject.GetComponent<EnemySpawn>().SpawnL();
                     LargeEnemies--;
                 }
 
             }
             else if (Formation == "Delayed")
             {
                 while (LargeEnemies > 0 || MedEnemies > 0 || SmallEnemies > 0)
                 {
                     Timer += Time.deltaTime;
                     if (Timer > 4f)
                     {
                         Timer = 0f;
                         if (LargeEnemies > 0)
                         {
                             gameObject.GetComponent<EnemySpawn>().SpawnL();
                             LargeEnemies--;
                         }
                         else if (MedEnemies > 0)
                         {
                             gameObject.GetComponent<EnemySpawn>().SpawnM();
                             MedEnemies--;
                         }
                         else if (SmallEnemies > 0)
                         {
                             gameObject.GetComponent<EnemySpawn>().SpawnS();
                             SmallEnemies--;
                         }
                     }
                 }
             }*/
        }


        /// <summary>Method of Update of FindByName.</summary>
        /// <remarks>Complete the task.</remarks>
        public override TaskStatus OnUpdate()
        {
            switch (Formation)
            {
                case "Group":
                {
                    while (SmallEnemies > 0)
                    {
                        gameObject.GetComponent<EnemySpawn>().SpawnS();
                        SmallEnemies--;
                        return TaskStatus.RUNNING;
                    }

                    while (MedEnemies > 0)
                    {
                        gameObject.GetComponent<EnemySpawn>().SpawnM();
                        MedEnemies--;
                        return TaskStatus.RUNNING;
                    }

                    while (LargeEnemies > 0)
                    {
                        gameObject.GetComponent<EnemySpawn>().SpawnL();
                        LargeEnemies--;
                        return TaskStatus.RUNNING;
                    }

                    break;
                }
                case "Delayed":
                {
                    while (LargeEnemies > 0 || MedEnemies > 0 || SmallEnemies > 0)
                    {
                        if (delay > 0)
                        {
                            ++elapsed;
                            elapsed %= delay;
                            if (elapsed != 0)
                                return TaskStatus.RUNNING;
                        }

                        if (LargeEnemies > 0)
                        {
                            gameObject.GetComponent<EnemySpawn>().SpawnL();
                            LargeEnemies--;
                            return TaskStatus.RUNNING;
                        }
                        else if (MedEnemies > 0)
                        {
                            gameObject.GetComponent<EnemySpawn>().SpawnM();
                            MedEnemies--;
                            return TaskStatus.RUNNING;
                        }
                        else if (SmallEnemies > 0)
                        {
                            gameObject.GetComponent<EnemySpawn>().SpawnS();
                            SmallEnemies--;
                            return TaskStatus.RUNNING;
                        }
                    }
                }

                    break;
            }

            return TaskStatus.COMPLETED;
        }
    }
}