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
    private float waitCardSelectTime = 180f; // 게임 준비시간.

    // get 프로퍼티. -> 하나의 클래스에 오직 하나의 객체 인스턴스.
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
    private bool isTowerPointSet = false; // 타워 포인트 짓기.(라운드 시작 전)
    private bool isRoundDone = true; // 라운드가 끝나면 true , 라운드 시작하면 false
    private bool isCardSetting = false; // 카드 세팅 5개 됫는지.(라운드 시작 전.)
    private bool isTowerSelected = false; // 타워 지어졌음. -> 라운드 시작 가능.
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
        // 타이머 설정.
        TimerUISetting();
        // 적 수 설정.
        EnemyNumUISetting();
        
        // 라운드가 시작 안해있고, 타워 처음 세팅 안됨.
        if (isRoundDone && !isCardSetting)
        {
            WaveUISetting(); // WAVE UI 세팅.
            NewCardSetting();
        }

        // 카드 패를 통한 타워 생성 시간.(3분)
        if(isRoundDone && !isTowerSelected)
        {
            // 제한 시간내 타워 선택 안할시, 넘어감.
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}

            // 자리 선정. 타워 설치 장소 표시하기.
            if (Input.GetMouseButtonDown(0) && !isTowerPointSet)
            {
                TileDetecterGM.MousePos = Input.mousePosition;
                TileDetecterGM.SavePlace();
                if (TileDetecterGM.TileToSpawnTower != null)
                {
                    isTowerPointSet = true;
                }
            }

            // 자리 선정 된 지점이 있고, 타워 선택 버튼 클릭시, 타워 생성 가능.
            if (isTowerPointSet)
            {
                btn_RoundStart.interactable = true;
            }

        }

        // 라운드 시작 안해있고, 타워 선택 끝남.
        if(isRoundDone && isTowerSelected)
        { 
            RoundStart();
            isTowerPointSet = false;
        }

        // 라운드 도중. 타이머10 초 넘어가면, 적 생성 멈춤.
        if(!isRoundDone && timerRound >= 10)
        {
           EnemySpawnerGM.StopSpawnEnemy();
        }

        // 라운드 도중, 타이머 20초 이후, 적이 없다면 라운드 종료.
        if (!isRoundDone && timerRound >= 20)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                RoundEnd();
            }
        }
    }

    // 라운드 시작 전, 카드 5개 세팅.
    public void NewCardSetting()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            PockerGeneraterGM.NewCards();
            btn_CardChange[i].interactable = true; // 한번씩 바꿀 기회 주기.
        }
        btn_RoundStart.interactable = false;
        this.isCardSetting = true;
    }

    // 적 생성 시작.
    public void RoundStart()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            btn_CardChange[i].interactable = false; // 한번씩 바꿀 기회 주기.
        }
        EnemySpawnerGM.StartSpawnEnemy();
        this.isRoundDone = false; this.isTowerSelected = false;
    }

    // 적 객체 모두 삭제 되거나, 제한 시간 종료.
    public void RoundEnd()
    {
        gameRound++;
        this.isRoundDone = true; this.isCardSetting = false;
    }

    // 카드 변경 버튼 콜벡함수.
    void CardChangeBtnClick(int index)
    {
        PockerGeneraterGM.ChangeCards(index);
        btn_CardChange[index].interactable = false; // 한번씩 바꿀 기회 주기.
    }

    // 라운드 시작 버튼 콜백함수. (타워 생성 위치 결정시, 활성화.)
    void RoundStartBtnClick()
    {
        if (TowerSpawnerGM.TowerPoint)
        {
            TileDetecterGM.GenerateTower(PockerGeneraterGM.CheckCardHands());
            isTowerSelected = true;
        }
        btn_RoundStart.interactable = false;
    }

    // 타이멍 UI 세팅
    void TimerUISetting()
    {
        // 타이머 설정.
        timerRound += Time.deltaTime;
        string timerString = string.Format("{0:D2} : {1:D2}", Mathf.CeilToInt(timerRound / 60) - 1, Mathf.CeilToInt(timerRound % 60) - 1);
        timerUI.SetText(timerString);
    }

    // waveUI 세팅
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
