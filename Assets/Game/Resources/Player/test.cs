using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
   public NavMeshAgent navMesh;
    void Update()
    {
        if (transform.position != null)
        {
            if (navMesh != null && navMesh.isOnNavMesh)
            {
                // NavMeshAgent đủ điều kiện để sử dụng SetDestination
                Debug.Log("đk đúng");
            }
            else
            {
                Debug.Log("NavMeshAgent không thể sử dụng SetDestination.");
            }

        }
    }
}
