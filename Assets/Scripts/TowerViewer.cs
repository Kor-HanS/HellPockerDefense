using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TowerViewer : MonoBehaviour
{
    [SerializeField]
    private GameObject towerInfoPanel;
    
    [SerializeField]
    private TMP_Text towerLevelTextUI;
    [SerializeField]
    private TMP_Text towerRangeTextUI;
    [SerializeField]
    private TMP_Text towerDamageTextUI;
    [SerializeField]
    private TMP_Text towerShootingRTextUI;
    [SerializeField]
    private TMP_Text towerCardTextUI; 
    [SerializeField]
    private Transform towerRangeUI;

    private void Awake(){
        towerInfoPanel.SetActive(false);
    }
    

    public void OnPanel(Transform towerTransform){

        if(!towerInfoPanel.activeSelf){
            towerInfoPanel.SetActive(true);
        }

        Tower nowTower = towerTransform.GetComponent<Tower>();
        string towerLevelText = string.Format("Level : {0:D2}", nowTower.Level);
        string towerRangeText = string.Format("Range : {0:0.0}", nowTower.ShootRange);
        string towerDmgText = string.Format("Damage : {0:0}", nowTower.Damage);
        string towerRateText = string.Format("Shooting Rate : {0:0.0}", nowTower.ShootSpeed);
        string towerCardText = "Card : " + nowTower.TowerCardResult.ToString();

        towerLevelTextUI.SetText(towerLevelText);
        towerRangeTextUI.SetText(towerRangeText);
        towerDamageTextUI.SetText(towerDmgText);
        towerShootingRTextUI.SetText(towerRateText);
        towerCardTextUI.SetText(towerCardText);

        towerRangeUI.position = towerTransform.position;

        // 타워 공격 범위 절대적 크기
        Transform parent = towerRangeUI.parent;
        towerRangeUI.parent = null;
        towerRangeUI.localScale = Vector3.one * (2 * nowTower.ShootRange);
        towerRangeUI.parent = parent;
    }

    public void OffPanel(){
        if(towerInfoPanel.activeSelf){
            towerInfoPanel.SetActive(false);
        }
    }

}
