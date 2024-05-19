using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUI : BaseUI
{
    private bool multiplayerMode = false;
    void Start()
    {
        
    }

    public void Select(int level){

    }
    public void SetMultiplayerMode(bool multiplayerMode){
        this.multiplayerMode = multiplayerMode;
    }

    public override void Show(Action callback){
        gameObject.SetActive(true);
        callback?.Invoke();
    }

    public override void Hide(Action callback){
        gameObject.SetActive(false);
        callback?.Invoke();
    }
}
