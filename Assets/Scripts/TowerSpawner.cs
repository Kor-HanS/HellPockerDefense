using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject towerPrefab;
    [SerializeField]
    GameObject pointPrefab;

    public GameObject TowerPoint { get; set; }

    private void Awake()
    {
        TowerPoint = null;
    }

    public void SetTowerPos(Transform savePosTransform)
    {
        // 타워 포인트 찍기.
        TowerPoint = Instantiate(pointPrefab, savePosTransform.position, Quaternion.identity);
    }

    public void SpawnTower(Transform towerTransform, int cardHand)
    {
        // 타워 포인트 있으면, 타워 짓기.
        if (TowerPoint)
        {
            Destroy(TowerPoint);
            towerTransform.GetComponent<Tile>().IsBuildTower = true;
            GameObject tower = Instantiate(towerPrefab, towerTransform.position, Quaternion.identity);
            this.TowerPoint = null;

            // 타워 정보 주기.
            switch (cardHand)
            {
                case 0:
                    // 플러쉬
                    tower.GetComponent<Tower>().Damage = 75;
                    break;
                case 1:
                    // 로티플
                    tower.GetComponent<Tower>().Damage = 180;
                    break;
                case 2:
                    // 백스플
                    tower.GetComponent<Tower>().Damage = 160;
                    break;
                case 3:
                    // 스트플
                    tower.GetComponent<Tower>().Damage = 140;
                    break;
                case 4:
                    // 파카풀
                    tower.GetComponent<Tower>().Damage = 120;
                    break;
                case 5:
                    // 파이브카드
                    tower.GetComponent<Tower>().Damage = 100;
                    break;
                case 6:
                    // 마운틴
                    tower.GetComponent<Tower>().Damage = 80;
                    break;
                case 7:
                    // 포카
                    tower.GetComponent<Tower>().Damage = 70;
                    break;
                case 8:
                    // 백스트레이트
                    tower.GetComponent<Tower>().Damage = 65;
                    break;
                case 9:
                    // 스트레이트
                    tower.GetComponent<Tower>().Damage = 60;
                    break;
                case 10:
                    // 풀하우스
                    tower.GetComponent<Tower>().Damage = 50;
                    break;
                case 11:
                    // 트리플
                    tower.GetComponent<Tower>().Damage = 40;
                    break;
                case 12:
                    // 투페어
                    tower.GetComponent<Tower>().Damage = 30;
                    break;
                case 13:
                    // 원페어
                    tower.GetComponent<Tower>().Damage = 20;
                    break;
                case 14:
                    // 탑
                    tower.GetComponent<Tower>().Damage = 10;
                    break;

            }
            Debug.Log(tower.GetComponent<Tower>().Damage);
        }
        else
        {
            Debug.Log("위치를 결정하세요.");
        }
    }


}
