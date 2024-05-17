using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class MovingStateStair : IsState
{    
    public void OnEnter(Enemyoan enemy)
    {

    }
    public void OnExecute(Enemyoan enemy)
    {
        if (enemy.SLlistBrik <= 0)
        {
            enemy.navMesh.velocity = Vector3.zero;
            enemy.ChangState(new MovingSate());            
        }
        else
        {
            enemy.Moving();
            // enemy.finFloor();           
            enemy.PositionWinSpos();
            if(enemy.isWinSpos)
            {
                enemy.ChangState(new IdelState());                              
            }
        }
    }
   

    public void OnExit(Enemyoan enemy)
    {

    }
}

