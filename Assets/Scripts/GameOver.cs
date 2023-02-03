using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Button Btn_RestartGame;

    private void Awake() {
        Btn_RestartGame.onClick.AddListener(OnRestartBtnClick);
    }

    public void OnRestartBtnClick(){
        SceneManager.LoadScene("Scene_Game");
    }

}
