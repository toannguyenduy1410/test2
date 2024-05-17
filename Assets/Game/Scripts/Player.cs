using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : Character
{      
    [SerializeField] private Transform prentPlayerBrick;
    [SerializeField] private GameObject listBrickPlayerPrent;
    [SerializeField] private GameObject prifabJoystich;
    private Transform parentJoystich;
  
    public DynamicJoystick joystick;
    private bool isjoystick = true;
    private Vector3 moveDirection;
    private void Awake()
    {
        // Tạo joystick từ prefab
        GameObject joystickObject = Instantiate(prifabJoystich, parentJoystich);
        // Nếu bạn cần truy cập vào component của joystick, bạn có thể làm như sau
        joystick = joystickObject.GetComponent<DynamicJoystick>();
        joystickObject.transform.SetParent(parentJoystich);
    }
    
    private void Update()
    {
        MoveContro();
    }
    public override void OnInit()
    {
        base.OnInit();
        EnebJoystick();         
    }
    public override void OnDesPawn()
    {
        base.OnDesPawn();
    }
    public override void StartGame()
    {
        base.StartGame();      
    }
    private void EnebJoystick()
    {

        isjoystick = true;
    }

    private void MoveContro()
    {
        if (isjoystick == true)
        {
            moveDirection = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);

            if (moveDirection.z < -0.5f && isDoor == false)
            {
                this.Speed = 8;
            }
            if (moveDirection.z > 0.01f && isDoor == true)
            {
                this.Speed = 8;
                isDoor = false;
            }
            if (moveDirection.sqrMagnitude > 0.01f) // Kiểm tra di chuyển đủ lớn để xử lý
            {
                ChangAnim("Run");
                Vector3 newPosition = transform.position + moveDirection * Speed * Time.deltaTime;
                transform.position = newPosition;
                var targetDirection = Vector3.RotateTowards(transform.forward, moveDirection, rotationSpess * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(targetDirection);
            }
            if (moveDirection.sqrMagnitude == 0)
            {
                ChangAnim("Idel");
            }
        }
    }
    //private bool isRayBrick()
    //{
    //    RaycastHit hit;
    //    var curentpos = transform.position + Vector3.up * 2f + transform.forward * 1f;
    //    var curentexit = transform.position + transform.forward * 1f;
    //    Debug.DrawLine(curentpos, curentexit, UnityEngine.Color.yellow);
    //    if (Physics.Raycast(curentpos, curentexit, out hit, 5f))
    //    {
    //        if (hit.collider.gameObject.tag == "Un_Brick")
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    public void AddBrick()
    {
        GameObject brick = Instantiate(prifabBrick, prentPlayerBrick);
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
        int slbrick = listBrickPlayerPrent.transform.childCount;
        if (slbrick > 0)
        {
            GameObject brick = listBrickPlayerPrent.transform.GetChild(slbrick - 1).gameObject;
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
        if (moveDirection.z > 0.01f)
        {
            if (other.CompareTag("Un_Brick"))
            {
                if (coler != other.GetComponent<BrickCau>().coleBrickCau)
                {
                    if (SLlistBrik <= 0)
                    {
                        this.Speed = 0;
                    }
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

                        if (SLlistBrik <= 0)
                        {
                            this.Speed = 0;
                        }
                        if (SLlistBrik > 0)
                        {
                            // Tạo cau thang
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
    }

    private void StopPlayerDoor(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            if (moveDirection.z < 0f)
            {
                this.Speed = 0;
                isDoor = true;
            }
        }
    }

    private void WinGame(Collider other)
    {
        if (other.CompareTag("FloorWin"))
        {
            ChangAnim("Victory");
            isjoystick = false;
            WinUIGame();
            Destroy(listBrickPlayerPrent);
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayGame)
        {
            AddUnBrick(other);
            StopPlayerDoor(other);
            WinGame(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPlayGame)
        {
            AddBrickFloor(collision);
            
        }
    }
}

