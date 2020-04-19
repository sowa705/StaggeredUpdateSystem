using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycle : StaggeredUpdateBehaviour
{
    public Gradient g;
    public float timer=2;
    Quaternion targetRot;
    // Start is called before the first frame update
    public override void SStart()
    {
    }

    // Update is called once per frame
    public override void SUpdate(float deltaTime)
    {
        timer += deltaTime/5f;
        if (timer>1)
        {
            timer = 0;
            targetRot = Random.rotation;
        }
        GetComponent<MeshRenderer>().material.color = g.Evaluate(timer);
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRot,deltaTime);
    }
}
