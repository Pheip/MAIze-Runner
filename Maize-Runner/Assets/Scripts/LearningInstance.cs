using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningInstance : MonoBehaviour
{
    public GameObject lab;
    public GameObject agent;
    InstanceInformation info;
    GameObject instLab;
    private void Awake()
    {
        info = gameObject.GetComponent<InstanceInformation>();
    }
    void Start()
    {
        instLab = Instantiate(lab, new Vector3(0, 0, 0), Quaternion.identity);
        instLab.transform.parent = gameObject.transform;
        instLab.transform.localPosition = new Vector3(0, 0, 0);

        MazeConstructor m = instLab.GetComponent<MazeConstructor>();
        m.GenerateNewMaze(info.rowSize,info.columnSize);
        placeGameFigure(); 


    }

    public void createNewMaze()
    {
        Destroy(instLab);
        instLab = Instantiate(lab, new Vector3(0, 0, 0), Quaternion.identity);
        instLab.transform.parent = gameObject.transform;
        instLab.transform.localPosition = new Vector3(0, 0, 0);

        MazeConstructor m = instLab.GetComponent<MazeConstructor>();
        m.GenerateNewMaze(info.rowSize, info.columnSize);
    }

    public void placeGameFigure()
    {
        int cMax = info.maze.GetUpperBound(1);
        int rMax = info.maze.GetUpperBound(0);

        int x;
        int y;
        do
        {
            x = Random.Range(cMax - 5, cMax - 1);
            y = Random.Range(0, rMax - 1);
        } while (info.maze[x, y] == 1);
        Debug.Log("Player instantiate at: " + (x) + " " + (y ));

        GameObject player = Instantiate(agent, new Vector3(x, 0.5f, y), Quaternion.identity);
        player.transform.parent = gameObject.transform;
        player.transform.localPosition = new Vector3(x, 0, y);
   
    }
}
