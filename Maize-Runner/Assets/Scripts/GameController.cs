using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    private MazeConstructor mazeConstructor;

    public int rowSize;
    public int columnSize;
    
    void Start()
    {

        mazeConstructor = GetComponent<MazeConstructor>();

        mazeConstructor.GenerateNewMaze(rowSize, columnSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
