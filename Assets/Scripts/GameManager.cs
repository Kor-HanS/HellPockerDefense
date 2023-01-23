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
    private bool isRoundDone = true; // 라운드가 끝나면 true , 라운드 시작하면 false
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
        // 타이머 설정.
        timerSetting();
        
        // 라운드가 시작 안해있고, 타워 처음 세팅 안됨.
        if (isRoundDone && !isTowerSetting)
        {
            waveSetting();
            newTowerSetting();
        }

        // 카드 패를 통한 타워 생성 시간.(3분)
        if(isRoundDone && !isTowerSelected)
        {
            // 제한 시간내 타워 선택 안할시, 넘어감.
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}
            
            // 자리 선정. x 표시.

            // 자리 선정 된 지점이 있고, 타워 선택 버튼 클릭시, 타워 생성.

        }

        // 라운드 시작 안해있고, 타워 선택 끝남.
        if(isRoundDone && isTowerSelected)
        { 
            roundStart();
        }

        // 라운드 도중. 타이머10 초 넘어가면, 적 생성 멈춤.
        if(!isRoundDone && timerRound >= 10)
        {
            enemySpawner.stopSpawnEnemy();
        }

        // 라운드 도중, 타이머 20초 이후, 적이 없다면 라운드 종료.
        if (!isRoundDone && timerRound >= 20)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                roundEnd();
            }
        }
    }

    // 라운드 시작 전, 카드 5개 세팅.
    public void newTowerSetting()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            pockergenerater.newCards();
            btn_CardChange[i].interactable = true; // 한번씩 바꿀 기회 주기.
        }
        this.isTowerSetting = true;
        Debug.Log(pockergenerater.checkCardHands());
    }

    // 적 생성 시작.
    public void roundStart()
    {
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            btn_CardChange[i].interactable = false; // 한번씩 바꿀 기회 주기.
        }
        enemySpawner.startSpawnEnemy();
        this.isRoundDone = false; this.isTowerSelected = false;
    }

    // 적 객체 모두 삭제 되거나, 제한 시간 종료.
    public void roundEnd()
    {
        gameRound++;
        this.isRoundDone = true; this.isTowerSetting = false;
    }

    // 카드 변경 버튼 콜벡함수.
    void cardChangeBtnClick(int index)
    {
        pockergenerater.changeCards(index);
        btn_CardChange[index].interactable = false; // 한번씩 바꿀 기회 주기.
        Debug.Log(pockergenerater.checkCardHands());
    }

    // 타이멍 UI 세팅
    void timerSetting()
    {
        // 타이머 설정.
        timerRound += Time.deltaTime;
        string timerString = string.Format("{0:D2} : {1:D2}", Mathf.CeilToInt(timerRound / 60) - 1, Mathf.CeilToInt(timerRound % 60) - 1);
        timerUI.SetText(timerString);
    }

    // waveUI 세팅
    void waveSetting()
    {
        string waveString = string.Format("{0:D4}", gameRound);
        waveUI.SetText(waveString);
    }
}
