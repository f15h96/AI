using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class NeighbourStrategyFactory
    {
        Dictionary<string, Type> strategies;

        public NeighbourStrategyFactory()
        {
            LoadTypesIFindNeighbourStrategy();
        }

        private void LoadTypesIFindNeighbourStrategy()
        {
            strategies = new Dictionary<string, Type>();
            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(IFindNeighbourStrategy).ToString())!= null)
                {
                    strategies.Add(type.Name.ToLower(), type);
                }
            }
        }

        internal IFindNeighbourStrategy CreateInstance(string strategyName)
        {
            Type t = GetTypeToCreate(strategyName);
            if (t == null)
            {
                t = GetTypeToCreate("more");
            }
            return Activator.CreateInstance(t) as IFindNeighbourStrategy;
        }

        private Type GetTypeToCreate(string strategyName)
        {
            foreach (var PossibleStrategy in strategies)
            {
                if (PossibleStrategy.Key.Contains(strategyName))
                {
                    return PossibleStrategy.Value;
                }
            }

            return null;
        }
    }

}
