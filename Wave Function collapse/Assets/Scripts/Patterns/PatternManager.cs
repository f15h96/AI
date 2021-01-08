using System.Collections;
using System.Collections.Generic;
using System.Text;
using Helpers;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class PatternManager
    {
        private Dictionary<int, PatternData> patternDataIndexDictionary;
        private Dictionary<int, PatternNeighbours> patternPossibleNeighboursDictionary;
        private int patternSize = -1;
        private IFindNeighbourStrategy strategy;

        public PatternManager(int patternSize)
        {
            this.patternSize = patternSize;
        }

        public void ProcessGrid<T>(ValuesManager<T> valuesManager, bool equalWeights, string strategyName = null)
        {
            NeighbourStrategyFactory strategyFactory = new NeighbourStrategyFactory();
            strategy = strategyFactory.CreateInstance(strategyName == null ? patternSize + "" : strategyName);
            CreatePatterns(valuesManager, strategy, equalWeights);
        }

        private void CreatePatterns<T>(ValuesManager<T> valuesManager, IFindNeighbourStrategy findNeighbourStrategy, bool equalWeights)
        {
            var patternFinderResult = PatternFinder.GetPatternDataFromGrid(valuesManager, patternSize, equalWeights);
            /*StringBuilder builder = null;
            List<string> list = new List<string>();
            for (int row = 0; row < patternFinderResult.GetGridLengthY(); row++)
            {
                builder = new StringBuilder();
                for (int col = 0; col < patternFinderResult.GetGridLengthX(); col++)
                {
                    builder.Append(patternFinderResult.GetIndexAt(col, row) + " ");
                }
                list.Add(builder.ToString());
            }
            list.Reverse();
            foreach (var item in list)
            {
                Debug.Log(item);
            }*/
            patternDataIndexDictionary = patternFinderResult.PatternIndexDictionary;
            GetPatternNeighbours(patternFinderResult, strategy);
        }

        private void GetPatternNeighbours(PatternDataResults patternFinderResult, IFindNeighbourStrategy findNeighbourStrategy)
        {
            patternPossibleNeighboursDictionary =
                PatternFinder.FindPossibleNeighboursForAllPatterns(strategy, patternFinderResult);
        }

        public PatternData GetPatternDataFromIndex(int index)
        {
            return patternDataIndexDictionary[index];
        }

        public HashSet<int> GetPossibleNeighboursForPatternInDirection(int patternIndex, Direction dir)
        {
            return patternPossibleNeighboursDictionary[patternIndex].GetNeighboursInDirection(dir);
        }

        public float getPatternFrequency(int index)
        {
            return GetPatternDataFromIndex(index).FrequencyRelative;
        }
        
        public float getPatternFrequency2(int index)
        {
            return GetPatternDataFromIndex(index).FrequencyRelative2;
        }

        public int GetNumberOfPatterns()
        {
            return patternDataIndexDictionary.Count;
        }

        public int[][] ConvertPatternToValues<T>(int[][] outputValues)
        {
            int patternOutputWidth = outputValues[0].Length;
            int patternOutputHeight = outputValues.Length;
            int valueGridWidth = patternOutputWidth + patternSize - 1;
            int valueGridHeight = patternOutputHeight + patternSize - 1;
            int[][] valueGrid = MyCollectionExtension.CreateJaggedArray<int[][]>(valueGridHeight, valueGridWidth);
            for (int row = 0; row < patternOutputHeight; row++)
            {
                for (int col = 0; col < patternOutputWidth; col++)
                {
                    Pattern pattern = GetPatternDataFromIndex(outputValues[row][col]).Pattern;
                    GetPatternValues(patternOutputWidth, patternOutputHeight, valueGrid, row, col, pattern);
                }
            }

            return valueGrid;
        }

        private void GetPatternValues(int patternOutputWidth, int patternOutputHeight, int[][] valueGrid, int row, int col, Pattern pattern)
        {
            if (row == patternOutputHeight -1 && col == patternOutputWidth - 1)
            {
                for (int row1 = 0; row1 < patternSize; row1++)
                {
                    for (int col1 = 0; col1 < patternSize; col1++)
                    {
                        valueGrid[row + row1][col + col1] = pattern.GetGridValue(col1, row1);
                    }
                }
            }
            else if (row == patternOutputHeight - 1)
            {
                for (int row1 = 0; row1 < patternSize; row1++)
                {
                    valueGrid[row + row1][col] = pattern.GetGridValue(0, row1);
                }
            }
            else if (col == patternOutputWidth - 1)
            {
                for (int col1 = 0; col1 < patternSize; col1++)
                {
                    valueGrid[row][col + col1] = pattern.GetGridValue(col1, 0);
                }
            }
            else
            {
                valueGrid[row][col] = pattern.GetGridValue(0, 0);
            }
        }
    }


}
