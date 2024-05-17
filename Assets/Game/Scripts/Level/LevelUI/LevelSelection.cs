using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class LevelSelection : Singleton<LevelSelection>
{
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private Transform parentLevel;
    [SerializeField] private Transform parentMap;
    [SerializeField] private LevelSO levelSO;
    [SerializeField] private List<GameObject> listObj;
    [SerializeField] private GameObject enemy;
    public int curentLevel = 1;

    public Action LevlePLay;
    private void Awake()
    {
        //LoadLevel();
        LevelActionHandel(curentLevel);
    }
   
    private void SpawnLevel(string textIndex, Sprite spritelevel, NavMeshData navMeshData)
    {
        LevelUI prifab = Instantiate(levelUI, parentLevel);
        prifab.OnInit(textIndex, spritelevel, LevelActionHandel, navMeshData);
    }
    public void LevelActionHandel(int Index)
    {
        //for (int i = 0; i < listObj.Count; i++)
        //{
        //    Instantiate(listObj[i]);
        //}

        //GameObject mapPrifab = Resources.Load<GameObject>($"Map/Map{Index}");
        //GameObject newMap = Instantiate(mapPrifab, parentMap);     \
        GameObject newMap = Instantiate(levelSO.lvSamples[Index].prefab, parentMap);      
        if (NavMesh.SamplePosition(new Vector3(0, 14, -8), out NavMeshHit hit, 10, NavMesh.AllAreas))
        {
            GameObject enem = Instantiate(enemy, hit.position, Quaternion.identity);
            Debug.Log(1, enem);
        }
        LevlePLay?.Invoke();    
    }       
    private void LoadLevel()
    {
        for (int i = 0; i < levelSO.listData.Count; i++)
        {
            SpawnLevel(levelSO.listData[i].IDindex.ToString(), levelSO.listData[i].spriteLevel, levelSO.listData[i].navMeshData);
        }
    }

}
