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

    private int rMax;
    private int cMax;
    private int[,] maze;
    string log = "";

    public void displayMaze(int[,] maze, int xOffset, int yOffset)
    {

        rMax = maze.GetUpperBound(0);
        cMax = maze.GetUpperBound(1);
       // Debug.Log(rMax);
        //Debug.Log(cMax);
        this.maze = maze;

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                createFloor(i, j, xOffset, yOffset);
                createWalls(i, j, xOffset, yOffset);

                log += maze[i,j]; 
            }
            log += "\n";
        }
      
    }

    private void createFloor(int x, int z, int xOffset, int yOffset)
    {
        var go = Instantiate(floor, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
        go.transform.parent = gameObject.transform;
        go.transform.localPosition = new Vector3(x, 0, z);
    }

    private void createWalls(int x, int z, int xOffset, int yOffset)
    {
        if(maze[0, z] == 0)
        {
            var go = Instantiate(exit, new Vector3(0 + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(0, 0, z);
        }
        if (maze[x,z] == 1)
        {
            if (!checkOuterWalls(x, z, xOffset, yOffset))
            {
                if (!checkIntersection(x, z, xOffset, yOffset))
                {
                    if (!checkTWall(x, z, xOffset, yOffset))
                    {
                        if (!checkCorner(x, z, xOffset, yOffset))
                        {
                            if (!checkEndPoint(x, z, xOffset, yOffset))
                            {
                                // Generate the rest of the walls
                                if (maze[x + 1, z] == 1 || maze[x - 1, z] == 1)
                                {
                                    var go = Instantiate(wall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
                                    go.transform.parent = gameObject.transform;
                                    go.transform.localPosition = new Vector3(x, 0, z);
                                }
                                else if (maze[x, z + 1] == 1 || maze[x, z - 1] == 1)
                                {
                                    var go = Instantiate(wall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
                                    go.transform.parent = gameObject.transform;
                                    go.transform.localPosition = new Vector3(x, 0, z);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private bool checkEndPoint(int x, int z, int xOffset, int yOffset)
    {
        // Right 
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            var go = Instantiate(endPoint, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else

        //Bottom
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(endPoint, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //Top 
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            var go = Instantiate(endPoint, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 270, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //left
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 0)
        {
            var go = Instantiate(endPoint, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 180, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }
    private bool checkTopTWall(int x, int z ,int xOffset, int yOffset)
    {
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 1 /*&& maze[x, z - 1] == 0*/)
        {
            var go = Instantiate(tWall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }

    private bool checkRightTWall(int x, int z, int xOffset, int yOffset)
    {
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            /*maze[x - 1, z] == 0 &&*/ maze[x, z - 1] == 1)
        {
            var go = Instantiate(tWall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }
    private bool checkBottomTWall(int x, int z, int xOffset, int yOffset)
    {
        if (maze[x + 1, z] == 1 && //maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(tWall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 180, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }

    private bool checkLeftTWall(int x, int z, int xOffset, int yOffset)
    {
        if (/*maze[x + 1, z] == 0 &&*/ maze[x, z + 1] == 1 &&
             maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(tWall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 270, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }

    private bool checkTWall(int x, int z, int xOffset, int yOffset)
    {
        return (checkTopTWall(x, z, xOffset, yOffset) || checkLeftTWall(x, z, xOffset, yOffset) ||
            checkRightTWall(x, z, xOffset, yOffset) || checkBottomTWall(x, z, xOffset, yOffset));
    }
    private bool checkCorner(int x, int z, int xOffset, int yOffset)
    {
        //Top - Right Corner
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 0)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else

        //Bottom - Right Corner
        if (maze[x + 1, z] == 1 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 0 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //Top - left Corner
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 1 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 0)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 270, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //bottom - left Corner
        if (maze[x + 1, z] == 0 && maze[x, z + 1] == 0 &&
            maze[x - 1, z] == 1 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 180, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }
    private bool checkIntersection(int x, int z, int xOffset, int yOffset)
    {
        if (maze[x + 1, z] == 1 && maze[x - 1, z] == 1 &&
                  maze[x, z + 1] == 1 && maze[x, z - 1] == 1)
        {
            var go = Instantiate(intersection, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }
    private bool checkOuterWalls(int x, int z, int xOffset, int yOffset)
    {
        if (!checkOuterCorner(x,z, xOffset, yOffset))
        {
           ;
            if (x == 0)
            {
                if (!checkRightTWall(x, z, xOffset, yOffset))
                {
                    generateOuterWalls(x, z, false, xOffset, yOffset);
                    return true;
                }
                return true;
            }
            else

            if (x == rMax)
            {
                if (!checkLeftTWall(x, z, xOffset, yOffset))
                {
                    generateOuterWalls(x, z, false, xOffset, yOffset);
                    return true;
                }
                return true;
            }
            else

            if (z == 0)
            {
                if (!checkTopTWall(x, z, xOffset, yOffset))
                {
                    generateOuterWalls(x, z, true, xOffset, yOffset);
                    return true;
                }
                return true;
            }
            else

            if (z == cMax)
            {
                if (!checkBottomTWall(x, z, xOffset, yOffset))
                {
                    generateOuterWalls(x, z, true, xOffset, yOffset);
                    return true;
                }
                return true;
            }
            return false;
        }
        return checkOuterCorner(x, z, xOffset, yOffset);
    }

    private bool checkOuterCorner(int x, int z, int xOffset, int yOffset)
    {
        if (x == 0 && z == 0)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //Corner top left
        if (x == 0 && z == cMax)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //Corner top right
        if (x == rMax && z == cMax)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 180, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        else
        //Corner bottom right
        if (x == rMax && z == 0)
        {
            var go = Instantiate(corner, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 270, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
            return true;
        }
        return false;
    }
    private void generateOuterWalls(int x, int z, bool rotate, int xOffset, int yOffset)
    {
     
        //Corner bottom left
        if (!rotate)
        {
            var go = Instantiate(wall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.Euler(0, 90, 0));
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);

        }
        else
        {
            var go = Instantiate(wall, new Vector3(x + xOffset, 0, z + yOffset), Quaternion.identity);
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = new Vector3(x, 0, z);
        }
        
    }

  
}
