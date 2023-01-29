using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetecter : MonoBehaviour
{
    [SerializeField] 
    private TowerSpawner towerSpawner;
    [SerializeField]
    private TowerViewer TowerViewer;

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
            // 타워 짓기 전, 타워 건설 위치 저장.
            if (hit.transform.CompareTag("Tile") && !hit.transform.GetComponent<Tile>().IsBuildTower)
            {
                towerSpawner.SetTowerPos(hit.transform);
                TileToSpawnTower = hit.transform;
            }

            MousePos = Vector3.zero;
        }
    }

    public void GenerateTower(int cardHand)
    {
        towerSpawner.SpawnTower(this.TileToSpawnTower, cardHand);
        TileToSpawnTower = null;
    }

    public void OnTowerClick(){
        ray = mainCam.ScreenPointToRay(this.MousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 타워 짓기 전, 타워 건설 위치 저장.
            if(hit.transform.CompareTag("Tower")){
                TowerViewer.OnPanel(hit.transform);
            }

            MousePos = Vector3.zero;
        }
        return;
    }

    public void OffTowerClick(){
        TowerViewer.OffPanel();
    }
}
