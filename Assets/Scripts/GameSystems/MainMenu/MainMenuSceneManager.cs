using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour, IMainMenuManager
{

    [SerializeField] GameDataController dataLoader;
    [SerializeField] MainMenuUIManager uiManager;
    [SerializeField] HeroManager heroManager;

    public Action HeroesReady { get; set; }
    public Action<HeroSelectionIcon, HeroScriptableObject, bool> HeroSelected { get; set; }
    public Action<bool> HeroSelectionReady { get; set; }
    public Action EnterBattle { get; set; }

    public GameDataController DataController => dataLoader;

    private void Awake()
    {

        if (!dataLoader.DataIsReady)
        {
            dataLoader.LoadingDone += LoadingComplete;
            dataLoader.Init();
        }
        else
        {
            LoadingComplete();
            CheckHeroSelection();
        }
        HeroSelected += HeroIsSelected;
        EnterBattle += LoadGameplayScene;
    }

    private void LoadingComplete()
    {
        uiManager.Init(this);
        heroManager.Init(this);
    }
    private void HeroIsSelected(HeroSelectionIcon icon, HeroScriptableObject hero, bool isSelected)
    {
       
        if (dataLoader.SelectedHeros.Count >= 3 && !dataLoader.SelectedHeros.Contains(hero))
        {
            icon.ForceDeselect();
            return;
        }

        dataLoader.ToogleSelectedHeroes(isSelected, hero);
        HeroSelectionReady?.Invoke(dataLoader.SelectedHeros.Count == 3);
    }


    public void CheckHeroSelection()
    {
        HeroSelectionReady?.Invoke(dataLoader.SelectedHeros.Count == 3);
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnApplicationQuit()
    {
        dataLoader.ClearOldData();
    }
}
