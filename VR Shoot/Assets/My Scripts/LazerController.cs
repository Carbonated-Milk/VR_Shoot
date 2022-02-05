using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour
{
    public LineRenderer lineRen;
    public LayerMask hit;
    private Vector3[] points;

    public GameObject eye;

    public bool targetLocked;

    private void Awake()
    {
        lineRen = GetComponent<LineRenderer>();
        points = new Vector3[2];
    }

    public void SetUpLine(Vector3[] points)
    {
        lineRen.positionCount = points.Length;
        this.points = points;
    }

    public void Update()
    {
        if (!targetLocked)
        {
            points[0] = eye.transform.position;
            points[1] = eye.transform.forward * 50 + eye.transform.position;
            for (int i = 0; i < points.Length; i++)
            {
                lineRen.SetPosition(i, points[i]);
            }
        }
    }
    public void LazerEnable()
    {
        lineRen.enabled = true;
    }
    public void LazerDisable()
    {
        lineRen.enabled = false;
    }

    public bool Hit()
    {
        return Physics.Raycast(eye.transform.position, eye.transform.forward, hit);
    }
}
