using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PockerGenerater pockergenerater;
    [SerializeField]
    private EnemySpawner enemySpawner;
    [SerializeField]
    private TowerSpawner towerSpawner;
    [SerializeField]
    private TileDetecter tileDetecter;
    [SerializeField]
    private Button btn_RoundStart;
    [SerializeField]
    private Button[] btn_CardChange;
    [SerializeField]
    private TMP_Text timerUI;
    [SerializeField]
    private TMP_Text waveUI;

    public int gameRound = 1;
    private bool isRoundDone = true; // ���尡 ������ true , ���� �����ϸ� false
    private bool isTowerSelected = false;
    private bool isTowerSetting = false;
    private int enemyNum = 0;
    private float timerRound = 0.0f;
    private const float waitCardSelectTime = 40f;

    private void Awake()
    {
        btn_CardChange[0].onClick.AddListener(delegate { cardChangeBtnClick(0); });
        btn_CardChange[1].onClick.AddListener(delegate { cardChangeBtnClick(1); });
        btn_CardChange[2].onClick.AddListener(delegate { cardChangeBtnClick(2); });
        btn_CardChange[3].onClick.AddListener(delegate { cardChangeBtnClick(3); });
        btn_CardChange[4].onClick.AddListener(delegate { cardChangeBtnClick(4); });
    }

    private void Update()
    {
        // Ÿ�̸� ����.
        timerSetting();
        
        // ���尡 ���� �����ְ�, Ÿ�� ó�� ���� �ȵ�.
        if (isRoundDone && !isTowerSetting)
        {
            waveSetting();
            newTowerSetting();
        }

        // ī�� �и� ���� Ÿ�� ���� �ð�.(3��)
        if(isRoundDone && !isTowerSelected)
        {
            // ���� �ð��� Ÿ�� ���� ���ҽ�, �Ѿ.
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}
            
            // �ڸ� ����. x ǥ��.

            // �ڸ� ���� �� ������ �ְ�, Ÿ�� ���� ��ư Ŭ����, Ÿ�� ����.

        }

        // ���� ���� �����ְ�, Ÿ�� ���� ����.
        if(isRoundDone && isTowerSelected)
        { 
            roundStart();
        }

        // ���� ����. Ÿ�̸�10 �� �Ѿ��, �� ���� ����.
        if(!isRoundDone && timerRound >= 10)
        {
            enemySpawner.stopSpawnEnemy();
        }

        // ���� ����, Ÿ�̸� 20�� ����, ���� ���ٸ� ���� ����.
        if (!isRoundDone && timerRound >= 20)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                roundEnd();
            }
        }
    }

    // ���� ���� ��, ī�� 5�� ����.
    public void newTowerSetting()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            pockergenerater.newCards();
            btn_CardChange[i].interactable = true; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        }
        this.isTowerSetting = true;
        Debug.Log(pockergenerater.checkCardHands());
    }

    // �� ���� ����.
    public void roundStart()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            btn_CardChange[i].interactable = false; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        }
        enemySpawner.startSpawnEnemy();
        this.isRoundDone = false; this.isTowerSelected = false;
    }

    // �� ��ü ��� ���� �ǰų�, ���� �ð� ����.
    public void roundEnd()
    {
        gameRound++;
        this.isRoundDone = true; this.isTowerSetting = false;
    }

    // ī�� ���� ��ư �ݺ��Լ�.
    void cardChangeBtnClick(int index)
    {
        pockergenerater.changeCards(index);
        btn_CardChange[index].interactable = false; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        Debug.Log(pockergenerater.checkCardHands());
    }

    // Ÿ�̸� UI ����
    void timerSetting()
    {
        // Ÿ�̸� ����.
        timerRound += Time.deltaTime;
        string timerString = string.Format("{0:D2} : {1:D2}", Mathf.CeilToInt(timerRound / 60) - 1, Mathf.CeilToInt(timerRound % 60) - 1);
        timerUI.SetText(timerString);
    }

    // waveUI ����
    void waveSetting()
    {
        string waveString = string.Format("{0:D4}", gameRound);
        waveUI.SetText(waveString);
    }
}
