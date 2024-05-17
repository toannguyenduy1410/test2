using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float Speed;
    [SerializeField] private Vector3 vt;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position ,player.position + vt ,Speed * Time.deltaTime);
    }
}
