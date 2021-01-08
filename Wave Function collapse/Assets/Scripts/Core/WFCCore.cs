using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveFunctionCollapse
{
    public class WFCCore
    {
        private OutputGrid outputGrid;
        private PatternManager patternManager;

        private int maxIterations = 0;

        public WFCCore(int outputWidth, int outputHeight, int maxIterations, PatternManager patternManager)
        {
            this.patternManager = patternManager;
            this.outputGrid = new OutputGrid(outputWidth, outputHeight, patternManager.GetNumberOfPatterns());
            this.maxIterations = maxIterations;
        }

        public int[][] CreateOutputGrid()
        {
            int iteration = 0;
            while (iteration < this.maxIterations)
            {
                CoreSolver solver = new CoreSolver(this.outputGrid, this.patternManager);
                int innerIteration = 100;
                while (!solver.CheckForConflicts() && !solver.CheckIfSolved())
                {
                    Vector2Int position = solver.GetLowestEntropycell();
                    solver.CollapseCell(position);
                    solver.Propagate();
                    innerIteration--;
                    if (innerIteration <= 0)
                    {
                        Debug.Log("Propagation is taking too long");
                        return new int[0][];
                    }
                }
                if (solver.CheckForConflicts())
                {
                    Debug.Log("\n Conflict occured. Iteration " + iteration);
                    iteration++;
                    outputGrid.ResetAllPossibilities();
                    solver = new CoreSolver(this.outputGrid, this.patternManager);
                }
                else
                {
                    Debug.Log("Solved On: "+ iteration);
                    this.outputGrid.PrintResultToConsole();
                    break;
                }
                
            }
            if (iteration >= this.maxIterations)
            {
                Debug.Log("Could not solve the tilemap");
            }

            return outputGrid.GetSolvedOututGrid();
        }
    }


}
