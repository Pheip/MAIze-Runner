using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplayMaze))]
public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator mazeDataGenerator;
    private DisplayMaze displayMaze;
    public bool allSame;
    
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

   public void GenerateNewMaze(int rows, int columns)
    {
        if (rows % 2 == 0 && columns % 2 == 0)
        {
            Debug.LogError("Better use odd numbers for maze size");
        }
        int xOffset = 0;
        int yOffset = 0;
        generateMazeLayout(rows, columns);
        displayMaze.displayMaze(data, xOffset, yOffset);

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
        InstanceInformation info = this.transform.parent.GetComponent<InstanceInformation>();
        if (info.easyMaze)
        {
            data = mazeDataGenerator.EasyMaze(sizeRows, sizeCols);
        }
        else if(info.lvlOneMaze)
        {
            data = mazeDataGenerator.mazeLvlOne(sizeRows, sizeCols);
        }
        else
        {
            data = mazeDataGenerator.FromDimensions(sizeRows, sizeCols);
        }
        
        info.maze = data;
    }
}
