using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour
{
    public int counter;
    public float waitTime;
    public int nextScene;
    public int waitForNextScene;

    public float delayEnding;
    public GameObject linePrefab;
    private GameObject endingline;
    private ParticleSystem wave;


    private bool noTurnBack;
    private bool endLevelCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        endingline = Instantiate(linePrefab, transform.position + new Vector3(5, 0, 0), Quaternion.identity);
        endingline.GetComponent<TrailRenderer>().widthMultiplier = 22f;
        endingline.GetComponent<TrailRenderer>().emitting = false;
        endingline.GetComponent<TrailRenderer>().time = 1;
        wave = this.GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((counter == 2) && (Time.timeSinceLevelLoad > waitTime))
        {
            wave.Play();
            if (endLevelCalled == false){
                endLevelCalled = true;
                StartCoroutine("EndLevel");
            }
                
        }
        else
        {
            endingline.GetComponent<TrailRenderer>().emitting = false;
            //endingline.GetComponent<TrailRenderer>().time -= Time.deltaTime;
            if (noTurnBack == false) {
                StopCoroutine("EndLevel");
                endLevelCalled = false;
            }
            
        } 
    }
    IEnumerator EndLevel()
    {
        //Debug.Log("EndLevel");
        endingline.GetComponent<TrailRenderer>().emitting = true;
        endingline.GetComponent<TrailRenderer>().time = delayEnding;
        yield return new WaitForSeconds(delayEnding);
        noTurnBack = true;
        //Debug.Log("no turn back true");
        endingline.GetComponent<TrailRenderer>().emitting = false;
        //Debug.Log("prefinish");
        FindObjectOfType<LoopsMaster>().Finish();
        //Debug.Log("post finish");
        FindObjectOfType<Vinyl>().SetEmission(false);
        yield return new WaitForSeconds(waitForNextScene);
        SceneManager.LoadScene(nextScene);
    }
}
