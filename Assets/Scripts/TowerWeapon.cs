using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget = 0, AttackTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private float attackRate = 0.5f;
    [SerializeField]
    private float attackRange = 5.0f;
    public float WeaponDamage { get; set; }

    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null; 
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        enemySpawner = GameManager.Instance.EnemySpawnerGM;
        WeaponDamage = transform.GetComponent<Tower>().Damage;
        ChangeState(WeaponState.SearchTarget);
    }

    private void Update()
    {
        if(attackTarget != null)
        {
            RotateToTarget();
        }
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }

    public void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closestDistSqr = Mathf.Infinity;

            for(int i = 0; i < enemySpawner.EnemyList.Count; i++)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

                if(distance <= attackRange && distance <= closestDistSqr)
                {
                    closestDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if(attackTarget != null)
            {
                ChangeState(WeaponState.AttackTarget);
            }

            yield return null;
        }
    }

    private IEnumerator AttackTarget()
    {
        while (true)
        {
            if(attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(attackRate);

            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, transform.rotation);
        clone.GetComponent<Projectile>().Setup(attackTarget,GetComponent<Tower>().Damage * GetComponent<Tower>().Level); // 레벨 1 데미지 1배, 레벨 2 데미지 2배, 레벨 3 데미지 3배.
    }
}
