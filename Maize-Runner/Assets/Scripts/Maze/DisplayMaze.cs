using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMaze :MonoBehaviour
{
    public GameObject floor;
    public GameObject wall;
    public GameObject corner;
    public GameObject intersection;
    public GameObject tWall;
    public GameObject endPoint;
    public GameObject exit;
    public GameObject wallLamp;
    public GameObject gameFigure;
    private int rMax;
    private int cMax;
    private int[,] maze;
    string log = "";

    public void displayMaze(int[,] maze)
    {

        rMax = maze.GetUpperBound(0);
        cMax = maze.GetUpperBound(1);
        Debug.Log(rMax);
        Debug.Log(cMax);
        this.maze = maze;

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                createFloor(i, j);
                createWalls(i, j);

                log += maze[i,j]; 
            }
            log += "\n";
        }
        placeGameFigure();
        Debug.Log(log);
    }

    private void createFloor(int x, int z)
    {
        Instantiate(floor, new Vector3(x, 0, z), Quaternion.identity);
    }

    private void createWalls(int x, int z)
    {
        if(maze[0, z] == 0)
        {
            Instantiate(exit, new Vector3(0, 0, z), Quaternion.Euler(0, 90, 0));
        }
        if (maze[x,z] == 1)
        {
            if (!checkOuterWalls(x, z))
            {
                if (!checkIntersection(x, z))
                {
                    if (!checkTWall(x, z))
                    {
                        if (!checkCorner(x, z))
                        {
                            if (!checkEndPoint(x, z))
                            {
                                // Generate the rest of the walls
                                if (maze[x + 1, z] == 1 || maze[x - 1, z] == 1)
                                {
                                    Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
                                }
                                else if (maze[x, z + 1] == 1 || maze[x, z - 1] == 1)
                                {
                                    Instantiate(wall, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private bool checkEndPoint(int x, int z)
    {
        // Right 
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            Instantiate(endPoint, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        else

        //Bottom
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 1)
        {
            Instantiate(endPoint, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
            return true;
        }
        else
        //Top 
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            Instantiate(endPoint, new Vector3(x, 0, z), Quaternion.Euler(0, 270, 0));
            return true;
        }
        else
        //left
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 0)
        {
            Instantiate(endPoint, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
            return true;
        }
        return false;
    }
    private bool checkTopTWall(int x, int z)
    {
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 1 /*&& maze[x, z - 1] == 0*/)
        {
            Instantiate(tWall, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        return false;
    }

    private bool checkRightTWall(int x, int z)
    {
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            /*maze[x - 1, z] == 0 &&*/ maze[x, z - 1] == 1)
        {
            Instantiate(tWall, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }
    private bool checkBottomTWall(int x, int z)
    {
        if (maze[x + 1, z] == 1 && //maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            Instantiate(tWall, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
            return true;
        }
        return false;
    }

    private bool checkLeftTWall(int x, int z)
    {
        if (/*maze[x + 1, z] == 0 &&*/ maze[x, z + 1] == 1 &&
             maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            Instantiate(tWall, new Vector3(x, 0, z), Quaternion.Euler(0, 270, 0));
            return true;
        }
        return false;
    }

    private bool checkTWall(int x, int z)
    {
        return (checkTopTWall(x, z) || checkLeftTWall(x, z) || checkRightTWall(x, z) || checkBottomTWall(x, z));
    }
    private bool checkCorner(int x, int z)
    {
        //Top - Right Corner
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        else

        //Bottom - Right Corner
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 1)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
            return true;
        }
        else
        //Top - left Corner
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 0)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 270, 0));
            return true;
        }
        else
        //bottom - left Corner
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
            return true;
        }
        return false;
    }
    private bool checkIntersection(int x, int z)
    {
        if (maze[x + 1, z] == 1 && maze[x - 1, z] == 1 &&
                  maze[x, z + 1] == 1 && maze[x, z - 1] == 1)
        {
            Instantiate(intersection, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        return false;
    }
    private bool checkOuterWalls(int x, int z)
    {
        Debug.Log(x + " " + z);
        if (!checkOuterCorner(x,z))
        {
           ;
            if (x == 0)
            {
                if (!checkRightTWall(x, z))
                {
                    generateOuterWalls(x, z, false);
                    return true;
                }
                return true;
            }
            else

            if (x == rMax)
            {
                if (!checkLeftTWall(x, z))
                {
                    generateOuterWalls(x, z, false);
                    return true;
                }
                Debug.Log("kek");
                return true;
            }
            else

            if (z == 0)
            {
                if (!checkTopTWall(x, z))
                {
                    generateOuterWalls(x, z, true);
                    return true;
                }
                return true;
            }
            else

            if (z == cMax)
            {
                if (!checkBottomTWall(x, z))
                {
                    generateOuterWalls(x, z, true);
                    return true;
                }
                return true;
            }
            return false;
        }
        return checkOuterCorner(x, z);
    }

    private bool checkOuterCorner(int x, int z)
    {
        if (x == 0 && z == 0)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.identity);
            return true;
        }
        else
        //Corner top left
        if (x == 0 && z == cMax)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
            return true;
        }
        else
        //Corner top right
        if (x == rMax && z == cMax)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 180, 0));
            return true;
        }
        else
        //Corner bottom right
        if (x == rMax && z == 0)
        {
            Instantiate(corner, new Vector3(x, 0, z), Quaternion.Euler(0, 270, 0));
            return true;
        }
        return false;
    }
    private void generateOuterWalls(int x, int z, bool rotate)
    {
     
        //Corner bottom left
        if (!rotate)
        {
           Instantiate(wall, new Vector3(x, 0, z), Quaternion.Euler(0, 90, 0));
             
        }
        else
        {
            Instantiate(wall, new Vector3(x, 0, z), Quaternion.identity);
        }
        
    }

    private void placeGameFigure()
    {
        int x = 0;
        int y = 0;
        do
        {
             x = Random.Range(cMax - 5, cMax - 1);
             y = Random.Range(0, rMax - 1);
        }while(maze[x,y] == 1) ;
        Debug.Log("Player instantiate at: " + x + " " + y + " " + maze[x, y]);

        Instantiate(gameFigure, new Vector3(x, 0.5f, y), Quaternion.identity);

    }
}
