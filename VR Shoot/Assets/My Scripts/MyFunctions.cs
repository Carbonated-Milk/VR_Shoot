using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFunctions : MonoBehaviour
{
    public static GameObject Shoot(string objectType, float shootAngle, Vector3 distError, Transform shootPoint, Transform target, GameObject thrownObj, string AudioName)
    {
        FindObjectOfType<AudioManager>().Play(AudioName);

        float shootAngleRad = Mathf.Deg2Rad * shootAngle;

        float gravity = Physics.gravity.y;

        float distance = (new Vector3(target.position.x, 0f, target.position.z) - new Vector3(shootPoint.position.x, 0f, shootPoint.position.z) - distError).magnitude;

        float heightDifference = target.position.y - shootPoint.position.y;

        float objVelocity = Mathf.Sqrt(-gravity * distance / (2 * (Mathf.Cos(shootAngleRad) * (Mathf.Sin(shootAngleRad) - Mathf.Cos(shootAngleRad) * heightDifference / distance))));

        //Debug.Log(fruitVelocity);
        if (objVelocity != float.NaN)
        {
            GameObject spawnedObj = Instantiate(thrownObj) as GameObject;
            spawnedObj.transform.position = shootPoint.position;
            spawnedObj.GetComponent<Rigidbody>().velocity = shootPoint.transform.forward * objVelocity;
            switch (objectType)
            {
                case "Fruit":
                    spawnedObj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));
                    break;
                case "Sawblade":
                    spawnedObj.transform.rotation = Quaternion.Euler(shootPoint.rotation.eulerAngles + Vector3.left * 90);
                    spawnedObj.GetComponent<Rigidbody>().maxAngularVelocity = 1000f;
                    break;
            }

            return spawnedObj;
        }

        return null;
    }

    public static Vector3 FindDistError(float shootError)
    {
        float random = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 distError = new Vector3(Mathf.Sin(random), 0f, Mathf.Cos(random)) * Random.Range(0f, 1f) * shootError;
        return distError;
    }

}
