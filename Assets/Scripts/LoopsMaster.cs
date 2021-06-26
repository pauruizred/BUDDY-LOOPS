using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopsMaster : MonoBehaviour
{
    public float finalFadeVelocity;

    [HideInInspector]
    public List<LoopController> loops;

    void Awake()
    {
        loops = new List<LoopController>();
    }

    public void Finish()
    {
        foreach (LoopController loop in loops)
        {
            loop.fadeVelocity = finalFadeVelocity;
            loop.DeactivateLoop();
        }
    }

}
