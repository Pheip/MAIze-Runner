using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DisplayMaze))]
public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator mazeDataGenerator;
    private DisplayMaze displayMaze;


    
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

        data = mazeDataGenerator.FromDimensions(sizeRows, sizeCols);
        displayMaze.displayMaze(data);

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
}
