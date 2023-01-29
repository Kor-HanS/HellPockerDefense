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

        // ���� ��ġ ù��° üũ����Ʈ(������)
        transform.position = wayPoints[currentIndex].position;

        // ���� ����Ʈ �̵� �ڷ�ƾ �Լ�.
        StartCoroutine(MoveTo());
    }

    private IEnumerator MoveTo()
    {
        // ���� ������ ����.
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
