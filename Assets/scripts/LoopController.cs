using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    private AudioSource source;
    private SpriteRenderer visuals;
    private ParticleSystem particles;

    public int counter;

    public bool fixer;
    public bool armer;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        visuals = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();

        counter = 0;
        fixer = false;
        armer = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        LoopManager();

       
    }

    void LoopManager()
    {
        if (fixer == false)
        {
            if (counter == 0)
            {
                source.mute = true;
                particles.Stop();
            }
            else //(counter > 0)
            {
                source.mute = false;
                particles.Play();
            }

            if (counter < 2)
            {
                armer = false;
            }

            if ((counter == 2) && (armer == false))
            {
                fixer = true;
                visuals.color = Color.grey;
            }

        }
        else //(fixer == true)
        {
            if (counter < 2)
            {
                armer = true;
            }

            if ((counter == 2) && (armer == true))
            {
                fixer = false;
                visuals.color = Color.white;
            }
        }



        /*if (fixer == false)
        {
            if (counter == 0)
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
                counter = 0;
            }
        }
        else //(fixer == true)
        {
            if (counter == 0)
            {
                source.mute = true;
            }

            else if (counter == -2)
            {
                counter = 0;
            }
        }*/
    }
}
