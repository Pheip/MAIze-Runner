using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplayMaze))]
public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator mazeDataGenerator;
    private DisplayMaze displayMaze;
    public int rows;
    public int columns;
    public bool easyMaze;
    public bool allSame;
    public bool lvl1Maze;

    
    public int[,] data
    {
        get; private set;
    }

    private void Awake()
    {

        data = new int[,]
        {
            {1,1,1 },
            {1,0,1 },
            {1,1,1 }
        };

        mazeDataGenerator = new MazeDataGenerator();
        displayMaze = GetComponent<DisplayMaze>();
    }
    void Start()
    {
        
    }

   public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Better use odd numbers for maze size");
        }


        int xOffset = 0;
        int yOffset = 0;
        if (allSame)
        {
            generateMazeLayout(sizeRows, sizeCols);
        }
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                if (!allSame)
                {
                    generateMazeLayout(sizeRows, sizeCols);
                }
                displayMaze.displayMaze(data, xOffset, yOffset);
                yOffset += 20;
                
            }
            yOffset = 0;
            xOffset += 20;
        }
        

    }

    private void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i,j] == 0)
                {
                    msg += "0";
                }
                else
                {
                    msg += "1";
                }
                
            }
            msg += "\n";
        }

        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    private void generateMazeLayout(int sizeRows, int sizeCols)
    {
        if (easyMaze)
        {
            data = mazeDataGenerator.EasyMaze(sizeRows, sizeCols);
        }
        else if(lvl1Maze)
        {
            data = mazeDataGenerator.mazeLvlOne(sizeRows, sizeCols);
        }
        else
        {
            data = mazeDataGenerator.FromDimensions(sizeRows, sizeCols);
        }
    }
}
