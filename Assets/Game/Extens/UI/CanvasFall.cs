using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFall : UICanvas
{
    [SerializeField] private Button ButtonMenu;
    [SerializeField] private Button ButtonPlayOver;

    private void Start()
    {
        ButtonMenu.onClick.AddListener(MenuGame);
        ButtonPlayOver.onClick.AddListener(PlayOver);
    }
    public void MenuGame()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }
    public void PlayOver()
    {
        //   UIManager.Instance.OpenUI<CanvasMenu>();
    }
}
