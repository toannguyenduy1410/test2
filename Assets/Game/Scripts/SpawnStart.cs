using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class SpawnStart : MonoBehaviour
{   

    private void Awake()
    {
      
    }  
    private void Start()
    {
        UIManager.Instance.OpenUI<CanvasMenu>();

    }   
}
