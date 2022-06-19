using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveCommand
{ 
    float MovementYAxis { get; set; }
    float RotationAxis { get; set; }
}
