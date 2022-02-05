using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public bool addheart;

    public int lives;

    public static int health;

    public GameObject[] hearts;

    public GameObject heart;

    public Transform heartLocation;
    
    public GameObject playerHead;

    void Start()
    {
        health = lives;
        hearts = new GameObject[lives];
        for(int i = 0; i < lives; i++)
        {   
            hearts[i] = Instantiate(heart);
            hearts[i].transform.position = heartLocation.position + new Vector3(0f, 0f, -(lives - 1f) * .35f / 2 + i * .35f);
            hearts[i].GetComponent<HeartFunction>().delay = i * .35f;
        }
    }

    void Update()
    {
        //hearts;
        if(addheart)
        {
            AddHeart(2f);
            addheart = false;
        }
    }

    public void Hurt(int livesLost)
    {
        Mathf.Clamp(livesLost, 0f, health);
        playerHead.gameObject.GetComponent<ParticleSystem>().Play();
        for (int i = 0; i < livesLost; i++)
        {
            if (!GameManager.gameOver)
            {
                hearts[health - 1].GetComponent<HeartFunction>().Broken();
                health -= 1;
                if (health <= 0f)
                {
                    GameManager.gameOver = true;
                    FindObjectOfType<AudioManager>().Play("Ouch");
                    FindObjectOfType<AudioManager>().Play("Patrick");
                    FindObjectOfType<AudioManager>().Play("Piano Slam");
                    return;

                }
            }
        }
        
    }

    public void AddHeart(float healthAdd)
    {
        healthAdd = Mathf.Clamp(healthAdd, 0f, lives - health);
        for(int i = 0; i < healthAdd; i++)
        {
            hearts[health] = Instantiate(heart);
            hearts[health].transform.position = heartLocation.position + new Vector3(0f, 0f, -(lives - 1f) * .35f / 2 + health * .35f);
            hearts[health].GetComponent<HeartFunction>().delay = health * .35f;
            health += 1;
        }
        Debug.Log(GameManager.money);
    }

}