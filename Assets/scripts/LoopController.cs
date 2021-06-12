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
    public Gradient grad1;
    public Gradient grad2;

    //public ParticleSystem particles;
    private ParticleSystem pGO;

    // Utils
    [HideInInspector]
    public int counter;
    private bool fixer;
    private bool armer;
    private bool loopActivated;

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

        // Utils
        counter = 0;
        fixer = false;
        armer = false;
        loopActivated=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Center").GetComponent<Center>().noTurnBack == false)
        {
            if (fixer == false)
            {
                if (counter == 0)
                {
                    if (loopActivated == true)
                    {
                        DeactivateLoop();
                    }
                }
                else //(when counter > 0)
                {
                    if (loopActivated == false)
                    {
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
            else //(when fixer == true)
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
    }

    private void ActivateLoop()
    {
        loopActivated = true;

        StopCoroutine("FadeOutLoop");
        StartCoroutine("FadeInLoop");

        pGO.Play();
    }

    public void DeactivateLoop()
    {
        loopActivated = false;

        StopCoroutine("FadeInLoop");
        StartCoroutine("FadeOutLoop");

        pGO.Stop();
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

    //modificar si es pot
    private void Ripple()
    {
        Debug.Log(transform.position);
        var cameras = FindObjectsOfType<Camera>();
        foreach (Camera camera in cameras)
        {
            camera.GetComponent<RipplePostProcessor>().RippleAtPoint(transform.position);
        }
    }
}
