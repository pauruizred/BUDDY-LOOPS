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
        //visuals = GetComponentInChildren<SpriteRenderer>();
        pGO = this.transform.Find("Continuous ripple").GetComponentInChildren<ParticleSystem>();
        var col = pGO.colorOverLifetime;
        col.color = grad1;
        oneShotpGO = this.transform.Find("One shot ripple").GetComponentInChildren<ParticleSystem>();
        backgroundMask.SetActive(false);
        GetComponent<Rotate>().enabled = false;
        //visuals.color = Color.red;
        //ChangeOpacity(0f);

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
        //Debug.Log(Input.mousePosition);
        if (fixer == false)
        {
            if (counter == 0)
            {            
                if (loopActivated == true){
                    loopActivated=false;
                    DeactivateLoop();
                }              
            }
            else //(counter > 0)
            {
                if (loopActivated == false){
                    loopActivated=true;
                    ActivateLoop();
                }          
            }

            if (counter < 2)
            {
                armer = false;
            }

            if ((counter == 2) && (armer == false))
            {
                fixer = true;
                //GetComponent<Rotate>().enabled = true;    
                var col = pGO.colorOverLifetime;
                col.color = grad2;

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
                var col = pGO.colorOverLifetime;
                col.color = grad1;
                canRipple = true;
            }
        }
    }

    private void ActivateLoop()
    {
       
        StopCoroutine("FadeOutLoop");
        StartCoroutine("FadeInLoop");
        
        if (!pGO.isPlaying) pGO.Play();
        //visuals.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        backgroundMask.SetActive(true);

    }

    public void DeactivateLoop()
    {
        StopCoroutine("FadeInLoop");
        StartCoroutine("FadeOutLoop");

        if (pGO.isPlaying) pGO.Stop();
        //visuals.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        backgroundMask.SetActive(false);
        //ChangeOpacity(0f);

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
            var camera = FindObjectOfType<Camera>();
            Debug.Log(transform.position);
            camera.GetComponent<RipplePostProcessor>().RippleAtPoint(transform.position);
        }
        canRipple = false;
    }
}
