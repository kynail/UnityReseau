using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProjectile : BaseProjectile
{
    [SerializeField]
    private float radius = 3;

    [SerializeField]
    private ParticleSystem impactParticle;

    public override void Start()
    {
        base.Start();
    }

    public override void OnReachedTarget()
    {
        base.OnReachedTarget();

        if (impactParticle != null)
        {            
            impactParticle.Play();
        }

        GetComponent<MeshRenderer>().enabled = false;

        Destroy(gameObject, 2);

        if (NetworkManager.Instance.IsServer)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                HealthSystem hp = hitCollider.GetComponent<HealthSystem>();
                if (hp)
                {
                    float actualDamage = damage - (Vector3.Distance(hp.transform.position, transform.position) * 10);
                    hp.TakeDamage((int)actualDamage, attackerName,target,hitNormal);
                }
            }
        }
    }
}
