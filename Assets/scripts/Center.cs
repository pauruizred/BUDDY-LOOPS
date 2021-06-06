using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour
{
    public int counter;
    public float waitTime;
    public int nextScene;

    public float delayEnding;
    public GameObject linePrefab;
    private GameObject endingline;

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

    }

    // Update is called once per frame
    void Update()
    {
        if ((counter == 2) && (Time.timeSinceLevelLoad > waitTime)) //&& (endLevelCalled == false))
        {
            StartCoroutine("EndLevel");
            endLevelCalled = true;
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
        endingline.GetComponent<TrailRenderer>().emitting = true;
        endingline.GetComponent<TrailRenderer>().time = 5;
        yield return new WaitForSeconds(delayEnding);
        noTurnBack = true;
        endingline.GetComponent<TrailRenderer>().emitting = false;
        FindObjectOfType<LoopsMaster>().Finish();
        FindObjectOfType<Vinyl>().SetEmission(false);
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(nextScene);
    }
}
