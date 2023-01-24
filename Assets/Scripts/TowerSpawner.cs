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
        // Ÿ�� ����Ʈ ���.
        TowerPoint = Instantiate(pointPrefab, savePosTransform.position, Quaternion.identity);
    }

    public void SpawnTower(Transform towerTransform, int cardHand)
    {
        // Ÿ�� ����Ʈ ������, Ÿ�� ����.
        if (TowerPoint)
        {
            Destroy(TowerPoint);
            towerTransform.GetComponent<Tile>().IsBuildTower = true;
            GameObject tower = Instantiate(towerPrefab, towerTransform.position, Quaternion.identity);
            this.TowerPoint = null;

            // Ÿ�� ���� �ֱ�.
            switch (cardHand)
            {
                case 0:
                    // �÷���
                    tower.GetComponent<Tower>().Damage = 75;
                    break;
                case 1:
                    // ��Ƽ��
                    tower.GetComponent<Tower>().Damage = 180;
                    break;
                case 2:
                    // �齺��
                    tower.GetComponent<Tower>().Damage = 160;
                    break;
                case 3:
                    // ��Ʈ��
                    tower.GetComponent<Tower>().Damage = 140;
                    break;
                case 4:
                    // ��īǮ
                    tower.GetComponent<Tower>().Damage = 120;
                    break;
                case 5:
                    // ���̺�ī��
                    tower.GetComponent<Tower>().Damage = 100;
                    break;
                case 6:
                    // ����ƾ
                    tower.GetComponent<Tower>().Damage = 80;
                    break;
                case 7:
                    // ��ī
                    tower.GetComponent<Tower>().Damage = 70;
                    break;
                case 8:
                    // �齺Ʈ����Ʈ
                    tower.GetComponent<Tower>().Damage = 65;
                    break;
                case 9:
                    // ��Ʈ����Ʈ
                    tower.GetComponent<Tower>().Damage = 60;
                    break;
                case 10:
                    // Ǯ�Ͽ콺
                    tower.GetComponent<Tower>().Damage = 50;
                    break;
                case 11:
                    // Ʈ����
                    tower.GetComponent<Tower>().Damage = 40;
                    break;
                case 12:
                    // �����
                    tower.GetComponent<Tower>().Damage = 30;
                    break;
                case 13:
                    // �����
                    tower.GetComponent<Tower>().Damage = 20;
                    break;
                case 14:
                    // ž
                    tower.GetComponent<Tower>().Damage = 10;
                    break;

            }
            Debug.Log(tower.GetComponent<Tower>().Damage);
        }
        else
        {
            Debug.Log("��ġ�� �����ϼ���.");
        }
    }


}
