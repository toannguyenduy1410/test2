using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="level/levelSO")]
public class LevelSO : ScriptableObject
{
   public List<LevelData> listData;
    public List<LvSample> lvSamples;
}
