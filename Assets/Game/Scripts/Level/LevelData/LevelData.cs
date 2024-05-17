using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class LevelData
{
    public int IDindex;
    public Sprite spriteLevel;
    public NavMeshData navMeshData;   
}

[Serializable]
public class LvSample
{
    public GameObject prefab;
    public NavMeshData navMeshData;
}