using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class CoreHelper
    {
        private float totalFrequency = 0;
        private float totalFrequencyLog = 0;
        private PatternManager patternManager;

        public CoreHelper(PatternManager manager)
        {
            patternManager = manager;

          /* for (int i = 0; i < patternManager.GetNumberOfPatterns(); i++)
            {
                totalFrequency += patternManager.getPatternFrequency(i);
                
            }

            totalFrequencyLog = Mathf.Log(totalFrequency, 2);*/
        }

        public int SelectSolutionPatternFromFrequency(List<int> possibleValues)
        {
            List<float> ValueFrequenciesFractions = GetListOfWeightsFromFrequencies(possibleValues);
            float randomValues = Random.Range(0, ValueFrequenciesFractions.Sum());
            float sum = 0;
            int index = 0;
            foreach (var item in ValueFrequenciesFractions)
            {
                sum += item;
                if (randomValues <= sum)
                {
                    return index;
                }

                index++;
            }

            return index;
        }

        private List<float> GetListOfWeightsFromFrequencies(List<int> possibleValues)
        {
            var valueFrequencies = possibleValues.Aggregate(new List<float>(), (acc, val) =>
                {
                    acc.Add(patternManager.getPatternFrequency(val));
                    return acc;
                },
                acc => acc).ToList();
            return valueFrequencies;
        }

        public List<VectorPair> Create4DirectionNeighbours(Vector2Int cellCoordinates, Vector2Int previousCell)
        {
            List<VectorPair> list = new List<VectorPair>()
            {
                new VectorPair(cellCoordinates, cellCoordinates + new Vector2Int(1, 0), Direction.Right, previousCell),
                new VectorPair(cellCoordinates, cellCoordinates + new Vector2Int(-1, 0), Direction.Left, previousCell),
                new VectorPair(cellCoordinates, cellCoordinates + new Vector2Int(0, 1), Direction.Up, previousCell),
                new VectorPair(cellCoordinates, cellCoordinates + new Vector2Int(0, -1), Direction.Down, previousCell),
            };
            return list;
        }

        public List<VectorPair> Create4DirectionNeighbours(Vector2Int cellCoordinates)
        {
            return Create4DirectionNeighbours(cellCoordinates, cellCoordinates);
        }

        public float CalculateEntropy(Vector2Int position, OutputGrid outputGrid)
        {
            float sum = 0;
            foreach (var possibleIndex in outputGrid.GetPossibleValueForPosition(position))
            {
                totalFrequency += patternManager.getPatternFrequency(possibleIndex);
                sum += patternManager.getPatternFrequency2(possibleIndex);
            }
            totalFrequencyLog = Mathf.Log(totalFrequency, 2);

            return totalFrequencyLog - (sum / totalFrequency);
        }

        public List<VectorPair> CheckIfNeighboursAreCollapsed(VectorPair pairToCheck, OutputGrid outputGrid)
        {
            return Create4DirectionNeighbours(pairToCheck.cellToPropagatePosition, pairToCheck.baseCellPosition).Where(
                x => outputGrid.CheckIfValidPosition(x.cellToPropagatePosition) &&
                     outputGrid.CheckIfCellIsCollapsed(x.cellToPropagatePosition) == false).ToList();
        }

        public bool CheckCellSolutionForCollision(Vector2Int cellCoordinates, OutputGrid outputGrid)
        {
            foreach (var neighbour in Create4DirectionNeighbours(cellCoordinates))
            {
                if (outputGrid.CheckIfValidPosition(neighbour.cellToPropagatePosition) == false)
                {
                    continue;;
                }
                HashSet<int> possibleIndicies =  new HashSet<int>();
                foreach (var patternIndexAtNeighbour in outputGrid.GetPossibleValueForPosition(neighbour.cellToPropagatePosition))
                {
                    var possibleNeighboursForBase =
                        patternManager.GetPossibleNeighboursForPatternInDirection(patternIndexAtNeighbour,
                            neighbour.directionFromBase.GetOppositeDirectionTo());
                    possibleIndicies.UnionWith(possibleNeighboursForBase);
                }

                if (possibleIndicies.Contains(outputGrid.GetPossibleValueForPosition(cellCoordinates).First()) == false)
                {
                    return true;
                }
            }

            return false;
        }
    }


}
