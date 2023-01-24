using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetecter : MonoBehaviour
{
    [SerializeField] 
    private TowerSpawner towerSpawner;

    private Camera mainCam;
    private Ray ray;
    private RaycastHit hit;

    public Vector3 MousePos { get; set; }
    public Transform TileToSpawnTower { get; set; }

    private void Awake()
    {
        mainCam = Camera.main;
        MousePos = Vector3.zero;
        TileToSpawnTower = null;
    }

    public void SavePlace()
    {
        ray = mainCam.ScreenPointToRay(this.MousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 현재 ray가 지난 transform이 tile 태그를 달고, 타일에 타워가 건설 안되있다.
            if (hit.transform.CompareTag("Tile") && !hit.transform.GetComponent<Tile>().IsBuildTower)
            {
                towerSpawner.SetTowerPos(hit.transform);
                TileToSpawnTower = hit.transform;
            }
            MousePos = Vector3.zero;
        }
    }

    // 타워 포인트가 지어져있는곳에, 타워 짓기.
    public void GenerateTower(int cardHand)
    {
        towerSpawner.SpawnTower(this.TileToSpawnTower, cardHand);
        TileToSpawnTower = null;
    }
}

// 타일 디텍터 컴포넌트
// 프로퍼티 : towerSpawner 컴포넌트
// 필드 . 카메라, ray, raycast.