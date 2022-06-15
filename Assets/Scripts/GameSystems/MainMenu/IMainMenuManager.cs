using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMainMenuManager 
{
  public GameDataController DataController { get; }

    public Action HeroesReady { get; set; }
    public Action<HeroSelectionIcon, HeroScriptableObject, bool> HeroSelected { get; set; }
    public Action<bool> HeroSelectionReady { get; set; }
    public Action EnterBattle { get; set; }


}
