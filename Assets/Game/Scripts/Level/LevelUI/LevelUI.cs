using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public static LevelUI Instances;
    [SerializeField] private TextMeshProUGUI textIndex;
    [SerializeField] private Image imageLevel;
    [SerializeField] private Button buttonLevel;  
    
    public NavMeshDataInstance navMeshInstance;
   
    private void Awake()
    {
        Instances = this;
    }
    public void OnInit(string textIndex, Sprite spritelevel , Action<int> levelbuttonClick, NavMeshData navMeshData)
    {
        this.textIndex.text = textIndex;
        this.imageLevel.sprite = spritelevel;
        if (navMeshData != null)
        {
            navMeshInstance = NavMesh.AddNavMeshData(navMeshData);           
        }
        buttonLevel.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseUI<CanvasMenu>(0);
            UIManager.Instance.OpenUI<CanvasGamePlay>();
            levelbuttonClick.Invoke(int.Parse(textIndex));                               
        });
    }   
}
