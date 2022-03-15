using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [Header("Enemy Parametres")]
    public float init_speed = 40.0f;
    public float speed;
    public int damage = 1;
    public int health = 10;
    public int bounty = 10;

    //Переменная для хранения следующего вэйпоинта
    private Transform target;
    private int wavepointIndex=0;
    
    private Renderer Rend;

    private float Slow_Counter = 1000;
    private bool Slowed = false;
    private Color defaultColor;
    public Color SlowedColor;

    // Start is called before the first frame update
    void Start()
    {
        //Получаем компонент рендерер, и сохраняем дефолтный цвет материала
        Rend = GetComponent<Renderer>();
        defaultColor = Rend.material.color;
        speed = init_speed;
    target = Waypoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Вычисляем направление и дальность движения
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position,target.position) <= 0.2f)
        {
            GetNextPoint();
        }
        //Если замедлены, то считаем сколько дебаф будет висеть, и когда время истекает возвращаем изначальную скорость и цвет
        if (Slowed)
        {
            Slow_Counter -= Time.deltaTime;
            if (Slow_Counter <= 0)
            {
                Slowed = false;
                speed = init_speed;
                Rend.material.color = defaultColor;
            }
        }
    }
    public void Take_Damage (int amount)
    {
        //Debug.Log("My health =" + health);
        //Debug.Log("I took damage =" + amount);
        health -= amount;
        
        if (health <= 0)
        {
            Die();
        }
    }
    public void Take_Slow(float amount, float duration)
    {
        //Debug.Log("Меня замедлили на " + duration);
        Rend.material.color = SlowedColor;
        speed = init_speed * amount;
        Slow_Counter = duration;
        Slowed = true;
    }

    void Die()
    {
        //Если цель
        WaveSpawner.remainings--;
        PlayerStats.Money += bounty;
        Destroy(gameObject);
    }
    void GetNextPoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {

            DamagePlayer(damage);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
    void DamagePlayer(int dmg)
    {
        WaveSpawner.remainings--;
        PlayerStats.Lives -= dmg;
        Destroy(gameObject);
        
    }


}
