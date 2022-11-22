using System.Collections;
using System.Collections.Generic;
using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace MyDOTSPhysics
{
    public class AttractorAuthoring : MonoBehaviour{
        
    }

    public class AttractorBaker : Baker<AttractorAuthoring>{
        public override void Bake(AttractorAuthoring authoring){
            AddComponent<Attractor>();
        }
    }
}
