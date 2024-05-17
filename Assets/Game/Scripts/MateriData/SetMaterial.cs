using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    public colorSO colorSO;
    
    public Material SetsMaterial(ColeType color)
    {
        return colorSO.colorList[(int)color];       
    }
}

