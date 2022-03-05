using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float ProjectileSpeed = 10;
    [HideInInspector]
    public Vector3 target;

    [HideInInspector]
    public Vector3 hitNormal;

    [HideInInspector]
    public int damage;

    [HideInInspector]
    public float lifeTime = 10.0f;

    [HideInInspector]
    public string attackerName;

    protected bool stopMove = false;

    public virtual void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    public virtual void Update()
    {
        if (!stopMove)
        {
            if (Vector3.Distance(transform.position, target) > 1)
            {
                transform.position += (transform.forward * ProjectileSpeed * Time.deltaTime);                
            }
            else
            {
                OnReachedTarget();
                stopMove = true;
            }
        }        
    }
    
    public virtual void OnReachedTarget()
    {
    }   
}
