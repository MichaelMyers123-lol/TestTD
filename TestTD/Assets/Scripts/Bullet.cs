using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public int damage = 5;
    public float speed = 1f;
    public float Explosion_R = 0f;
    public float Slow = 0f;
    public float Slow_Duration = 0f;
    public GameObject Impact_Effect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Проверяем существует ли ещё наша цель и если у нас взрывной снаряд, то уничтожаем его в месте, где он потерял цель
            if (target == null)
            {
            if (Explosion_R > 0f)
            {
                Explosion();
                Destroy(gameObject);
                return;
            }
            else
            {
                //Debug.Log("I Have no target");
                Destroy(gameObject);
                return;
            }

            }
            //Во избежание ситуаций, где снаряд лишнее время летает вокруг объекта высчитываем сколько он пролетел за этот фрэйм и если цель ближе этого расстояния, то регистрируем попадание
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            transform.LookAt(target);
        }
    void HitTarget()
    {
        
        
        //При регистрации попадания проверяем есть ли у нашего снаряда радиус взрыва
        if (Explosion_R > 0f)
        {
            Explosion();

        }
        else
        {
            Damage_Opp(target);
        }
        Destroy(gameObject);
        

    }
    
    void Explosion ()
    {
        //Debug.Log("Взрываю!");
        DrawImpactEffect();
        Collider[] hit_objs = Physics.OverlapSphere(transform.position, Explosion_R);

        foreach(Collider collider in hit_objs)
        {
            if(collider.tag == "Enemy")
            {

                Damage_Opp(collider.transform);


            }
            
        }
    }
    //После регистрации попадания уменьшаем здоровье цели и замедляем если снаряд обладает этим свойством
    void Damage_Opp(Transform enemy)
    {
        Enemy_Movement e = enemy.GetComponent<Enemy_Movement>();
        e.Take_Damage(damage);
        if (Slow > 0)
        {
            e.Take_Slow(Slow, Slow_Duration);
        }
        DrawImpactEffect();
        

    }
    void DrawImpactEffect ()
    {
        GameObject imp_inst = (GameObject)Instantiate(Impact_Effect, transform.position, transform.rotation);
        Destroy(imp_inst, 2.0f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Explosion_R);
    }
    
}


