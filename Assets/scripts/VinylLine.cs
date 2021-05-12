using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VinylLine : MonoBehaviour
{

    private Vector3 center;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        center = new Vector3(50, 0, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, new Vector3(0, 1, 0), rotationSpeed * Time.deltaTime);
    }
}
