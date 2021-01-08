using Pada1.BBCore;
using Pada1.BBCore.Framework;
using UnityEngine;


namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the AOE tower is more prominent in the scene
    /// </summary>
    [Condition("Perception/AOETowerCheck")]
    [Help("Checks which tower type more prominent in the scene")]
    public class AOETowerCheck : ConditionBase
    {
        ///<value>Input Name of the target game object Parameter.</value>
        [InParam("TowerType")]
        [Help("The name of the TowerType")]
        public string TowerType;
        
        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
        {
            if (TowerType.Equals("AOETowers"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}