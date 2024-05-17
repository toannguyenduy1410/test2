using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenu : UICanvas
{
    [SerializeField] private Button ButtonplayGame;
    [SerializeField] private Button Buttonsetting;

    private void Start()
    {
        //ButtonplayGame.onClick.AddListener(PlayGame);
        Buttonsetting.onClick.AddListener(Setting);
    }
    public void PlayGame(Action playAction)
    {                  
        ButtonplayGame.onClick.AddListener(() =>
        {            
             Close(0);
            UIManager.Instance.OpenUI<CanvasGamePlay>();           
            playAction.Invoke();
        });
    }
    public void Setting()
    {        
        UIManager.Instance.OpenUI<CanvasSetting>();
    }
}
