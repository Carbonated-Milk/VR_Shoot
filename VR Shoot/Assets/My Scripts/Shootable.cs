using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public int damage;

    public float spinSpeed;

    protected Rigidbody rigidbody;

    private bool inflictedPain;

    void OnTriggerEnter(Collider oof)
    {
        if (oof.gameObject.CompareTag("MainCamera"))
        {
            if (!inflictedPain)
            {
                inflictedPain = true;
                FindObjectOfType<Lives>().Hurt(damage);
            }
            //FindObjectOfType<AudioManager>().Play("Ouch");
            //GetComponent<AudioSource>().enabled = true;
            //GetComponent<AudioSource>().Play();
        }
    }
}
