using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : IMoveCommand
{
    public float MovementXAxis { get; set; }
    public float MovementYAxis { get; set; }
    public float RotationAxis { get; set; }
}
