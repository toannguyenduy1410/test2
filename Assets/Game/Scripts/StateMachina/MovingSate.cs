using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSate : IsState
{   
    private int Randombrick;
    public void OnEnter(Enemyoan enemy)
    {
        Randombrick = (int)Random.Range(3, 9);
    }
    public void OnExecute(Enemyoan enemy)
    {
        if (enemy.SLlistBrik > Randombrick)
        {
            enemy.navMesh.velocity = Vector3.zero;
            enemy.ChangState(new MovingStateStair());
        }
        else
        {          
            enemy.Moving();           
            enemy.findBrick();
        }
    }
    public void OnExit(Enemyoan enemy)
    {

    }

}
