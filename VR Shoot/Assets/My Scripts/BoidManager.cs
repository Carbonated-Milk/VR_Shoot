using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public int boidNum;

    public float speed;
    public float closenessFac;
    public float alignmentFac;

    public float distLim;
    public float colliderRad;
    public GameObject boidPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        Boid.closenessFac = closenessFac;
        Boid.colliderRad = colliderRad;
        Boid.alignmentFac = alignmentFac;
        Boid.distLim = distLim;
        Boid.speed = speed;
    }
    
    void Start()
    {
        for(int i = 0; i < boidNum; i++)
        { 
            GameObject prefab = Instantiate(boidPrefab);
            prefab.transform.position = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Boid.closenessFac = closenessFac;
        Boid.alignmentFac = alignmentFac;
        Boid.distLim = distLim;
        Boid.speed = speed;
    }
}
