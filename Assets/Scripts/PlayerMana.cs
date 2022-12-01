using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private HealthBar healthManaBar;

    public int mana = 9;
    // Start is called before the first frame update
    void Start()
    {
        healthManaBar.setMaxMana(mana);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsumeMana()
    {
        if (mana > 0)
        {
            mana -= 1;
            healthManaBar.setMana(mana);
        }
    }

    public void RestoreMana(int i)
    {
        int restoreMana = mana + i;
        if (restoreMana <= 9)
        {
            mana = restoreMana;
        }
        else
        {
            mana = 9;
        }

        healthManaBar.setMana(mana);
    }

}
