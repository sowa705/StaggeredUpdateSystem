# StaggeredUpdateSystem
Staggered update system is used to dynamically change update rate of MonoBehaviours for increased performance under high load
# Example
To use stagerred update system just extend StaggeredUpdateBehaviour.
```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycle : StaggeredUpdateBehaviour
{
    public Gradient g;
    public float timer;
    
    public override void SStart()
    {
    }
    public override void SUpdate(float deltaTime)
    {
        timer += deltaTime/5f;
        if (timer>1)
        {
            timer = 0;
            targetRot = Random.rotation;
        }
        GetComponent<MeshRenderer>().material.color = g.Evaluate(timer);
    }
}

```
