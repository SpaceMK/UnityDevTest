using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUIManager : GameSceneController
{
    [SerializeField] Canvas gameUICanvas;
    [SerializeField] EndOfGamePopUp endOfGamePopUp;
    IGameManager gameManager;

    public override void LoadDependencies(IGameManager manager)
    {
        gameManager = manager;
        SetupUI();
    }

    private void SetupUI()
    {
        endOfGamePopUp.SetUpEndOfGamePopUp(gameManager.LoadMenuScene);
        gameManager.EndGameSession += endOfGamePopUp.OpenPopUp;
    }
}
