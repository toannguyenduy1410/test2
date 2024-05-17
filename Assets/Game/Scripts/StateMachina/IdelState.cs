using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdelState : IsState
{
    private float timer;
    private float Randomtimer;
    public void OnEnter(Enemyoan enemy)
    {
       
      
    }
    public void OnExecute(Enemyoan enemy)
    {
       
        if (enemy.isWinSpos == false)
        {
            if (enemy.isPlayGame)
            {
                enemy.ChangState(new MovingSate());
            }
                      
        }        
    }

    public void OnExit(Enemyoan enemy)
    {

    }

}
