using System;
using System.Collections.Generic;

namespace StaggeredUpdate
{
    public class UpdateType
    {
        public Type T;
        public float AvgUpdateTime;
        public int CurrentIndex;
        public List<StaggeredUpdateBehaviour> UpdateObjects = new List<StaggeredUpdateBehaviour>();
    }
}