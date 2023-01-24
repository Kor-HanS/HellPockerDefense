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
    private bool isTowerPointSet = false; // Ÿ�� ����Ʈ ����.(���� ���� ��)
    private bool isRoundDone = true; // ���尡 ������ true , ���� �����ϸ� false
    private bool isCardSetting = false; // ī�� ���� 5�� �̴���.(���� ���� ��.)
    private bool isTowerSelected = false; // Ÿ�� ��������. -> ���� ���� ����.
    private float timerRound = 0.0f;
    private const float waitCardSelectTime = 180f;

    private void Awake()
    {
        btn_CardChange[0].onClick.AddListener(delegate { cardChangeBtnClick(0); });
        btn_CardChange[1].onClick.AddListener(delegate { cardChangeBtnClick(1); });
        btn_CardChange[2].onClick.AddListener(delegate { cardChangeBtnClick(2); });
        btn_CardChange[3].onClick.AddListener(delegate { cardChangeBtnClick(3); });
        btn_CardChange[4].onClick.AddListener(delegate { cardChangeBtnClick(4); });

        btn_RoundStart.onClick.AddListener(roundStartBtnClick);
    }

    private void Update()
    {
        // Ÿ�̸� ����.
        timerSetting();
        
        // ���尡 ���� �����ְ�, Ÿ�� ó�� ���� �ȵ�.
        if (isRoundDone && !isCardSetting)
        {
            waveSetting(); // WAVE UI ����.
            newCardSetting();
        }

        // ī�� �и� ���� Ÿ�� ���� �ð�.(3��)
        if(isRoundDone && !isTowerSelected)
        {
            // ���� �ð��� Ÿ�� ���� ���ҽ�, �Ѿ.
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}

            // �ڸ� ����. Ÿ�� ��ġ ��� ǥ���ϱ�.
            if (Input.GetMouseButtonDown(0) && !isTowerPointSet)
            {
                tileDetecter.MousePos = Input.mousePosition;
                tileDetecter.SavePlace();
                if (tileDetecter.TileToSpawnTower != null)
                {
                    isTowerPointSet = true;
                }
            }

            // �ڸ� ���� �� ������ �ְ�, Ÿ�� ���� ��ư Ŭ����, Ÿ�� ���� ����.
            if (isTowerPointSet)
            {
                btn_RoundStart.interactable = true;
            }

        }

        // ���� ���� �����ְ�, Ÿ�� ���� ����.
        if(isRoundDone && isTowerSelected)
        { 
            roundStart();
            isTowerPointSet = false;
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
    public void newCardSetting()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            pockergenerater.newCards();
            btn_CardChange[i].interactable = true; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        }
        btn_RoundStart.interactable = false;
        this.isCardSetting = true;
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
        this.isRoundDone = true; this.isCardSetting = false;
    }

    // ī�� ���� ��ư �ݺ��Լ�.
    void cardChangeBtnClick(int index)
    {
        pockergenerater.changeCards(index);
        btn_CardChange[index].interactable = false; // �ѹ��� �ٲ� ��ȸ �ֱ�.
    }

    // ���� ���� ��ư �ݹ��Լ�. (Ÿ�� ���� ��ġ ������, Ȱ��ȭ.)
    void roundStartBtnClick()
    {
        if (towerSpawner.TowerPoint)
        {
            tileDetecter.GenerateTower(pockergenerater.checkCardHands());
            isTowerSelected = true;
        }
        btn_RoundStart.interactable = false;
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
