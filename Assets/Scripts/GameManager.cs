using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    internal static class WaitForClass{
        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        public static WaitForSeconds WaitForSeconds(float seconds){
            WaitForSeconds WaitForSecondsRet = new WaitForSeconds(seconds);
            return WaitForSecondsRet;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds){
            WaitForSecondsRealtime WaitForSecondsRealtimeRet = new WaitForSecondsRealtime(seconds);
            return WaitForSecondsRealtimeRet; 
        }
    }

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
    private ObjectDetecter objectDetecterGM;
    public ObjectDetecter ObjectDetecterGM => objectDetecterGM;

    [SerializeField]
    private Transform canvasTransform;
    public Transform CanvasTransform => canvasTransform;

    [SerializeField]
    private TMP_Text systemText;

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
    private TMP_Text moneyNumUI;
    [SerializeField]
    private float waitCardSelectTime = 180f;
    public int Money{get ; set;}

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
    private bool isTowerPointSet = false;
    private bool isRoundDone = true;
    private bool isCardSetting = false; 
    private bool isTowerSelected = false; 
    private float timerRound = 0.0f;

    private void Awake()
    {

        btn_CardChange[0].onClick.AddListener(delegate { CardChangeBtnClick(0); });
        btn_CardChange[1].onClick.AddListener(delegate { CardChangeBtnClick(1); });
        btn_CardChange[2].onClick.AddListener(delegate { CardChangeBtnClick(2); });
        btn_CardChange[3].onClick.AddListener(delegate { CardChangeBtnClick(3); });
        btn_CardChange[4].onClick.AddListener(delegate { CardChangeBtnClick(4); });

        btn_RoundStart.onClick.AddListener(RoundStartBtnClick);

        this.Money = 10;
    }

    private void Update()
    {
        // 게임 오버 여부 체크.
        CheckGameOver();

        // 타워 정보 출력.
        if(Input.GetKeyDown(KeyCode.Escape)){ObjectDetecterGM.OffTowerClick();}
        
        if(Input.GetMouseButtonDown(0)){
            ObjectDetecterGM.MousePos = Input.mousePosition;
            ObjectDetecterGM.OnTowerClick();
        }

        TimerUISetting();

        EnemyNumUISetting();

        MoneyNumUISetting();
        
        if (isRoundDone && !isCardSetting)
        {
            WaveUISetting();
            NewCardSetting();
        }

        if(isRoundDone && !isTowerSelected)
        {
            if(timerRound >= waitCardSelectTime) { isTowerSelected = true;}

            if (Input.GetMouseButtonDown(0) && !isTowerPointSet)
            {
                ObjectDetecterGM.MousePos = Input.mousePosition;
                ObjectDetecterGM.SavePlace();
                if (ObjectDetecterGM.TileToSpawnTower != null)
                {
                    isTowerPointSet = true;
                }
            }

            if (isTowerPointSet)
            {
                btn_RoundStart.interactable = true;
            }

        }

        if(isRoundDone && isTowerSelected)
        { 
            RoundStart();
            isTowerPointSet = false;
        }

        if(!isRoundDone && timerRound >= 15)
        {
           EnemySpawnerGM.StopSpawnEnemy();
        }

        if (!isRoundDone && timerRound >= 20)
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            if (enemy == null)
            {
                RoundEnd();
            }
        }
    }

    public void NewCardSetting()
    {
        PrintSystemText(1);
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            PockerGeneraterGM.NewCards();
            btn_CardChange[i].interactable = true;
        }
        btn_RoundStart.interactable = false;
        this.isCardSetting = true;
    }

    public void RoundStart()
    {
        PrintSystemText(1);
        timerRound = 0.0f;
        for (int i = 0; i < 5; i++)
        {
            btn_CardChange[i].interactable = false;
        }

        // 보스 라운드 구현.
        if((gameRound % 5) == 0){
            
        }else{
            EnemySpawnerGM.StartSpawnEnemy();
        }
        this.isRoundDone = false; this.isTowerSelected = false;
    }

    public void RoundEnd()
    {
        gameRound++;
        this.isRoundDone = true; this.isCardSetting = false;
    }

    void CardChangeBtnClick(int index)
    {
        PockerGeneraterGM.ChangeCards(index);
        btn_CardChange[index].interactable = false;
    }

    void RoundStartBtnClick()
    {
        if (TowerSpawnerGM.TowerPoint)
        {
            ObjectDetecterGM.GenerateTower(PockerGeneraterGM.CheckCardHands());
            isTowerSelected = true;
        }
        btn_RoundStart.interactable = false;
    }

    void TimerUISetting()
    {
        timerRound += Time.deltaTime;
        string timerString = string.Format("{0:D2} : {1:D2}", Mathf.CeilToInt(timerRound / 60) - 1, Mathf.CeilToInt(timerRound % 60) - 1);
        timerUI.SetText(timerString);
    }

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

    void MoneyNumUISetting(){
        string moneyUiString = string.Format("{0:D2}", this.Money);
        moneyNumUI.SetText(moneyUiString);
    }   

    void CheckGameOver(){
        if(this.Money < 0){
            SceneManager.LoadScene("Scene_GameOver");
        }
    }

    void PrintSystemText(int caseNum){
        switch(caseNum){
            case 1:
                systemText.text = "SYSTEM: ROUND START!";
                systemText.alpha = 1; 
                break;
            
            case 2 :
                systemText.text = "SYSTEM: PICK TOWER PLACE &  POCKER CARD !";
                systemText.alpha = 1;  
                break;

            case 3 :
                systemText.text = "SYSTEM: 포커 패를 고르세요!";
                systemText.alpha = 1;  
                break;
        }
        StartCoroutine(SystemTextFadeOut());
    }

    private IEnumerator SystemTextFadeOut(){
        float currentTime = 0f;
        float percent = 0f;

        while(percent < 1){
            currentTime += Time.deltaTime;
            percent = currentTime / 5.0f; 

            systemText.alpha = Mathf.Lerp(1,0,percent);

            yield return null;
        }
    }
}
