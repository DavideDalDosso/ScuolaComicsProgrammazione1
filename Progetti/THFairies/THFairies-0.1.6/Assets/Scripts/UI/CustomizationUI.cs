using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationUI : BaseUI
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button applyButton;
    void Start()
    {
        
    }

    public override void Hide(Action callback)
    {
        gameObject.SetActive(false);
        callback?.Invoke();
    }

    public override void Show(Action callback)
    {
        gameObject.SetActive(true);
        callback?.Invoke();
    }
}
