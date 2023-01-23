using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject towerPrefab;
    public void SpawnTower(Transform towerTransform)
    {

        if (!towerTransform.GetComponent<Tile>().IsBuildTower)
        {
            towerTransform.GetComponent<Tile>().IsBuildTower = true;
            Instantiate(towerPrefab, towerTransform.position, Quaternion.identity);
        }
       
    }
}
