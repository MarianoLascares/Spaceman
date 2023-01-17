using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType { manaBar, healthBar};
public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    public BarType type;

    // Start is called before the first frame update
        GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        slider = GetComponent<Slider>();

        switch (type)
        {
            case BarType.manaBar:
                slider.value = 15;
                slider.maxValue = PlayerController.MAX_MANA;
                break;
            case BarType.healthBar:
                slider.maxValue = PlayerController.MAX_HEALTH;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BarType.manaBar:
                slider.value = player.GetComponent<PlayerController>().GetMana();
                break;
            case BarType.healthBar:
                slider.value = player.GetComponent<PlayerController>().GetHealth();
                break;
        } 
    }
}
