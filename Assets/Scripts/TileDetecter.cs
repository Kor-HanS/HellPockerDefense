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
            // ���� ray�� ���� transform�� tile �±׸� �ް�, Ÿ�Ͽ� Ÿ���� �Ǽ� �ȵ��ִ�.
            if (hit.transform.CompareTag("Tile") && !hit.transform.GetComponent<Tile>().IsBuildTower)
            {
                towerSpawner.SetTowerPos(hit.transform);
                TileToSpawnTower = hit.transform;
            }
            MousePos = Vector3.zero;
        }
    }

    // Ÿ�� ����Ʈ�� �������ִ°���, Ÿ�� ����.
    public void GenerateTower(int cardHand)
    {
        towerSpawner.SpawnTower(this.TileToSpawnTower, cardHand);
        TileToSpawnTower = null;
    }
}

// Ÿ�� ������ ������Ʈ
// ������Ƽ : towerSpawner ������Ʈ
// �ʵ� . ī�޶�, ray, raycast.