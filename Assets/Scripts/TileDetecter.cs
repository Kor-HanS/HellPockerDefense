using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetecter : MonoBehaviour
{
    [SerializeField] 
    TowerSpawner towerSpawner;

    private Camera mainCam;
    private Ray ray;
    private RaycastHit hit;

    private Vector3 mousePos = Vector3.zero;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    void GenerateTower(Vector3 mousePos)
    {
        ray = mainCam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Tile"))
            {
                towerSpawner.SpawnTower(hit.transform);
            }
        }
    }




}