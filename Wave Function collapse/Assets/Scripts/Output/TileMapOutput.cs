using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WaveFunctionCollapse
{
   public class TileMapOutput : IOutputCreator<Tilemap>
   {
      
      public Tilemap OutputImage { get; }
      private ValuesManager<TileBase> valuesManager;
      private Tilemap outputImage;

      public TileMapOutput(ValuesManager<TileBase> valuesManager, Tilemap outputImage)
      {
         this.outputImage = outputImage;
         this.valuesManager = valuesManager;
      }
      
      public void CreateOutput(PatternManager manager, int[][] outputValues, int width, int height)
      {
         if (outputValues.Length == 0)
         {
            return;
         }
         this.outputImage.ClearAllTiles();

         int[][] valueGrid;
         valueGrid = manager.ConvertPatternToValues<TileBase>(outputValues);

         for (int row = 0; row < height; row++)
         {
            for (int col = 0; col < width; col++)
            {
               TileBase tile = (TileBase) this.valuesManager.GetValueFromIndex(valueGrid[row][col]).Value;
               this.outputImage.SetTile(new Vector3Int(col,row,0 ), tile );
            }
         }
      }
   }
 

}

