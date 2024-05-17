using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemyoan : Character
{
    public NavMeshAgent navMesh;
    private IsState currentState;

    [SerializeField] private LayerMask layerBrick;
    [SerializeField] private LayerMask layerUnBrick;
    [SerializeField] private LayerMask layerFloor;
    [SerializeField] private Transform prentEnemyBrick;
    [SerializeField] private GameObject listBrickEnemyPrent;
    [SerializeField] private GameObject positionWinSpos;

    public Transform nearestBrick;
    private float diameter = 200f;

    public bool isStair = false;
    public bool isFloor = false;
    public bool isWinSpos = false;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();

        ChangState(new IdelState());

        positionWinSpos = GameObject.Find("Win");
        // Lấy tham chiếu đến Box Collider của GameObject
    }
    public override void OnDesPawn()
    {
        base.OnDesPawn();
    }
    public override void StartGame()
    {
        base.StartGame();
    }
    public void ChangState(IsState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Moving()
    {
        //if (!navMesh.isOnNavMesh)
        //{
        //    Debug.LogError("NavMeshAgent không được đặt trên NavMesh.");
        //    return;
        //}
        // Định nghĩa một biến để lưu trữ vị trí đặt NavMeshAgent
        //        



        if (nearestBrick != null)
        {
            if (navMesh.isOnNavMesh)
            {
                // NavMeshAgent đủ điều kiện để sử dụng SetDestination
                navMesh.SetDestination(nearestBrick.position);
            }
            else
            {
                Debug.Log("NavMeshAgent không thể sử dụng SetDestination.");
            }
        }
        ChangAnim("Run");
    }
    public void finFloor()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, diameter, layerFloor);
        float nearestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            // Chỉ gọi GetComponent một lần cho mỗi vật phẩm
            IDPlan floor = collider.GetComponent<IDPlan>();
            if (floor != null && floor.iDFloor == currentFLoor + 1)
            {
                // Lấy vị trí của Collider một lần
                Vector3 colliderPosition = collider.transform.position;

                float distance = Vector3.Distance(transform.position, colliderPosition);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestBrick = collider.transform;
                    // Sử dụng Equals để so sánh Vector3 vì không thể so sánh trực tiếp vị trí Vector3
                    if (transform.position.Equals(colliderPosition))
                    {
                        isFloor = true;
                        return;
                    }
                    Debug.Log(nearestBrick);
                }
            }
        }
        isFloor = false;
    }

    public void finStair()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, diameter, layerUnBrick);
        if (colliders.Length == 0)
        {
            isStair = true;
            return;
        }
        float nearestDistance = Mathf.Infinity;
        bool foundStair = false;
        foreach (Collider collider in colliders)
        {
            BrickCau brickComponent = collider.GetComponent<BrickCau>();
            if (brickComponent != null && coler == brickComponent.coleBrickCau)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestBrick = collider.transform;

                    // Kiểm tra ngưỡng khoảng cách
                    float threshold = 0.0f;
                    if (distance < threshold)
                    {
                        foundStair = true;
                        Debug.Log("1");
                        break; // Thoát khỏi vòng lặp ngay khi tìm thấy cầu thang
                    }
                }
            }
        }

        // Nếu không tìm thấy cầu thang trong vòng lặp, đặt isStair = true
        isStair = foundStair;
        Debug.Log(isStair);

    }

    public void findBrick()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, diameter, layerBrick);

        float nearestDistance = Mathf.Infinity;
        foreach (Collider collider in colliders)
        {
            Brick brickComponent = collider.GetComponent<Brick>(); // Lấy component Brick từ Collider
            if (brickComponent != null && coler == brickComponent.colorBrick)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestBrick = collider.transform;
                }
            }
        }
    }
    public void PositionWinSpos()
    {
        nearestBrick.position = positionWinSpos.transform.position;
    }
    public void AddBrick()
    {
        GameObject brick = Instantiate(prifabBrick, prentEnemyBrick);
        //setMaterial
        Renderer render = brick.GetComponent<Renderer>();
        render.material = setMaterial.SetsMaterial(coler);
        //tăng size
        Vector3 brickSize = brick.GetComponent<Renderer>().bounds.size;
        brick.transform.localPosition = new Vector3(0, brickLapUp + brickSize.y, 0);
        brickLapUp += brickSize.y;
        SLlistBrik++;
    }
    protected void RemoveBrick()
    {
        int slbrick = listBrickEnemyPrent.transform.childCount;
        if (slbrick > 0)
        {
            GameObject brick = listBrickEnemyPrent.transform.GetChild(slbrick - 1).gameObject;
            Vector3 sizebrick = brick.GetComponent<Renderer>().bounds.size;
            Destroy(brick);
            brickLapUp -= sizebrick.y;
            SLlistBrik--;
        }
        for (int i = 0; i < listBrickFloor.transform.childCount; i++)
        {
            GameObject child = listBrickFloor.transform.GetChild(i).gameObject;
            if (!child.activeSelf && child.GetComponent<Brick>().colorBrick == coler)
            {
                child.SetActive(true);
                break;
            }
        }
    }
    private void AddUnBrick(Collider other)
    {
        if (other.CompareTag("Un_Brick"))
        {
            if (coler != other.GetComponent<BrickCau>().coleBrickCau)
            {
                if (SLlistBrik > 0)
                {
                    other.GetComponent<BrickCau>().coleBrickCau = coler;
                    Renderer brickRender = other.GetComponent<Renderer>();
                    brickRender.material = setMaterial.SetsMaterial(coler);
                    RemoveBrick();
                }
            }
        }
        if (other.CompareTag("BoxBridge"))
        {
            Ray ray = new Ray(other.transform.position + Vector3.up * 2f, Vector3.down);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 3, UnityEngine.Color.red, 1000f);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (!hitInfo.collider.CompareTag("Un_Brick"))
                {
                    if (SLlistBrik > 0)
                    {
                        BrickCau brickcau = Instantiate(prifabBrick_Cau);
                        brickcau.transform.position = other.transform.position;
                        brickcau.coleBrickCau = coler;
                        Renderer brickRender = brickcau.GetComponent<Renderer>();
                        brickRender.material = setMaterial.SetsMaterial(coler);
                        RemoveBrick();
                    }
                }
            }
        }


    }
    public void WinGame(Collider other)
    {
        if (isWin(other))
        {
            ChangAnim("Victory");
            isWinSpos = true;
            WinUIGame();
            Destroy(listBrickEnemyPrent);
        }
    }
    public bool isWin(Collider other)
    {
        return other.CompareTag("FloorWin");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayGame)
        {
            AddUnBrick(other);
            WinGame(other);
        }
        //StopPlayerDoor(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayGame)
        {
            AddBrickFloor(collision);
        }
    }
}
