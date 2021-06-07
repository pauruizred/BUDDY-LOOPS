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
    public float fixingTime;
    public bool canRipple;

    // Other game objects
    private Vinyl vinyl;



    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponentInParent<LoopsMaster>().loops.Add(this);

        // Audio
        source = GetComponent<AudioSource>();
        source.volume = 0;
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
        canRipple = true;

        // others
        vinyl = FindObjectOfType<Vinyl>();
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition);
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
                Debug.Log(transform.position);
                Ripple();
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
                canRipple = true;
            }
        }
    }

    private void ActivateLoop()
    {
        StopCoroutine("FadeOutLoop");
        StartCoroutine("FadeInLoop");

        pGO.SetActive(true);
        visuals.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        backgroundMask.SetActive(true);

    }

    public void DeactivateLoop()
    {
        StopCoroutine("FadeInLoop");
        StartCoroutine("FadeOutLoop");

        pGO.SetActive(false);
        visuals.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        backgroundMask.SetActive(false);
        //ChangeOpacity(0f);

    }

    IEnumerator FadeInLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (source.volume < maxVol) {
            source.volume += Time.deltaTime * fadeVelocity;
            yield return new WaitForSeconds(0.1f);
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
        /*for (float vol = 1; vol > 0; vol -= fadeVelocity)
        {
            source.volume -= vol;
            // ChangeOpacity(vol*255);
            yield return new WaitForSeconds(0.1f);

        }*/
        while (source.volume >= 0) {
            source.volume -= Time.deltaTime * fadeVelocity;
            yield return new WaitForSeconds(0.1f);
        }

    }


    // Experimento para hacer fade in de visuals, no implementado de momento
    private void ChangeOpacity(float newOpacity)
    {
        Color temp = visuals.color;
        temp.a = newOpacity;
        visuals.color = temp;
    }

    private void Ripple()
    {
        if (canRipple)
        {
            var camera = FindObjectOfType<Camera>();
            Debug.Log(transform.position);
            camera.GetComponent<RipplePostProcessor>().RippleAtPoint(transform.position);
        }
        canRipple = false;
    }
}

    /*void LoopManager()
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
        }
    }
}*/
