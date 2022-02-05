using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRing : MonoBehaviour
{
    public GameObject ringObject;

    public ObjRingType[] ringInfo;

    //public int[] arrayNums;

    private GameObject[][] ringObjectHolder;

    private Transform[] emptys;

    float offsetRotate = 0;
    void Start()
    {
        ringObjectHolder = new GameObject[ringInfo.Length][];
        emptys = new Transform[ringInfo.Length];
        for (int j = 0; j < ringInfo.Length; j++)
        {
            ringObjectHolder[j] = new GameObject[ringInfo[j].objCount];
            emptys[j] = new GameObject().transform;
            emptys[j].transform.parent = gameObject.transform;
            emptys[j].transform.localPosition = Vector3.zero;

            for (int i = 0; i < ringInfo[j].objCount; i++)
            {
                ringObjectHolder[j][i] = Instantiate(ringObject);
                ringObjectHolder[j][i].transform.parent = emptys[j];
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < ringInfo.Length; j++)
        {
            for (int i = 0; i < ringInfo[j].objCount; i++)
            {
                float objTime = i * 360 / ringInfo[j].objCount;
                ringObjectHolder[j][i].transform.localPosition = new Vector3(Mathf.Cos(offsetRotate * ringInfo[j].moveSpeed + objTime * Mathf.Deg2Rad), 0f, Mathf.Sin(offsetRotate * ringInfo[j].moveSpeed + objTime * Mathf.Deg2Rad)) * ringInfo[j].radius * (1f + .5f * Mathf.Cos(Time.time / 2.8f * j - 3 * j) * Mathf.Sin(Time.time * 4f * j + 4 * j));
                ringObjectHolder[j][i].transform.rotation = Quaternion.Euler(Vector3.one * (objTime + offsetRotate * 100 * ringInfo[j].rotateSpeed));
            }

            emptys[j].rotation = Quaternion.Euler(Vector3.one * (offsetRotate * 100 * ringInfo[j].axisRotateSpeed));
        }
        
        offsetRotate += 1f * Time.deltaTime;
    }
}
