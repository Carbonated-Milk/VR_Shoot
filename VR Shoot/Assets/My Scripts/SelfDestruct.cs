using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private IEnumerator Start()
     {
         yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration + GetComponent<ParticleSystem>().startLifetime);
         Destroy(gameObject); 
     }
}
