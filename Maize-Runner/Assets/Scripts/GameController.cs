using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    private MazeConstructor mazeConstructor;

    public int rowSizeInstances;
    public int columnSizeInstances;

    public int rowSizeLabyrinth;
    public int columnsizeLabyrinth;
    public bool easyMaze;
    public bool lvlOneMaze;
    public GameObject instance;
    
    void Start()
    {

        //mazeConstructor = GetComponent<MazeConstructor>();

        // mazeConstructor.GenerateNewMaze(rowSize, columnSize);
        int xOffset = 0;
        int yOffset = 0;
        for (int i = 0; i < columnSizeInstances; i++)
        {
            for (int j = 0; j < rowSizeInstances; j++)
            {
                var go = Instantiate(instance, new Vector3(xOffset, 0, yOffset), Quaternion.identity);
                InstanceInformation info = go.GetComponent<InstanceInformation>();
                info.rowSize = rowSizeLabyrinth;
                info.columnSize = columnsizeLabyrinth;
                info.easyMaze = easyMaze;
                info.lvlOneMaze = lvlOneMaze;
                yOffset += 30;

            }
            yOffset = 0;
            xOffset += 30;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
