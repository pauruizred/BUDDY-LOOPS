using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopController : MonoBehaviour
{

    // Audio
    private AudioSource source;
    public float maxVol;
    public float fadeVelocity;
    public float startDelay;

    // Visuals
    private SpriteRenderer visuals;
    private ParticleSystem particles;
    public GameObject pGO;
    public GameObject backgroundMask;
    private Color visualsColor;

    // Utils
    [HideInInspector]
    public int counter;
    public bool fixer;
    public bool armer;

    // Start is called before the first frame update
    void Start()
    {
        // Audio
        source = GetComponent<AudioSource>();

        // Visuals 
        visuals = GetComponentInChildren<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        backgroundMask.SetActive(false);
        GetComponent<Rotate>().enabled = false;
        visuals.color = Color.red;
        //ChangeOpacity(0f);

        // Utils
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
                ActivateLoop();
            }

            if (counter < 2)
            {
                armer = false;
            }

            if ((counter == 2) && (armer == false))
            {
                fixer = true;
                visuals.color = Color.white;
                GetComponent<Rotate>().enabled = true;
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
                visuals.color = Color.red;
                GetComponent<Rotate>().enabled = false;
            }
        }
    }

    private void ActivateLoop()
    {
        StartCoroutine("FadeInLoop");

        pGO.SetActive(true);
        visuals.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        backgroundMask.SetActive(true);

    }

    private void DeactivateLoop()
    {
        StopCoroutine("FadeInLoop");

        pGO.SetActive(false);
        visuals.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        StartCoroutine("FadeOutLoop");
        backgroundMask.SetActive(false);
        //ChangeOpacity(0f);

    }

    IEnumerator FadeInLoop()
    {

        yield return new WaitForSeconds(startDelay);

        while (source.volume < maxVol){
            source.volume += Time.deltaTime/10;
        }

        /*for (float vol = 0f; vol < maxVol; vol += fadeVelocity)
        {
            source.volume += vol;
            // ChangeOpacity(vol*255);
            yield return new WaitForSeconds(0.1f);

        }*/

    }

        IEnumerator FadeOutLoop()
    {

        yield return new WaitForSeconds(startDelay);
        
        /*for (float vol = 1; vol > 0; vol -= fadeVelocity)
        {
            source.volume -= vol;
            // ChangeOpacity(vol*255);
            yield return new WaitForSeconds(0.1f);

        }*/
        while (source.volume >= 0){
            source.volume -= Time.deltaTime/10;
        }

    }


    // Experimento para hacer fade in de visuals, no implementado de momento
    private void ChangeOpacity(float newOpacity)
    {
        Color temp = visuals.color;
        temp.a = newOpacity;
        visuals.color = temp;
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
