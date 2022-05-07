using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBackgroundManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float loopDistance;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position -= Vector3.right * speed * Time.deltaTime;
        if ((transform.position - startPosition).magnitude > loopDistance)
        {
            transform.position += Vector3.right * loopDistance;
        }
    }
}
