using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour
{
    public int xOffset { get; set; }
    public int yOffset { get; set; }
    public int[,] maze { get; set; }
}
