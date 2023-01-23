using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private Transform[] wayPoints;


    public void startSpawnEnemy()
    {
        StartCoroutine("EnemySpawn"); // 0.5�ʸ��� �� ����.
    }

    public void stopSpawnEnemy()
    {
        StopCoroutine("EnemySpawn");
    }


    IEnumerator EnemySpawn()
    {
        while(true)
        {
            GameObject enemyInstance = Instantiate(EnemyPrefab);
            enemyInstance.GetComponent<Enemy>().SetWayPoints(wayPoints);
            
            // 0.5�ʸ��� �� ����.
            yield return new WaitForSeconds(1.0f);
        }
    }


}
