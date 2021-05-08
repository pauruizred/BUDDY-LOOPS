using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Center : MonoBehaviour
{
    public int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 2)
        {
            Debug.Log("GAME OVER");
            SceneManager.LoadScene(0);

        }
    }
}
