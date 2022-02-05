using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenHeart : MonoBehaviour
{
    public float breakForce;
    // Start is called before the first frame update
    void Start()
    {
        //FindObjectOfType<AudioManager>().Play("Break");
        foreach(Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * Random.Range(0f, breakForce);
                rb.AddForce(force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
