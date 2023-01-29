using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private float spawnTime;

    private List<Enemy> enemyList;

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }


    public void StartSpawnEnemy()
    {
        StartCoroutine("EnemySpawn"); // 0.5�ʸ��� �� ����.
    }

    public void StopSpawnEnemy()
    {
        StopCoroutine("EnemySpawn");
    }


    IEnumerator EnemySpawn()
    {
        while(true)
        {
            GameObject enemyInstance = Instantiate(EnemyPrefab);
            enemyInstance.GetComponent<Enemy>().SetWayPoints(wayPoints);
            enemyInstance.GetComponent<Enemy>().EnemyHp = GameManager.Instance.gameRound * 20;
            enemyList.Add(enemyInstance.GetComponent<Enemy>());

            // 0.5�ʸ��� �� ����.
            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnenmy(Enemy enemy)
    {
        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

}
