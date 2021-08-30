using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    //chance of empty space
    public float placementThreshold; 

    public MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeCols, sizeRows];

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
      
                if(i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                } else if (i % 2 == 0 && j % 2 == 0)
                {
                    if(Random.value > placementThreshold)
                    {
                        maze[i, j] = 1;

                        int a = Random.value < .5 ? 0: (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b] = 1;
                    }
                }
            }
        }
        
        int exit = Random.Range(1, rMax - 2);
        Debug.Log(Random.Range(1, 3));
        if (Random.Range(1, 100) % 2 == 0) {

            maze[0, exit] = 0;
        } else {
            maze[cMax, exit] = 0;
        }


        return maze;
    }

    public int[,] mazeLvlOne(int sizeRows, int sizeCols)
    {
        int[,] maze = EasyMaze(sizeRows, sizeCols);
        
        for (int i = 0; i <= 10; i++)
        {
            maze[Random.Range(2,sizeRows - 2), Random.Range( 2, sizeCols - 2)] = 1;
        }
        
        return maze;
    }


    public int[,] EasyMaze(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeCols, sizeRows];

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {

                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = 1;
                }
                else if (i % 2 == 0 && j % 2 == 0)
                {
                     maze[i, j] = 0;          
                }
            }
        }

        int exit = Random.Range(1, rMax - 2);
        Debug.Log(Random.Range(1, 3));
        if (Random.Range(1, 100) % 2 == 0)
        {

            maze[0, exit] = 0;
        }
        else
        {
            maze[cMax, exit] = 0;
        }


        return maze;
    }

}
