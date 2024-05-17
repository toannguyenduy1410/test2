using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "coler/newcoler")]
public class colorSO : ScriptableObject
{
    public List<Material> colorList;    

}
public  enum ColeType { red = 0, green = 1, purple = 2 };
