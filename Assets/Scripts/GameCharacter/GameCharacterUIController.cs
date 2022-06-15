using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCharacterUIController :MonoBehaviour , IGameCharacterComponent
{
    [SerializeField] Slider healthBar;
    float currentHealth;

    public void LoadCharacterDataToComponents(GameCharacterController controller)
    {
        healthBar.maxValue = controller.CharacterData.Health;
        currentHealth = controller.CharacterData.Health;
        healthBar.value = controller.CharacterData.Health;
        controller.HealthUpdate += UpdateHealthBar;
    }


    public void UpdateHealthBar(float newHealth)
    {
        currentHealth = newHealth;
        healthBar.DOValue(currentHealth,1f); 
    }
}
