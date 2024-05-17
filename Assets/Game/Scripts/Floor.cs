using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class Floor : MonoBehaviour
{
    //[SerializeField] private SetMaterial setMaterial;      
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Transform parenPosition;  
    [SerializeField] private SetMaterial setMaterial;    

    void Start()
    {
        OnInit();
    }
    //private void InstanceBricks()
    //{
    //    BoxCollider boxCollider = boxParen.GetComponent<BoxCollider>();     
    //    Vector3 boxSize = boxCollider.size;

    //    float brickSize = 2f;
    //    int bricks=0;

    //    // Tạo một lưới viên gạch
    //    for (float z = -boxSize.z / 2; z < boxSize.z / 2; z += brickSize)
    //    {
    //        for (float x = -boxSize.x / 2; x < boxSize.x / 2; x += brickSize)
    //        {
    //            if(bricks > 10)
    //            {
    //                return;
    //            }
    //            // Tính toán vị trí của viên gạch
    //            Vector3 brickPosition = new Vector3(x, 0, z);

    //            // Tạo viên gạch
    //            GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity);

    //            // Gán cha cho viên gạch
    //            brick.transform.parent = parenPosition;
    //            bricks++;
    //        }
    //    }
    //}
    [ContextMenu("onInit")]
    public void OnInit()
    {
      //  InstanceBricks(10,ColeType.red);
    }
  private void BoxSai()
    {
      
    }
    //private List<Vector3> usedPositions = new List<Vector3>(); // Danh sách các vị trí đã được sử dụng

    //public void InstanceBricks(int numberOfBricksToCreate, ColeType coleType, Vector3 collisionBoxPosition, Vector3 boxSize)
    //{
    //    int bricksCreated = 0;

    //    // Tạo một lưới viên gạch
    //    float distanceBetweenBricks = 2f; // Khoảng cách giữa mỗi viên gạch
    //    while (bricksCreated < numberOfBricksToCreate)
    //    {
    //        // Sinh ngẫu nhiên vị trí của viên gạch trong mặt phẳng x-z của hộp
    //        float randomX = Random.Range(-boxSize.x / 2, boxSize.x / 2);
    //        float randomZ = Random.Range(-boxSize.z / 2, boxSize.z) / 2;

    //        // Điều chỉnh vị trí để nằm trên lưới với khoảng cách 2f
    //        float adjustedX = Mathf.Round(randomX / distanceBetweenBricks) * distanceBetweenBricks;//làm tròn số
    //        float adjustedZ = Mathf.Round(randomZ / distanceBetweenBricks) * distanceBetweenBricks;

    //        // Tính toán vị trí cuối cùng của viên gạch
    //        Vector3 brickPosition = new Vector3(adjustedX, 0, adjustedZ);

    //        // Kiểm tra xem vị trí mới có trong danh sách đã sử dụng chưa
    //        if (!usedPositions.Contains(brickPosition))
    //        {
    //            // Tạo viên gạch
    //            GameObject brick = Instantiate(brickPrefab, brickPosition + collisionBoxPosition, Quaternion.identity);
    //            Renderer renderBrick = brick.GetComponent<Renderer>();
    //            renderBrick.material = setMaterial.SetsMaterial(coleType);
    //            Brick.Instance.colorBrick = coleType;
    //            // Gán cha cho viên gạch
    //            brick.transform.parent = parenPosition;

    //            // Thêm vị trí mới vào danh sách đã sử dụng
    //            usedPositions.Add(brickPosition);
    //            // Tăng biến đếm
    //            bricksCreated++;
    //        }
    //    }
    //}

    private List<Vector3> createdPositions = new List<Vector3>();

    private List<Vector3> GenerateRandomPositions(Vector3 boxCenter, Vector3 boxSize, int numberOfBricks)
    {
        List<Vector3> randomPositions = new List<Vector3>();

        int gridCountX = Mathf.FloorToInt(boxSize.x / 1.5f);
        int gridCountZ = Mathf.FloorToInt(boxSize.z / 1.5f);

        while (randomPositions.Count < numberOfBricks)
        {
            float xPos = (Random.Range(-gridCountX, gridCountX) * 1.5f) + boxCenter.x;
            float zPos = (Random.Range(-gridCountZ, gridCountZ) * 1.5f) + boxCenter.z;
            float yPos = boxCenter.y;

            Vector3 randomPosition = new Vector3(xPos, yPos, zPos);

            // Kiểm tra xem vị trí mới có nằm trong phạm vi của box không
            if (IsInsideBox(randomPosition, boxCenter, boxSize) && !createdPositions.Contains(randomPosition))
            {
                // Nếu không trùng và nằm trong phạm vi của box, thêm vào danh sách và danh sách các vị trí đã tạo ra
                randomPositions.Add(randomPosition);
                createdPositions.Add(randomPosition);
            }
        }

        return randomPositions;
    }   
    private bool IsInsideBox(Vector3 position, Vector3 boxCenter, Vector3 boxSize)
    {
        // Kiểm tra xem vị trí có nằm trong phạm vi của box không
        if (Mathf.Abs(position.x - boxCenter.x) <= boxSize.x / 2f &&
            Mathf.Abs(position.z - boxCenter.z) <= boxSize.z / 2f &&
            Mathf.Abs(position.y - boxCenter.y) <= boxSize.y / 2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Hàm trộn ngẫu nhiên danh sách
    private List<Vector3> ShuffleList(List<Vector3> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public void SpawnBricks(Vector3 boxCenter, Vector3 boxSize, ColeType coleType,int numberOfBricks)
    {
        List<Vector3> gridPositions = GenerateRandomPositions(boxCenter, boxSize, numberOfBricks);

        foreach (Vector3 gridPosition in gridPositions)
        {
            // Tạo viên gạch tại vị trí ô lưới
            GameObject brick = Instantiate(brickPrefab, gridPosition, Quaternion.identity, parenPosition);
            Renderer renderBrick = brick.GetComponent<Renderer>();
            renderBrick.material = setMaterial.SetsMaterial(coleType);
            Brick.Instance.colorBrick = coleType;
        }
    }
}