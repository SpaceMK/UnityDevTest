using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button playButton;
    IMainMenuManager sceneManager;

    private void Start()
    {
        playButton.interactable = false;
    }

    public void Init(IMainMenuManager manager)
    {
        sceneManager = manager;
        sceneManager.HeroSelectionReady += TooglePlayButton;
        playButton.onClick.AddListener(() =>
        {
            sceneManager.EnterBattle?.Invoke();
        });
    }

    private void TooglePlayButton(bool buttonEnabled)
    {
        playButton.interactable = buttonEnabled;
    }

}
