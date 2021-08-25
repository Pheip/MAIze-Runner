using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceInformation : MonoBehaviour
{
    public int rowSize { get; set; }
    public int columnSize { get; set; }
    public bool easyMaze { get; set; }
    public bool lvlOneMaze { get; set; }
    public int xOffset { get; set; }
    public int yOffset { get; set; }
    public int[,] maze { get; set; }
}