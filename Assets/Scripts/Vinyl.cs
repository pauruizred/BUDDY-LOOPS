using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinyl : MonoBehaviour
{
    //Space
    public int numberOfLines;
    public float centerStickerRadius;

    private Vector3 center;
    private int radius;

    //VinylLines
    public GameObject vinylLinePrefab;

    private List<GameObject> vinylLines;
    
    void Start()
    {
        center = transform.position;
        radius = 70;
        vinylLines = new List<GameObject>();
        InstantiateLines();
    }

    private void InstantiateLines()
    {
        for (int i = 1; i <= numberOfLines; i++)
        {
            float xPosOffset = i * (radius / numberOfLines) + centerStickerRadius;
            GameObject vinylLine = Instantiate(vinylLinePrefab, center + new Vector3(xPosOffset, 0, 0), Quaternion.identity);
            vinylLine.transform.parent = gameObject.transform;
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
