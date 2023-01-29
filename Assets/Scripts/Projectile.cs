using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2d;
    private Transform target;
    private int damage;
    private void Update()
    {
        if(target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2d.SetDirection(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;
        if (collision.transform != target) return;

        Destroy(gameObject);
        collision.GetComponent<Enemy>().TakeDamage(damage);
    }
    
    public void Setup(Transform target, int damage)
    {
        movement2d = GetComponent<Movement2D>();
        this.target = target;
        this.damage = damage;
    }
}
