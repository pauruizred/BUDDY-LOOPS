using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsMaster : MonoBehaviour
{
    public List<LoopController> loops;
    // Start is called before the first frame update
    void Start()
    {
        loops = new List<LoopController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Finish()
    {
        foreach (LoopController loop in loops)
        {
            if (loop.fixer == true)
            {
                loop.DeactivateLoop();
            }
        }
    }

}
