using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    private AudioSource source;
    public int counter;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            source.mute = true;
        }
        else if (counter == 1)
        {
            source.mute= false;
        }
        else if (counter == 2)
        {
            source.mute= false;
            //poner algo mas para fijar el loop
        }
    }
}
