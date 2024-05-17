using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSetting : UICanvas
{
    [SerializeField] private Button ButtonMenu;
  

    private void Start()
    {
        ButtonMenu.onClick.AddListener(MenuGame);   
    }
    public void MenuGame()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasMenu>();
    }   
}
