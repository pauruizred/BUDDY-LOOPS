using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    private AudioSource source;
    private SpriteRenderer visuals;
    private ParticleSystem particles;
    public GameObject pGO;

    public float maxVol;
    public float fadeVelocity;
    public GameObject backgroundMask;
    [HideInInspector]

    public int counter;

    public bool fixer;
    public bool armer;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        visuals = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();

        backgroundMask.SetActive(false);

        counter = 0;
        fixer = false;
        armer = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (fixer == false)
        {
            if (counter == 0)
            {
                DeactivateLoop();
            }
            else //(counter > 0)
            {
                NewMethod();
            }

            if (counter < 2)
            {
                armer = false;
            }

            if ((counter == 2) && (armer == false))
            {
                fixer = true;
                //visuals.color = Color.grey;
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
    }

    private void NewMethod()
    {
        StartCoroutine("FadeInLoop");


        visuals.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        backgroundMask.SetActive(true);
    }

    IEnumerator FadeInLoop()
    {
        //pGO.SetActive(true);

        yield return new WaitForSeconds(2);

        for (float vol = 0f; vol < maxVol; vol += fadeVelocity)
        {
            source.volume += vol;
            yield return new WaitForSeconds(0.1f);

        }
        /*if (source.volume < maxVol)
        {
            source.volume += Time.deltaTime/5;
        }*/
    }


    private void DeactivateLoop()
    {
        StopCoroutine("FadeInLoop");
        visuals.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        source.volume = 0;
        backgroundMask.SetActive(false);
        //pGO.SetActive(false);
    }




    void LoopManager()
    {
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
