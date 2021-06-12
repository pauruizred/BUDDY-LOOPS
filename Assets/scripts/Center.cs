using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour
{
    //Visuals
    public GameObject linePrefab;

    private GameObject endingline;
    private ParticleSystem pS;
    public GameObject sticker;

    //Utils
    [HideInInspector]
    public int counter;

    public float waitTime;
    public float holdingTime;

    public int nextScene;
    public int waitForNextScene;

    [HideInInspector]
    public bool noTurnBack = false;
    private bool endLevelCalled = false;

    void Start()
    {
        //Utils
        counter = 0;

        //Visuals
        endingline = Instantiate(linePrefab, transform.position + new Vector3(6.3f, 0, 0), Quaternion.identity);
        endingline.GetComponent<TrailRenderer>().widthMultiplier = 27.5f;
        endingline.GetComponent<TrailRenderer>().emitting = false;
        endingline.transform.parent = gameObject.transform;

        pS = this.GetComponent<ParticleSystem>();

        Invoke("WaitTimeExpired", waitTime);

    }
    
    void Update()
    {
        if ((counter == 2) && (Time.timeSinceLevelLoad > waitTime))
        {
            if (endLevelCalled == false){
                endLevelCalled = true;
                StartCoroutine("EndLevel");
            }  
        }
        else
        {
            endingline.GetComponent<TrailRenderer>().emitting = false;

            if (noTurnBack == false) {
                StopCoroutine("EndLevel");
                endLevelCalled = false;
            }
        } 
    }
    IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(1);
        endingline.GetComponent<TrailRenderer>().emitting = true;
        endingline.GetComponent<TrailRenderer>().time = holdingTime;
        yield return new WaitForSeconds(holdingTime);
        noTurnBack = true;
        FindObjectOfType<LoopsMaster>().Finish();
        endingline.GetComponent<TrailRenderer>().emitting = false;
        FindObjectOfType<Vinyl>().SetEmission(false);
        sticker.SetActive(false);
        yield return new WaitForSeconds(holdingTime + 2);
        yield return new WaitForSeconds(waitForNextScene);
        SceneManager.LoadScene(nextScene);
    }
    void WaitTimeExpired()
    {
        pS.Play();
    }
}
