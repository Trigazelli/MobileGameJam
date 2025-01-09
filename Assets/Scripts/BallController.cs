using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private MousePositionStocker stocker;

    private void Update()
    {
        List<Vector3> pointList = new();
        Vector3 distance = transform.position - stocker.mousePos;
        //Debug.Log(distance);
        Debug.DrawRay(transform.position, distance, Color.white);
        for (int i = 0; i < 10; i++)
        {
            float temp = i / 10f;
            Debug.Log(temp);
            pointList.Add(Vector3.Slerp(transform.position, distance, temp));
            Debug.DrawLine(Vector3.Slerp(transform.position, distance, temp), Vector3.Slerp(transform.position, distance, temp + 0.1f));
        }

    }
}
