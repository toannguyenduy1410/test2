using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : SetMaterial
{
    public static Brick Instance;
    [SerializeField] private SetMaterial setMaterial;
    [SerializeField] private Renderer renderBrick;
    public ColeType colorBrick;
    private void Awake()
    {
        Instance = this;
       
    }   
    //private void Start()
    //{      
    //    colorBrick = (ColeType)Random.Range(0, 3);
    //    renderBrick.material = setMaterial.SetsMaterial(colorBrick);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (colorBrick == other.GetComponent<Player>().coler)
            {
                other.GetComponent<Player>().AddBrick();
                gameObject.SetActive(false);
            }
           
        }
        if (other.CompareTag("Enemy"))
        {
            if (colorBrick == other.GetComponent<Enemyoan>().coler)
            {
                other.GetComponent<Enemyoan>().AddBrick();
                gameObject.SetActive(false);
            }
        }
    }   
}
