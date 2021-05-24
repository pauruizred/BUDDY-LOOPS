using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinyl : MonoBehaviour
{

    private Vector3 center;
    private int radius;
    public int numberOfLines;
    public float centerStickerRadius;
    public GameObject vinylLinePrefab;
    private List<GameObject> vinylLines;
    


    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        radius = 70;
        vinylLines = new List<GameObject>();
        InstantiateLines();
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    private void InstantiateLines()
    {
        for (int i = 1; i <= numberOfLines; i++)
        {
            Debug.Log(i);
            float xPosOffset = i * (radius / numberOfLines) + centerStickerRadius;
            GameObject vinylLine = Instantiate(vinylLinePrefab, center + new Vector3(xPosOffset, 0, 0), Quaternion.identity);
            vinylLines.Add(vinylLine);
        }
    }
    public void SetEmission(bool turnon)
    {
        foreach (GameObject vinylLine in vinylLines){
            vinylLine.GetComponent<TrailRenderer>().emitting = turnon;
        }
    }
}
