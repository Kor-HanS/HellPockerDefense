using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    GameObject hpBarPrefab;

    private Movement2D movement2D;
    private Transform[] wayPoints; 
    private int wayPointLength;
    private int currentIndex = 0;

    public int EnemyHp { get; set; }

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }

    public void SetWayPoints(Transform[] wayPoints)
    {
        this.wayPoints = new Transform[wayPoints.Length];
        this.wayPoints = wayPoints;
        this.wayPointLength = wayPoints.Length;

        // 현재 위치 첫번째 체크포인트(시작점)
        transform.position = wayPoints[currentIndex].position;

        // 웨이 포인트 이동 코루틴 함수.
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        // 다음 도착지 결정.
        NextMoveTo();

        while (true)
        {
            transform.Rotate(Vector3.forward * 10);

            if (Vector2.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo(); 
            }

            yield return null;
        }
    }
    // 다음 도착지 결정.
    private void NextMoveTo()
    {
       if(currentIndex < wayPointLength - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.SetDirection(direction);
        }
        else
        {
            GameManager.Instance.EnemySpawnerGM.DestroyEnenmy(this);
        }
    }

    public void OnDie()
    {
        GameManager.Instance.EnemySpawnerGM.EnemyList.Remove(this);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        EnemyHp -= damage;
        if(EnemyHp <= 0)
        {
            OnDie();
        }
    }

}
