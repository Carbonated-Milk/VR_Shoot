using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordGigamongus : MonoBehaviour
{
    public int collideDamage;
    public int burnDamage;
    public int sliceDamage;
    public float flipError;

    public GameObject deathBeer;

    public float timeTake;
    private float moveStop;

    private Vector3[] weightedPositions;
    public float distRadius;

    public float newPower;

    private float time;
    private float attackTime;

    GameObject player;
    GameObject eye;
    GameObject healthBar;
    Transform shootPoint;
    ParticleSystem teleportParticals;

    private Health healthScript;
    private LazerController lazerScript;

    // Start is called before the first frame update
    void Start()
    {
        moveStop = 1;
        weightedPositions = new Vector3[4];
        player = GameObject.FindWithTag("MainCamera");
        eye = transform.GetChild(0).gameObject;
        healthBar = transform.GetChild(1).gameObject;
        healthScript = GetComponent<Health>();
        teleportParticals = GetComponent<ParticleSystem>();
        lazerScript = GetComponentInChildren<LazerController>();
        shootPoint = transform.GetChild(0).GetChild(0).transform;
        ResetPos();
    }

    // Update is called once per frame
    void Update()
    {
        //destroys self if player dies
        if(GameManager.gameOver)
        {
            teleportParticals.Play();
            FindObjectOfType<AudioManager>().Stop("BossTheme");
            Destroy(gameObject);
        }

        //new pos targets
        if (time > 1f)
        {
            weightedPositions[1] = velocityPos();
            weightedPositions[0] = weightedPositions[3];
            
            for (int i = 2; i < weightedPositions.Length; i++)
            {
                weightedPositions[i] = RandomSpace();
            }
            time = 0;
        }

        //position
        transform.position = newPosition();
        time += moveStop * Time.deltaTime / timeTake;

        //rotation
        Vector3 relPosEye = player.transform.position - eye.transform.position;
        eye.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(relPosEye, Vector3.up), 1f);
        Vector3 relPosBar = player.transform.position - healthBar.transform.position;
        healthBar.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(relPosBar, Vector3.up), 1f);

        attackTime += Time.deltaTime;

        //starts a random attack
        if (attackTime > 8f)
        {
            if (Random.Range(0, 1f) > .5)
            {
                StartCoroutine(ShootBeer());
            }
            else
            {
                StartCoroutine(ShootLazer());
            }
            attackTime = 0;
        }
    }

    public void ResetPos()
    {
        //picks 4 new points and resets the bezier curve
        time = 0;
        teleportParticals.Play();
        for (int i = 0; i < weightedPositions.Length; i++)
        {
            weightedPositions[i] = RandomSpace();
        }
        transform.position = newPosition();
        teleportParticals.Play();
    }

    public Vector3 RandomSpace()
    {
        //picks random position in a spherical shell around player
        float arround = Random.Range(0, 2 * Mathf.PI);
        float up = Random.Range(0, Mathf.PI);
        return new Vector3(Mathf.Sin(arround) * Mathf.Sin(up), Mathf.Sin(up), Mathf.Cos(arround) * Mathf.Sin(up)) * distRadius * Random.Range(.5f, 1.5f) + player.transform.position;
    }

    public Vector3 newPosition()
    {
        //finds the place boss should be after the time
        Vector3 newPos = Vector3.zero;
        newPos += weightedPositions[0] * (-Mathf.Pow(time, 3f) + 3 * time * time - 3 * time + 1f);
        newPos += weightedPositions[1] * ( 3 * Mathf.Pow(time, 3f) - 6 * time * time + 3 * time);
        newPos += weightedPositions[2] * (-3 * Mathf.Pow(time, 3f) + 3 * time * time);
        newPos += weightedPositions[3] * Mathf.Pow(time, 3f);
        return newPos;
    }

    public Vector3 velocityPos()
    {
        // picks second point on bezier curve (first one being the last point on the last bezie curve) so that the path looks smooth
        Vector3 velPos = Vector3.zero;
        velPos += weightedPositions[0] * (-3 * Mathf.Pow(time, 2f) + 6 * time - 3);
        velPos += weightedPositions[1] * (9 * Mathf.Pow(time, 2f) - 12 * time + 3);
        velPos += weightedPositions[2] * (-9 * Mathf.Pow(time, 2f) + 6 * time);
        velPos += weightedPositions[3] * 3 * Mathf.Pow(time, 2f);
        return velPos / (distRadius * newPower) + weightedPositions[3];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            FindObjectOfType<Lives>().Hurt(collideDamage);
            ResetPos();
        }
        if(other.CompareTag("Slicer"))
        {
            healthScript.TakeDamage(sliceDamage);
            ResetPos();
        }
    }

    IEnumerator ShootBeer()
    {
        //shoots beers at the player
        for (int i = 0; i < 10; i++)
        {
            float shootAngle = Random.Range(0f, 45f);
            shootPoint.rotation = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0f, player.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z));
            shootPoint.rotation = Quaternion.Euler(Vector3.left * shootAngle + shootPoint.rotation.eulerAngles);
            GameObject bottle = MyFunctions.Shoot("Fruit", shootAngle, MyFunctions.FindDistError(flipError), shootPoint, player.transform, deathBeer, "ShootSound");
            bottle.GetComponent<Deathbeer>().breakable = true;
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator ShootLazer()
    {
        //corrotine that goes through and executes shooting the lazer at the player
        lazerScript.LazerEnable();
        while (moveStop > 0)
        {
            moveStop -= .01f;
            yield return null;
        }
        lazerScript.targetLocked = true;
        yield return new WaitForSeconds(1);
        while (lazerScript.lineRen.endWidth < .8)
        {
            float newValue = lazerScript.lineRen.endWidth + 8f * Time.deltaTime;
            lazerScript.lineRen.SetWidth(newValue, newValue);
            yield return null;
        }
        lazerScript.lineRen.SetWidth(.8f, .8f);
        if (lazerScript.Hit())
        {
            FindObjectOfType<Lives>().Hurt(burnDamage);
        }

        yield return new WaitForSeconds(.5f);
        while (lazerScript.lineRen.endWidth > .03f)
        {
            float newValue = lazerScript.lineRen.endWidth - 8f * Time.deltaTime;
            lazerScript.lineRen.SetWidth(newValue, newValue);
            yield return null;
        }
        lazerScript.lineRen.SetWidth(.03f, .03f);

        lazerScript.targetLocked = false;
        lazerScript.LazerDisable();
        while (moveStop < 1)
        {
            moveStop += .01f;
            yield return null;
        }
        
    }
}
