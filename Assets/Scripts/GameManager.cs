using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PockerGenerater pockerGeneraterGM;
    public PockerGenerater PockerGeneraterGM => pockerGeneraterGM;
    
    [SerializeField]
    private EnemySpawner enemySpawnerGM;
    public EnemySpawner EnemySpawnerGM => enemySpawnerGM;
    
    [SerializeField]
    private TowerSpawner towerSpawnerGM;
    public TowerSpawner TowerSpawnerGM => towerSpawnerGM;

    [SerializeField]
    private TileDetecter tileDetecterGM;
    public TileDetecter TileDetecterGM => tileDetecterGM;
    [SerializeField]
    private Button btn_RoundStart;
    [SerializeField]
    private Button[] btn_CardChange;
    [SerializeField]
    private TMP_Text timerUI;
    [SerializeField]
    private TMP_Text waveUI;
    [SerializeField]
    private TMP_Text enemyNumUI;
    [SerializeField]
    private float waitCardSelectTime = 180f; // ���� �غ�ð�.

    // get ������Ƽ. -> �ϳ��� Ŭ������ ���� �ϳ��� ��ü �ν��Ͻ�.
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }


    public int gameRound = 1;
    private bool isTowerPointSet = false; // Ÿ�� ����Ʈ ����.(���� ���� ��)
    private bool isRoundDone = true; // ���尡 ������ true , ���� �����ϸ� false
    private bool isCardSetting = false; // ī�� ���� 5�� �̴���.(���� ���� ��.)
    private bool isTowerSelected = false; // Ÿ�� ��������. -> ���� ���� ����.
    private float timerRound = 0.0f;

    private void Awake()
    {

        btn_CardChange[0].onClick.AddListener(delegate { CardChangeBtnClick(0); });
        btn_CardChange[1].onClick.AddListener(delegate { CardChangeBtnClick(1); });
        btn_CardChange[2].onClick.AddListener(delegate { CardChangeBtnClick(2); });
        btn_CardChange[3].onClick.AddListener(delegate { CardChangeBtnClick(3); });
        btn_CardChange[4].onClick.AddListener(delegate { CardChangeBtnClick(4); });

        btn_RoundStart.onClick.AddListener(RoundStartBtnClick);
    }

    private void Update()
    {
        // Ÿ�̸� ����.
        TimerUISetting();
        // �� �� ����.
        EnemyNumUISetting();
        
        // ���尡 ���� �����ְ�, Ÿ�� ó�� ���� �ȵ�.
        if (isRoundDone && !isCardSetting)
        {
            WaveUISetting(); // WAVE UI ����.
            NewCardSetting();
        }

        // ī�� �и� ���� Ÿ�� ���� �ð�.(3��)
        if(isRoundDone && !isTowerSelected)
        {
            // ���� �ð��� Ÿ�� ���� ���ҽ�, �Ѿ.
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}

            // �ڸ� ����. Ÿ�� ��ġ ��� ǥ���ϱ�.
            if (Input.GetMouseButtonDown(0) && !isTowerPointSet)
            {
                TileDetecterGM.MousePos = Input.mousePosition;
                TileDetecterGM.SavePlace();
                if (TileDetecterGM.TileToSpawnTower != null)
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
            RoundStart();
            isTowerPointSet = false;
        }

        // ���� ����. Ÿ�̸�10 �� �Ѿ��, �� ���� ����.
        if(!isRoundDone && timerRound >= 10)
        {
           EnemySpawnerGM.StopSpawnEnemy();
        }

        // ���� ����, Ÿ�̸� 20�� ����, ���� ���ٸ� ���� ����.
        if (!isRoundDone && timerRound >= 20)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                RoundEnd();
            }
        }
    }

    // ���� ���� ��, ī�� 5�� ����.
    public void NewCardSetting()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            PockerGeneraterGM.NewCards();
            btn_CardChange[i].interactable = true; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        }
        btn_RoundStart.interactable = false;
        this.isCardSetting = true;
    }

    // �� ���� ����.
    public void RoundStart()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            btn_CardChange[i].interactable = false; // �ѹ��� �ٲ� ��ȸ �ֱ�.
        }
        EnemySpawnerGM.StartSpawnEnemy();
        this.isRoundDone = false; this.isTowerSelected = false;
    }

    // �� ��ü ��� ���� �ǰų�, ���� �ð� ����.
    public void RoundEnd()
    {
        gameRound++;
        this.isRoundDone = true; this.isCardSetting = false;
    }

    // ī�� ���� ��ư �ݺ��Լ�.
    void CardChangeBtnClick(int index)
    {
        PockerGeneraterGM.ChangeCards(index);
        btn_CardChange[index].interactable = false; // �ѹ��� �ٲ� ��ȸ �ֱ�.
    }

    // ���� ���� ��ư �ݹ��Լ�. (Ÿ�� ���� ��ġ ������, Ȱ��ȭ.)
    void RoundStartBtnClick()
    {
        if (TowerSpawnerGM.TowerPoint)
        {
            TileDetecterGM.GenerateTower(PockerGeneraterGM.CheckCardHands());
            isTowerSelected = true;
        }
        btn_RoundStart.interactable = false;
    }

    // Ÿ�̸� UI ����
    void TimerUISetting()
    {
        // Ÿ�̸� ����.
        timerRound += Time.deltaTime;
        string timerString = string.Format("{0:D2} : {1:D2}", Mathf.CeilToInt(timerRound / 60) - 1, Mathf.CeilToInt(timerRound % 60) - 1);
        timerUI.SetText(timerString);
    }

    // waveUI ����
    void WaveUISetting()
    {
        string waveString = string.Format("{0:D2}", gameRound);
        waveUI.SetText(waveString);
    }

    void EnemyNumUISetting()
    {
        int count = EnemySpawnerGM.EnemyList.Count;
        string enmyUiString = string.Format("{0:D2}", count);
        enemyNumUI.SetText(enmyUiString);
    }
}
