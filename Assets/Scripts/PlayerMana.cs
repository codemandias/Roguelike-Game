using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public int currentMana;
    public int maxMana = 9;

    public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaBar.setMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            ConsumeMana();
            Debug.Log(currentMana);
        }
        if (Input.GetKeyDown(KeyCode.RightShift)) {
            RestoreMana(2);
            Debug.Log(currentMana);
        }
    }

    public void ConsumeMana()
    {
        if (currentMana > 0)
        {
            currentMana -= 1;
            manaBar.setMana(currentMana);
            Debug.Log(currentMana);
        }
    }

    public void RestoreMana(int i)
    {
        int restoreMana = currentMana + i;
        if (restoreMana <= 9)
        {
            currentMana = restoreMana;
            manaBar.setMana(currentMana);
            Debug.Log(currentMana);
        }
        else
        {
            currentMana = 9;
            manaBar.setMana(currentMana);
            Debug.Log(currentMana);
        }
    }

}
