using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    
    private void Start() {
    }

    public void setMaxHealth(int maxHealth) {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void setHealth(int health) {
        healthSlider.value = health;
    }

    public void setMaxMana(int maxMana) {
        manaSlider.maxValue = maxMana;
        manaSlider.value = maxMana;
    }

    public void setMana(int mana) {
        manaSlider.value = mana;
    }
}
