using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject EnemyHpBarPrefab;

    [SerializeField]
    private Transform[] wayPoints;
    private float spawnTime = 1.0f;

    private List<Enemy> enemyList;
    private List<GameObject> enemyHpBarList;

    public List<Enemy> EnemyList => enemyList;
    public List<GameObject> EnemyHpBarList => enemyHpBarList;


    private void Awake()
    {
        enemyList = new List<Enemy>();
        enemyHpBarList = new List<GameObject>();
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine("EnemySpawn");
    }

    public void StopSpawnEnemy()
    {
        StopCoroutine("EnemySpawn");
    }

    IEnumerator EnemySpawn()
    {
        while(true)
        {
            Enemy enemyInstance = Instantiate(EnemyPrefab).GetComponent<Enemy>();
            GameObject enemyHpBarInstance = Instantiate(EnemyHpBarPrefab);

            enemyInstance.SetHpBar(enemyHpBarInstance);
            enemyInstance.SetWayPoints(wayPoints);
            enemyInstance.EnemyMaxHp = GameManager.Instance.gameRound * 20;
            enemyInstance.EnemyHp = enemyInstance.EnemyMaxHp;

            enemyHpBarInstance.transform.SetParent(GameManager.Instance.CanvasTransform);
            enemyHpBarInstance.transform.localScale = Vector3.one;
            enemyHpBarInstance.GetComponent<Slider>().value =  1;

            enemyList.Add(enemyInstance);
            enemyHpBarList.Add(enemyHpBarInstance);

            yield return GameManager.WaitForClass.WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnenmy(Enemy enemy)
    {
        GameManager.Instance.Money -= 1;
        enemy.OnDie();
    }

}
