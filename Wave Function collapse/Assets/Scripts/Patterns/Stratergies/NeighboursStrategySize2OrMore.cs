using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class NeighboursStrategySize2OrMore : IFindNeighbourStrategy
    {
        public Dictionary<int, PatternNeighbours> FindNeighbours(PatternDataResults patternFinderResult)
        {
            Dictionary<int, PatternNeighbours> result = new Dictionary<int, PatternNeighbours>();
            foreach (var patternData in patternFinderResult.PatternIndexDictionary)
            {
                foreach (var possibleNeighboursForPattern in patternFinderResult.PatternIndexDictionary)
                {
                    FindNeighboursInAllDirections(result, patternData, possibleNeighboursForPattern);
                }
            }

            return result;
        }

        private void FindNeighboursInAllDirections(Dictionary<int, PatternNeighbours> result, KeyValuePair<int, PatternData> patternData, KeyValuePair<int, PatternData> possibleNeighboursForPattern)
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (patternData.Value.CompareGrid(dir, possibleNeighboursForPattern.Value))
                {
                    if (result.ContainsKey(patternData.Key) == false)
                    {
                        result.Add(patternData.Key, new PatternNeighbours());
                    }
                    result[patternData.Key].AddPatternToDictionary(dir, possibleNeighboursForPattern.Key);
                }
            }
        }
        
    }


}

