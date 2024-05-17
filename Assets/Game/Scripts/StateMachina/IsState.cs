using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsState 
{
    void OnEnter(Enemyoan enemy);
    void OnExecute(Enemyoan enemy);
    void OnExit(Enemyoan enemy);
}
