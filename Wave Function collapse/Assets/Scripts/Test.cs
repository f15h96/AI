using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using WaveFunctionCollapse;

public class Test : MonoBehaviour
{
    public Tilemap InputTilemap;

    public Tilemap OutputTilemap;

    public int patternSize;

    public int maxItteration = 500;

    public int outputWidth = 5;

    public int outputHeight = 5;

    public bool equalWeights = false;

    private ValuesManager<TileBase> valueManager;

    private WFCCore core;

    private PatternManager manager;

    private TileMapOutput output;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateWFC();
        
        /*InputReader reader = new InputReader(input);
        var grid = reader.ReadInputToGrid();
        //for (int row = 0; row < grid.Length; row++)
        //{
        //    for (int col = 0; col < grid[0].Length; col++)
        //    {
        //        Debug.Log("Row: " + row + " Col: " + col + " tile name " + grid[row][col].Value.name);
        //    }
        //}
        ValuesManager<TileBase> valueManager = new ValuesManager<TileBase>(grid);
        PatternManager manager = new PatternManager(1);
        manager.ProcessGrid(valueManager, false);
        WFCCore core = new WFCCore(5,5,500, manager);
        var result = core.CreateOutputGrid();
        /*foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            Debug.Log(dir.ToString() + " " + string.Join(" ", manager.GetPossibleNeighboursForPatternInDirection(0, dir).ToArray()));
        }*/
        //StringBuilder builder = null;
        //List<string> list = new List<string>();
        //for (int row = 0; row < grid.Length; row++)
        //{
        //    builder = new StringBuilder();
        //    for (int col = 0; col < grid[0].Length; col++)
        //    {
        //        builder.Append(valueManager.GetGridValuesIncludingOffset(col, row) + " ");
        //    }
        //    list.Add(builder.ToString());
        //}
        //list.Reverse();
        //foreach (var item in list)
        //{
        //    Debug.Log(item);
        //}*/
    }

    public void CreateWFC()
    {
        InputReader reader = new InputReader(InputTilemap);
        var grid = reader.ReadInputToGrid();
        valueManager = new ValuesManager<TileBase>(grid);
        manager = new PatternManager(patternSize);
        manager.ProcessGrid(valueManager, equalWeights);
        core = new WFCCore(outputWidth,outputHeight,maxItteration, manager);
    }

    public void CreateTileMap()
    {
        output = new TileMapOutput(valueManager,this.OutputTilemap);
        var result = core.CreateOutputGrid();
        output.CreateOutput(manager, result, outputWidth, outputHeight);
    }

    public void SaveTilemap()
    {
        if (output.OutputImage != null)
        {
            OutputTilemap = output.OutputImage;
            GameObject objectToSave = OutputTilemap.gameObject;

            PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Saved/output.prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
