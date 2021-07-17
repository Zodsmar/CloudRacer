using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundFocal : MonoBehaviour
{
    private float rotationSpeed = 25f;
    public GameObject focalPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(focalPoint.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
