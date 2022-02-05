using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathbeer : Shootable
{
    public bool breakable;
    private void OnTriggerEnter(Collider other)
    {
        if (breakable && !other.CompareTag("Enemy"))
        {
            float random = Random.Range(1, 5);
            switch (random)
            {
                case 1:
                    FindObjectOfType<AudioManager>().Play("GlassBreaking1");
                    break;
                case 2:
                    FindObjectOfType<AudioManager>().Play("GlassBreaking2");
                    break;
                case 3:
                    FindObjectOfType<AudioManager>().Play("GlassBreaking3");
                    break;
                case 4:
                    FindObjectOfType<AudioManager>().Play("GlassBreaking4");
                    break;
            }
            Destroy(gameObject);
        }
    }
}
