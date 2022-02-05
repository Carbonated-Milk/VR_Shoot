using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFunction : MonoBehaviour
{
    Vector3 origin;
    public bool turning;

    public float delay;

    public float height;

    public float turningSpeed;

    public GameObject brokenHeart;
    void Start()
    {
        origin = transform.position;
        StartCoroutine(StartSpin());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = origin + new Vector3(0f, Mathf.Cos(Time.time - delay), 0f) * height;
        if(Mathf.Cos(Time.time - delay) > .9 && -Mathf.Sin(Time.time - delay) > 0 || turning)
        {
            turning = true;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(-90f, 0f, 180)), 1 / turningSpeed);
        }
        if(Mathf.Cos(Time.time - delay) < 0f)
        {
            turning = false;
            transform.rotation = Quaternion.Euler(Vector3.left * 90f);
        }
    }

    IEnumerator StartSpin()
    {
        yield return  new WaitForSeconds(1);

        GetComponent<Animator>().enabled = false;
    }

    public void Broken()
    {
        GameObject broken = Instantiate(brokenHeart, transform.position, transform.rotation);
        broken.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.right * 90);
        Destroy(gameObject);
    }
}
