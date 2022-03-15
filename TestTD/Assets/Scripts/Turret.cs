using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    [Header("Tower Parametres")]
    public float range = 20f;
    public float RateOfFire = 1f;
    public float RotationSpeed = 10f;
    private float ShootCD = 0f;
    [Header("Unity Setup")]
    public string enemyTag = "Enemy";

    public Transform rotater;
    public GameObject BulletPrefab;
    public Transform FirePoint;


    //public GameObject
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) { target = nearestEnemy.transform; }
        else { target = null; }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        { return; }

        Vector3 dir = target.position - transform.position;
        Quaternion lookrotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotater.rotation, lookrotation, Time.deltaTime * RotationSpeed).eulerAngles;
        rotater.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (ShootCD <= 0f)
        {
            Shoot();
            ShootCD = 1f / RateOfFire;
        }
        ShootCD -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        { bullet.Seek(target); }
            


    }
}
