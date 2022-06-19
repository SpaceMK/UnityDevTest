using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameManager manager;
    IGameManager gameManager => manager;

    [SerializeField] HUDStatusDisplay statusDisplay;
    void Start()
    {
        statusDisplay.Init(gameManager);
    }
}
