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
        TowerPoint = Instantiate(pointPrefab, savePosTransform.position, Quaternion.identity);
    }

    public void SpawnTower(Transform towerTransform, int cardHand)
    {
        if (TowerPoint)
        {
            Destroy(TowerPoint);
            towerTransform.GetComponent<Tile>().IsBuildTower = true;
            GameObject towerObj = Instantiate(towerPrefab, towerTransform.position, Quaternion.identity);
            Tower towerInstance = towerObj.GetComponent<Tower>();
            towerInstance.Level = 1;
            towerInstance.ShootRange = 3;
            towerInstance.ShootSpeed = 6;
            this.TowerPoint = null;

            switch (cardHand)
            {
                case 0:
                    // �÷���
                    towerInstance.Damage = 75;
                    break;
                case 1:
                    // ��Ƽ��
                    towerInstance.Damage = 180;
                    towerInstance.ShootRange = 5;
                    break;
                case 2:
                    // �齺��
                    towerInstance.Damage = 160;
                    towerInstance.ShootRange = 5;
                    break;
                case 3:
                    // ��Ʈ��
                    towerInstance.Damage = 140;
                    towerInstance.ShootRange = 5;
                    break;
                case 4:
                    // ��īǮ
                    towerInstance.Damage = 120;
                    towerInstance.ShootRange = 5;
                    break;
                case 5:
                    // ���̺�ī��
                    towerInstance.Damage = 100;
                    towerInstance.ShootRange = 5;
                    break;
                case 6:
                    // ����ƾ
                    towerInstance.Damage = 80;
                    break;
                case 7:
                    // ��ī
                    towerInstance.Damage = 70;
                    break;
                case 8:
                    // �齺Ʈ����Ʈ
                    towerInstance.Damage = 65;
                    break;
                case 9:
                    // ��Ʈ����Ʈ
                    towerInstance.Damage = 60;
                    break;
                case 10:
                    // Ǯ�Ͽ콺
                    towerInstance.Damage = 50;
                    break;
                case 11:
                    // Ʈ����
                    towerInstance.Damage = 40;
                    break;
                case 12:
                    // �����
                    towerInstance.Damage = 30;
                    break;
                case 13:
                    // �����
                    towerInstance.Damage = 20;
                    break;
                case 14:
                    // ž
                    towerInstance.Damage = 10;
                    break;
            }
        }
        else
        {
            Debug.Log("없는 패입니다.");
        }
    }


}
