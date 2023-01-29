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
                    towerInstance.TowerCardResult = CardHand.Flush;
                    towerInstance.Damage = 75;
                    break;
                case 1:
                    towerInstance.TowerCardResult = CardHand.RoyalStraightFlush;
                    towerInstance.Damage = 180;
                    towerInstance.ShootRange = 5;
                    break;
                case 2:
                    towerInstance.TowerCardResult = CardHand.BackStraightFlush;
                    towerInstance.Damage = 160;
                    towerInstance.ShootRange = 5;
                    break;
                case 3:
                    towerInstance.TowerCardResult = CardHand.StraightFlush;
                    towerInstance.Damage = 140;
                    towerInstance.ShootRange = 5;
                    break;
                case 4:
                    towerInstance.TowerCardResult = CardHand.FiveCardFlush;
                    towerInstance.Damage = 120;
                    towerInstance.ShootRange = 5;
                    break;
                case 5:
                    towerInstance.TowerCardResult = CardHand.FiveCard;
                    towerInstance.Damage = 100;
                    towerInstance.ShootRange = 5;
                    break;
                case 6:
                    towerInstance.TowerCardResult = CardHand.Mountain;
                    towerInstance.Damage = 80;
                    break;
                case 7:
                    towerInstance.TowerCardResult = CardHand.FourCard;
                    towerInstance.Damage = 70;
                    break;
                case 8:
                    towerInstance.TowerCardResult = CardHand.BackStraight;
                    towerInstance.Damage = 65;
                    break;
                case 9:
                    towerInstance.TowerCardResult = CardHand.Straight;
                    towerInstance.Damage = 60;
                    break;
                case 10:
                    towerInstance.TowerCardResult = CardHand.FullHouse;
                    towerInstance.Damage = 50;
                    break;
                case 11:
                    towerInstance.TowerCardResult = CardHand.Triple;
                    towerInstance.Damage = 40;
                    break;
                case 12:
                    towerInstance.TowerCardResult = CardHand.TwoPair;
                    towerInstance.Damage = 30;
                    break;
                case 13:
                    towerInstance.TowerCardResult = CardHand.OnePair;
                    towerInstance.Damage = 20;
                    break;
                case 14:
                    towerInstance.TowerCardResult = CardHand.Top;
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
