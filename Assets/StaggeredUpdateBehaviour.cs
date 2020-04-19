using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StaggeredUpdate;

public class StaggeredUpdateBehaviour : MonoBehaviour
{
    bool initialized;
    public float LastUpdate;
    // Start is called before the first frame update
    void Start()
    {
        if (UpdateManager.Instance!=null)
        {
            Initialize();
        }
    }

    void Initialize()
    {
        initialized = true;
        UpdateManager.Instance.RegisterObject(this);
    }

    void OnDestroy()
    {
        if (initialized)
        {
            UpdateManager.Instance.UnregisterObject(this);
        }
    }

    public virtual void SStart()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            if (UpdateManager.Instance != null)
            {
                Initialize();
            }
        }
    }
    public virtual void SUpdate(float deltaTime)
    {
    }
}
