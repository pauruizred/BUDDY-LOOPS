using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    private AudioSource source;
    public bool fixer;
    public int counter;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        counter = 0;
        fixer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fixer == false)
        {
            if (counter < 0)
            {
                counter = 0;
            }
            else if (counter == 0)
            {
                source.mute = true;
            }
            else if (counter == 1)
            {
                source.mute = false;
            }
            else if (counter == 2)
            {
                source.mute = false;
                fixer = true;
            }
        }
        if (fixer == true)
        {
            if (counter == -2)
            {
                fixer = false;
            }
        }
        
    }
}
