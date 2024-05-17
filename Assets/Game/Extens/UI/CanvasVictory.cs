using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasVictory : UICanvas
{
    [SerializeField] private Button ButtonMenu;
    [SerializeField] private Button Buttonnext;

    private void Start()
    {
        ButtonMenu.onClick.AddListener(MenuGame);
        Buttonnext.onClick.AddListener(Next);
    }
    public void MenuGame()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }
    public void Next() 
    {
     //   UIManager.Instance.OpenUI<CanvasMenu>();
    }
}
