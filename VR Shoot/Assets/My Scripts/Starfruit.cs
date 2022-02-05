using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfruit : MonoBehaviour
{
    public int healAmmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameOver)
        {
            healAmmount = 0;
        }
    }

    
    void OnTriggerEnter (Collider oof)
    {
        if(oof.gameObject.CompareTag("MainCamera"))
        {
            FindObjectOfType<AudioManager>().Play("Eat");
            FindObjectOfType<Lives>().AddHeart(healAmmount);
        }
        Destroy(gameObject);
    }
}
