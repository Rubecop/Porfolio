using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{

    //public EnemyMove enemigo;
    public Transform target;
    VidaMinion vidaminion;
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 2f;
    private float fireCountdown = 0f;
    public Transform partToRotate;
    public float damageABase;
    public float damageAEscudo;

    [Header("Unity Setup")]

    public bool EstaAtacando = false;
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;
    Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //enemigo = FindObjectOfType<EnemyMove>();
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
            
            if(distanceToEnemy < shortestDistance)
            {

                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;

            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {

            target = nearestEnemy.transform;


        } else
        {

            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {




        if (target == null) 
        {
            
            EstaAtacando = false;
            return;
         }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
        

        if (fireCountdown <= 0f)
        {

            Shoot();
            fireCountdown = 3f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        

    }

    void Shoot()
    {
        vidaminion = target.gameObject.GetComponent<VidaMinion>();
        if (vidaminion.VIDA > 0)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Disparo disparo = bulletGO.GetComponent<Disparo>();

            if (disparo != null)
            {

                EstaAtacando = true;
                disparo.Seek(target);


            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}
