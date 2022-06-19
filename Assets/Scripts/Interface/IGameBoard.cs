
using UnityEngine;

public interface IGameBoard
{
    float HorizotalCameraBoundary { get; }
    float VerticallCameraBoundary { get; }
    Camera MainCamera { get; }
    void CalculateCameraBounds();




}
