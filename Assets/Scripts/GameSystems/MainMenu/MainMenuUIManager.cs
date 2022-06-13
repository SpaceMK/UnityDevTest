using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] SplashScreenPopUp splashScreen;
    [SerializeField] HeroSelectionPanel selectionPanel;
    [SerializeField] BottomBar bottomBar;

    GameDataLoader gameData;
    MainMenuSceneManager sceneManager;

    public void Init(GameDataLoader data,MainMenuSceneManager manager)
    {
        gameData = data;
        sceneManager = manager;
        sceneManager.HeroesReady += CreateHeroIcons;
        bottomBar.Init(sceneManager);
        splashScreen.ToggleSplashScreen(false);
    }

    private void CreateHeroIcons()
    {
        foreach (var hero in gameData.AvailableHeros)
        {
            selectionPanel.CreateHeroIcon(hero,sceneManager);
        }
    }
}