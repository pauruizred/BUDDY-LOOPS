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
    public float centerMaxSize;

    public float delayEnding;
    public GameObject linePrefab;
    private GameObject endingline;
    private ParticleSystem ps;
    public GameObject sticker;


    private bool noTurnBack;
    private bool endLevelCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        endingline = Instantiate(linePrefab, transform.position + new Vector3(6.3f, 0, 0), Quaternion.identity);
        endingline.GetComponent<TrailRenderer>().widthMultiplier = 27.5f;
        endingline.GetComponent<TrailRenderer>().emitting = false;
        endingline.GetComponent<TrailRenderer>().time = 1;
        ps = this.GetComponent<ParticleSystem>();
        //FadeInSticker();
        Invoke("WaitTimeExpired", waitTime);

    }

    // Update is called once per frame
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
        sticker.SetActive(false);
        yield return new WaitForSeconds(waitForNextScene);
        SceneManager.LoadScene(nextScene);
    }
    IEnumerator FadeInSticker()
    {
        while (sticker.transform.localScale.x < centerMaxSize)
        {
            var x = sticker.transform.localScale.x;
            x += 0.1f;

            var z = sticker.transform.localScale.z;
            z += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    void WaitTimeExpired()
    {
        ps.Play();
    }
}
