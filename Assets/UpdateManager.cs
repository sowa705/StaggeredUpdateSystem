using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace StaggeredUpdate
{
    public class UpdateManager : MonoBehaviour
    {
        List<UpdateType> UpdateList=new List<UpdateType>();
        public float TargetFrameTime = 0.016f;
        int lerpedfillrate;
        public float CurrentFillRate;
        public float MaxSingleTypeUpdatePercentage = 0.5f;
        public float CurrentTimeBudget;

        public float ScriptUpdateTime;
        public float RenderingTime;

        public static UpdateManager Instance;
        public Stopwatch Timer=new Stopwatch();
        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        // Update is called once per frame
        void Update()
        {
            Timer.Stop();
            RenderingTime = (float)Timer.Elapsed.TotalSeconds;
            Timer.Restart();
        }
        private void OnGUI()
        {
            GUI.Label(new Rect(8,8,200,30), new GUIContent(lerpedfillrate.ToString()));
        }
        void LateUpdate()
        {
            Timer.Stop();
            ScriptUpdateTime = (float)Timer.Elapsed.TotalSeconds;
            Timer.Restart();
            CurrentTimeBudget = TargetFrameTime - ScriptUpdateTime - RenderingTime;
            if (CurrentTimeBudget<=0)
            {
                return;
            }
            CurrentFillRate = 0;
            for (int i = 0; i < UpdateList.Count; i++)
            {
                if (UpdateList[i].UpdateObjects.Count==0)
                {
                    continue;
                }
                float startT = (float)Timer.Elapsed.TotalSeconds;
                while ((((Timer.Elapsed.TotalSeconds-startT))/CurrentTimeBudget)<MaxSingleTypeUpdatePercentage)
                {
                    if (UpdateList[i].CurrentIndex >= UpdateList[i].UpdateObjects.Count)
                    {
                        UpdateList[i].CurrentIndex = 0;
                        break;
                    }
                    var obj = UpdateList[i].UpdateObjects[UpdateList[i].CurrentIndex];
                    obj.SUpdate(Time.time - obj.LastUpdate);
                    obj.LastUpdate = Time.time;
                    UpdateList[i].CurrentIndex++;
                    CurrentFillRate++;
                }
                if (Timer.Elapsed.TotalSeconds>CurrentTimeBudget)
                {
                    
                }
            }
            lerpedfillrate = ((int)Mathf.Lerp(lerpedfillrate, CurrentFillRate, Time.deltaTime * 5f));
        }
        UpdateType GetUpdateType(Type t)
        {
            for (int i = 0; i < UpdateList.Count; i++)
            {
                if (UpdateList[i].T==t)
                {
                    return UpdateList[i];
                }
            }
            return null;
        }
        public void RegisterObject(StaggeredUpdateBehaviour behaviour)
        {
            var t = behaviour.GetType();
            if (GetUpdateType(t)==null)
            {
                var utype = new UpdateType();
                utype.T = t;
                UpdateList.Add(utype);
            }
            GetUpdateType(t).UpdateObjects.Add(behaviour);
        }
        public void UnregisterObject(StaggeredUpdateBehaviour behaviour)
        {
            var t = behaviour.GetType();
            GetUpdateType(t).UpdateObjects.Remove(behaviour);
        }
    }
}