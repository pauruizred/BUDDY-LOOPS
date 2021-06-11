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
    public Gradient grad1;
    public Gradient grad2;
    //public ParticleSystem particles;
    public GameObject backgroundMask;
    private Color visualsColor;
    public ParticleSystem pGO;
    public ParticleSystem oneShotpGO;

    // Utils
    [HideInInspector]
    public int counter;
    public bool fixer;
    public bool armer;
    public float fixingTime;
    public bool canRipple;
    public bool loopActivated;


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
        pGO = this.transform.Find("Continuous ripple").GetComponentInChildren<ParticleSystem>();
        var col = pGO.colorOverLifetime;
        col.color = grad1;
        backgroundMask.SetActive(false);
        GetComponent<Rotate>().enabled = false;

        // Utils
        counter = 0;
        fixer = false;
        armer = false;
        canRipple = true;

        // others
        vinyl = FindObjectOfType<Vinyl>();
        loopActivated=false;

        if (!oneShotpGO.isPlaying) oneShotpGO.Play();
    }



    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Center").GetComponent<Center>().noTurnBack == false)
        {
            //Debug.Log(Input.mousePosition);
            if (fixer == false)
            {
                if (counter == 0)
                {
                    if (loopActivated == true)
                    {
                        loopActivated = false;
                        DeactivateLoop();
                    }
                }
                else //(counter > 0)
                {
                    if (loopActivated == false)
                    {
                        loopActivated = true;
                        ActivateLoop();
                    }
                }

                if (counter < 2)
                {
                    armer = false;
                    StopCoroutine("Fix");
                }

                if ((counter == 2) && (armer == false))
                {
                    StopCoroutine("Unfix");
                    StartCoroutine("Fix");
                }

            }
            else //(fixer == true)
            {
                if (counter < 2)
                {
                    armer = true;
                    StopCoroutine("Unfix");
                }

                if ((counter == 2) && (armer == true))
                {
                    StopCoroutine("Fix");
                    StartCoroutine("Unfix");
                }
            }
        }
    }
    IEnumerator Fix()
    {
        yield return new WaitForSeconds(startDelay);
        fixer = true;
        var col = pGO.colorOverLifetime;
        col.color = grad2;
        Ripple();
    }
    IEnumerator Unfix()
    {
        yield return new WaitForSeconds(startDelay);
        fixer = false;
        var col = pGO.colorOverLifetime;
        col.color = grad1;
        Ripple();
        
        //canRipple = true;
    }

    private void ActivateLoop()
    {
        StopCoroutine("FadeOutLoop");
        StartCoroutine("FadeInLoop");

        pGO.Play();
        backgroundMask.SetActive(true);
    }

    public void DeactivateLoop()
    {
        StopCoroutine("FadeInLoop");
        StartCoroutine("FadeOutLoop");

        pGO.Stop();
        backgroundMask.SetActive(false);

    }

    IEnumerator FadeInLoop()
    {
        
        yield return new WaitForSeconds(startDelay);

        while (source.volume < maxVol) {
            source.volume += fadeVelocity;
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator FadeOutLoop()
    {
        while (source.volume >= 0) {
            source.volume -= fadeVelocity;
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
            Debug.Log(transform.position);
            var cameras = FindObjectsOfType<Camera>();
            foreach (Camera camera in cameras)
            {
                camera.GetComponent<RipplePostProcessor>().RippleAtPoint(transform.position);
            }
        }
        //canRipple = false;
    }
}
