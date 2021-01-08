using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class CoreSolver
    {
        private PatternManager patternManager;
        private OutputGrid outputGrid;
        private CoreHelper coreHelper;
        private PropragationHelper propragationHelper;

        public CoreSolver(OutputGrid outputGrid, PatternManager patternManager)
        {
            this.outputGrid = outputGrid;
            this.patternManager = patternManager;
            this.coreHelper = new CoreHelper(this.patternManager);
            this.propragationHelper = new PropragationHelper(this.outputGrid, this.coreHelper);
        }

        public void Propagate()
        {
            while (propragationHelper.PairsToPropergate.Count > 0)
            {
                var propagatePair = propragationHelper.PairsToPropergate.Dequeue();
                if (propragationHelper.CheckIfPairShouldBeProcessed(propagatePair))
                {
                    ProcessCell(propagatePair);
                }

                if (propragationHelper.CheckForConflicts() || outputGrid.CheckIfGridIsSolved())
                {
                    return;
                }
            }

            if (propragationHelper.CheckForConflicts() && propragationHelper.PairsToPropergate.Count == 0 && propragationHelper.LowEntropyCells.Count == 0)
            {
                propragationHelper.SetConflictFlag();
            }
        }

        private void ProcessCell(VectorPair propagatePair)
        {
            if (outputGrid.CheckIfCellIsCollapsed(propagatePair.cellToPropagatePosition))
            {
                propragationHelper.EnqueueUncollapsedNeighbours(propagatePair);
            }
            else
            {
                PropagateNeighbour(propagatePair);
            }
        }

        private void PropagateNeighbour(VectorPair propagatePair)
        {
            var possibleValuesAtNeighbour =
                outputGrid.GetPossibleValueForPosition(propagatePair.cellToPropagatePosition);
            int startCount = possibleValuesAtNeighbour.Count;

            RemoveImpossibleNeighbours(propagatePair, possibleValuesAtNeighbour);

            int newPossiblePatternCount = possibleValuesAtNeighbour.Count;
            propragationHelper.AnalyzePropagationResults(propagatePair, startCount, newPossiblePatternCount);
        }

        private void RemoveImpossibleNeighbours(VectorPair propagatePair, HashSet<int> possibleValuesAtNeighbour)
        {
            HashSet<int> possibleIndicies = new HashSet<int>();

            foreach (var patternIndexAtBase in outputGrid.GetPossibleValueForPosition(propagatePair.baseCellPosition))
            {
                var possibleNeighboursForBase =
                    patternManager.GetPossibleNeighboursForPatternInDirection(patternIndexAtBase,
                        propagatePair.directionFromBase);
                possibleIndicies.UnionWith(possibleNeighboursForBase);
            }
            possibleValuesAtNeighbour.IntersectWith(possibleIndicies);
        }

        public Vector2Int GetLowestEntropycell()
        {
            if (propragationHelper.LowEntropyCells.Count <= 0)
            {
                return outputGrid.GetRandomCell();
            }
            else
            {
                var lowestEntropyElement = propragationHelper.LowEntropyCells.First();
                Vector2Int returnVector = lowestEntropyElement.Position;
                propragationHelper.LowEntropyCells.Remove(lowestEntropyElement);
                return returnVector;
            }
        }

        public void CollapseCell(Vector2Int cellCoordinates)
        {
            var possibleValue = outputGrid.GetPossibleValueForPosition(cellCoordinates).ToList();

            if (possibleValue.Count == 0 || possibleValue.Count == 1)
            {
                return;
            }
            else
            {
                int index = coreHelper.SelectSolutionPatternFromFrequency(possibleValue);
                outputGrid.SetPatternOnPosition(cellCoordinates.x, cellCoordinates.y, possibleValue[index]);
            }

            if (coreHelper.CheckCellSolutionForCollision(cellCoordinates, outputGrid) == false)
            {
                propragationHelper.AddNewPairsToPropagateQueue(cellCoordinates, cellCoordinates);
            }
            else
            {
                propragationHelper.SetConflictFlag();
            }
            
        }

        public bool CheckIfSolved()
        {
            return outputGrid.CheckIfGridIsSolved();
        }

        public bool CheckForConflicts()
        {
            return propragationHelper.CheckForConflicts();
        }
    }


}

