using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform[] wayPoints;
    private GameObject hpBar;
    private RectTransform hpBarRectTransform; 
    private int wayPointLength;
    private int currentIndex = 0;

    private Vector3 offset;

    public float EnemyHp { get; set; }
    public float EnemyMaxHp {get; set;}

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        offset = new Vector3(0,-20f,0);
    }

    private void LateUpdate() {
        hpBarFollowObj();
    }

    private IEnumerator MoveTo()
    {
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

    private void hpBarFollowObj(){
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        this.hpBarRectTransform.position = screenPos + offset;
    }

    public void SetWayPoints(Transform[] wayPoints){
        this.wayPoints = new Transform[wayPoints.Length];
        this.wayPoints = wayPoints;
        this.wayPointLength = wayPoints.Length;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine(MoveTo());
    }

    public void SetHpBar(GameObject hpBar){
        this.hpBar = hpBar;
        this.hpBarRectTransform = hpBar.GetComponent<RectTransform>();
    }

    public void OnDie(){
        GameManager.Instance.EnemySpawnerGM.EnemyHpBarList.Remove(this.hpBar);
        GameManager.Instance.EnemySpawnerGM.EnemyList.Remove(this);
        Destroy(this.hpBar.gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage){
        EnemyHp -= damage;
        hpBar.GetComponent<Slider>().value = EnemyHp / EnemyMaxHp;
        if(EnemyHp <= 0)
        {
            OnDie();
        }
    }

}
