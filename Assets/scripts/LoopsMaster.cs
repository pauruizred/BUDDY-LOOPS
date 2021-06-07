using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsMaster : MonoBehaviour
{
    public float finalFadeVelocity;
    public List<LoopController> loops;// = new List<LoopController>();
    // Start is called before the first frame update
    void Awake()
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
            loop.fadeVelocity = finalFadeVelocity * 10;
            if (loop.fixer == true)
            {
                loop.DeactivateLoop();
            }
        }
    }

}
