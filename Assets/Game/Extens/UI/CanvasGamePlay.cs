using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGamePlay : UICanvas
{  
    [SerializeField] private Button Buttonsetting;
    [SerializeField] private TextMeshProUGUI TextCoin;

    private void Start()
    {      
        Buttonsetting.onClick.AddListener(Setting);
    }
    public override void SetUp()
    {
        base.SetUp();
        UpdateCoin(0);
    }
    public void UpdateCoin(int coin)
    {
        TextCoin.text = coin.ToString();
    }

    public void Setting()
    {
        UIManager.Instance.OpenUI<CanvasSetting>();
    }
}
