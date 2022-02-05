using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public static float alignmentFac, closenessFac, colliderRad, distLim, speed;
    public List<GameObject> otherBoids;
    private GameObject player;

    public float check;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("MainCamera");
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        GetComponent<SphereCollider>().radius = colliderRad;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * -speed * Time.fixedDeltaTime;
        float playerDis = Vector3.SqrMagnitude(transform.position - player.transform.position);
        Vector3 averageDir = Vector3.zero;
        Vector3 closestBoid = Vector3.one * 100000;
        foreach (GameObject boid in otherBoids)
        {
            averageDir += boid.transform.rotation.eulerAngles;
            if (Vector3.SqrMagnitude(boid.transform.position - transform.position) < Vector3.SqrMagnitude(closestBoid - transform.position))
            {
                closestBoid = boid.transform.position - transform.position;
            }
        }

        averageDir /= otherBoids.Count;

        if(otherBoids.Count != 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(averageDir), alignmentFac);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-closestBoid), 1/closestBoid.sqrMagnitude * closenessFac);    
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), Mathf.Pow(100/(distLim - playerDis), 3));
        check = 100/(200 - playerDis);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boid"))
        {
            otherBoids.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Boid"))
        {
            otherBoids.Remove(other.gameObject);
        }
    }

    

}
