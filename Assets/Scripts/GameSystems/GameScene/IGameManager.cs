using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager: ICameraComponent
{
    GameDataController GameData { get; }
    GameSceneController GetGameControllerComponent(Type type);
    void LoadMenuScene();
    Action<GameplayState> EndGameSession { get; set; }
}
