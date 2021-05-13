using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicians : MonoBehaviour
{
    // Start is called before the first frame update
    private /*IEnumerator*/ void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Loop"))
        {
            //yield return new WaitForSeconds(1);
            other.GetComponent<LoopController>().counter++;
        }
        if (other.gameObject.CompareTag("Center"))
        {
            //yield return new WaitForSeconds(1);
            other.GetComponent<Center>().counter++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Loop"))
        {
            other.GetComponent<LoopController>().counter--;
        }
        if (other.gameObject.CompareTag("Center"))
        {
            other.GetComponent<Center>().counter--;
        }
    }
}
