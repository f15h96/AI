using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
    public class InputReader : IInputReader<TileBase>
    {
        private Tilemap _inputTilemap;

        public InputReader(Tilemap inputTilemap)
        {
            _inputTilemap = inputTilemap;
        }
        

        public IValue<TileBase>[][] ReadInputToGrid()
        {
            var grid = ReadInputTileMap();

            TileBaseValue[][] gridofValues = null;
            if (grid != null)
            {
                gridofValues = MyCollectionExtension.CreateJaggedArray<TileBaseValue[][]>(grid.Length, grid[0].Length);
                for (int row = 0; row < grid.Length; row++)
                {
                    for (int col = 0; col < grid[0].Length; col++)
                    {
                        gridofValues[row][col] = new TileBaseValue(grid[row][col]);
                    }
                }
            }

            return gridofValues;
        }

        private TileBase[][] ReadInputTileMap()
        {
            InputImageParameters imagePerameters = new InputImageParameters(_inputTilemap);
            return CreateTileBaseGrid(imagePerameters);
        }

        private TileBase[][] CreateTileBaseGrid(InputImageParameters imagePerameters)
        {
            TileBase[][] gridOfInputTiles = null;
            gridOfInputTiles =
                MyCollectionExtension.CreateJaggedArray<TileBase[][]>(imagePerameters.Height, imagePerameters.Width);
            for (int row = 0; row < imagePerameters.Height; row++)
            {
                for (int col = 0; col < imagePerameters.Width; col++)
                {
                    gridOfInputTiles[row][col] = imagePerameters.StackOfTiles.Dequeue().Tile;

                }
            }

            return gridOfInputTiles;
        }
    }   

}
