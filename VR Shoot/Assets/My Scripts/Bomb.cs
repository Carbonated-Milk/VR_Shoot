using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;

    public float force = 700f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    //public CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        /*countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }*/
    }

    void Explode ()
    {
        //StartCoroutine(cameraShake.Shake(.15f, .4f));

        FindObjectOfType<AudioManager>().Play("ExplosionEffect");
        
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders =Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb =nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
           //add force
           //damage
        
        Destroy(gameObject);

        
    }

    void OnCollisionEnter (Collision beans)
    {
        Explode();
    }
}
