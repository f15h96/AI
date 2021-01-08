using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class PropragationHelper
    {
        private OutputGrid outputGrid;
        private CoreHelper coreHelper;
        private bool cellWithNoSolutionPresent;
        SortedSet<LowEntropyCell> lowEntropyCells = new SortedSet<LowEntropyCell>();
        Queue<VectorPair> pairsToPropergate = new Queue<VectorPair>();

        public SortedSet<LowEntropyCell> LowEntropyCells
        {
            get => lowEntropyCells;
        }

        public Queue<VectorPair> PairsToPropergate
        {
            get => pairsToPropergate;
        }

        public PropragationHelper(OutputGrid outputGrid, CoreHelper coreHelper)
        {
            this.outputGrid = outputGrid;
            this.coreHelper = coreHelper;
        }

        public bool CheckIfPairShouldBeProcessed(VectorPair propergationPair)
        {
            return outputGrid.CheckIfValidPosition(propergationPair.cellToPropagatePosition) &&
                   propergationPair.AreWeCheckingPreviousCellAgain() == false;
        }

        public void AnalyzePropagationResults(VectorPair propagatePair,int startCount, int newPossiblePatternCount)
        {
            if (newPossiblePatternCount > 1 && startCount > newPossiblePatternCount)
            {
                AddNewPairsToPropagateQueue(propagatePair.cellToPropagatePosition, propagatePair.baseCellPosition);
                AddToLowEntropySet(propagatePair.cellToPropagatePosition);
            }

            if (newPossiblePatternCount == 0)
            {
                cellWithNoSolutionPresent = true;
            }

            if (newPossiblePatternCount == 1)
            {
                cellWithNoSolutionPresent =
                    coreHelper.CheckCellSolutionForCollision(propagatePair.cellToPropagatePosition, outputGrid);
            }
        }

        private void AddToLowEntropySet(Vector2Int propagatePairCellToPropagatePosition)
        {
            var elementOfLowEntropySet = lowEntropyCells.Where(x => x.Position == propagatePairCellToPropagatePosition).FirstOrDefault();
            if (elementOfLowEntropySet == null && outputGrid.CheckIfCellIsCollapsed(propagatePairCellToPropagatePosition) == false)
            {
                float entropy = coreHelper.CalculateEntropy(propagatePairCellToPropagatePosition, outputGrid);
                lowEntropyCells.Add(new LowEntropyCell(propagatePairCellToPropagatePosition, entropy));
            }
            else
            {
                lowEntropyCells.Remove(elementOfLowEntropySet);
                elementOfLowEntropySet.Entropy =
                    coreHelper.CalculateEntropy(propagatePairCellToPropagatePosition, outputGrid);
                lowEntropyCells.Add(elementOfLowEntropySet);
            }
            
        }
        

        public void AddNewPairsToPropagateQueue(Vector2Int cellToPropagate, Vector2Int baseCellPosition)
        {
            var list = coreHelper.Create4DirectionNeighbours(cellToPropagate, baseCellPosition);
            foreach (var item in list)
            {
                pairsToPropergate.Enqueue(item);
            }
        }

        public bool CheckForConflicts()
        {
            return cellWithNoSolutionPresent;
        }

        public void SetConflictFlag()
        {
            cellWithNoSolutionPresent = true;
        }

        public void EnqueueUncollapsedNeighbours(VectorPair propagatePair)
        {
            var uncollapsedNeighbours = coreHelper.CheckIfNeighboursAreCollapsed(propagatePair, outputGrid);
            foreach (var uncollapsed  in uncollapsedNeighbours)
            {
                pairsToPropergate.Enqueue(uncollapsed);
            }
        }
    }


}
