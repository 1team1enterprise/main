using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider hpbar;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpbar.maxValue = PlayerController.instance.maxHealth;
        hpbar.value = PlayerController.instance.curHealth;
        HandleHp();
    }

    public void HandleHp()
    {
        hpbar.value = PlayerController.instance.curHealth;
    }
}
