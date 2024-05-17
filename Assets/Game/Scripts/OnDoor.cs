using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoor : MonoBehaviour
{
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
    private string currentAnim;
    private string currentAnim2;
    private bool isdoor = true;  
    public void ChangAnimDoor1(string animName)
    {
        if (currentAnim != animName)
        {           
            currentAnim = animName;
            door1.SetTrigger(currentAnim);
        }
    }public void ChangAnimDoor2(string animName)
    {
        if (currentAnim2 != animName)
        {           
            currentAnim2 = animName;
            door2.SetTrigger(currentAnim2);
        }
    }  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (isdoor == true)
            {
                ChangAnimDoor1("ondoor");
                ChangAnimDoor2("ondoor");
                isdoor = false;
            }
        }
    }
}
