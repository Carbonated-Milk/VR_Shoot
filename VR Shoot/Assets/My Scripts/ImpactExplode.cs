using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactExplode : MonoBehaviour
{
    public int damage;

    public float radius = 5f;

    public float force = 700f;

    public GameObject explosionEffect;

  

    void Explode ()
    { 
        transform.gameObject.tag = "Exploded";
        Debug.Log("boom");
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders =Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            if(nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.GetComponent<Health>().TakeDamage(damage);
            }

            if(nearbyObject.CompareTag("Explodable"))
            {
                nearbyObject.GetComponent<ImpactExplode>().Explode();
            }

            Rigidbody rb =nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
           
        
        Destroy(gameObject);

        
    }

    void OnCollisionEnter (Collision die)
    {
        Explode();
    }
}
