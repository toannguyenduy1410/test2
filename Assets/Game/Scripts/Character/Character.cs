using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    public static Character Instance;
    [SerializeField] private Animator anim;
    [SerializeField] protected float rotationSpess;
    [SerializeField] protected GameObject prifabBrick;
    [SerializeField] protected BrickCau prifabBrick_Cau;

    [SerializeField] protected GameObject listBrickFloor;
    [SerializeField] protected SetMaterial setMaterial;
    [SerializeField] protected Renderer render;
    [SerializeField] protected Renderer renderBrick_Cau;
    [SerializeField] protected Vector3 StartPoit;

    public ColeType coler;

    protected bool isDoor = false;
    public bool isPlayGame = false;

    protected float brickLapUp = 0;
    public int SLlistBrik = 0;
    public int currentFLoor = 10;
    protected int Speed = 8;
    private string currentAnim;
    protected CapsuleCollider boxCollider;
    protected Rigidbody rb;
    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {
        OnInit();
        OnDesPawn();
        transform.position = StartPoit;
        listBrickFloor = GameObject.FindGameObjectWithTag("listFloor");
    }
    private void OnDisable()
    {

    }
    public virtual void OnInit()
    {
        SetColerPlayer(coler);
        boxCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();

        boxCollider.enabled = false;
        rb.isKinematic = true;
        //khi nhẫn play
        UIManager.Instance.GetUI<CanvasMenu>().PlayGame(StartGame);
        // khi nhẫn level
        LevelSelection.Instance.LevlePLay -= StartGame;
        LevelSelection.Instance.LevlePLay += StartGame;

    }
    private void Update()
    {

    }
    public virtual void StartGame()
    {
        boxCollider.enabled = true;
        rb.isKinematic = false;
        isPlayGame = true;
    }
    public virtual void OnDesPawn()
    {

    }
    protected void SetColerPlayer(ColeType colertype)
    {
        if (render != null)
        {
            // Đặt vật liệu hiện tại thành null để reset
            render.material = null;

            // Lấy Renderer của người chơi           
            render.material = setMaterial.SetsMaterial(colertype);
        }
    }


    protected void AddBrickFloor(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            int idFloor = collision.gameObject.GetComponent<IDPlan>().iDFloor;
            if (currentFLoor != idFloor)
            {
                Transform boxParent = collision.transform;
                BoxCollider boxCollider = boxParent.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    float width = boxCollider.size.x;
                    float length = boxCollider.size.z;
                    float area = width * length;
                    float result = area / 3;
                    // Làm tròn giá trị result về số nguyên gần nhất
                    int CountBrick = Mathf.RoundToInt(result) / 5;
                    Vector3 boxSize = boxCollider.size;
                    Vector3 boxCenter = boxCollider.bounds.center;
                    collision.gameObject.GetComponent<Floor>().SpawnBricks(boxCenter, boxSize, coler, CountBrick);
                    currentFLoor = idFloor;
                }
            }
        }
    }
    protected void ChangAnim(string animName)
    {
        if (currentAnim != animName)
        {
            //  anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }


    protected void WinUIGame()
    {

        UIManager.Instance.OpenUI<CanvasVictory>();
    }
    protected void LossUIGame()
    {
        UIManager.Instance.OpenUI<CanvasFall>();
    }

}
