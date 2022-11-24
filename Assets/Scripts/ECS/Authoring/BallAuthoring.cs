using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace MyDOTSPhysics{
    public class BallAuthoring : MonoBehaviour{
        public float Speed;
    }


    public class BallBaker : Baker<BallAuthoring>{
        public override void Bake(BallAuthoring authoring){
            AddComponent(new Ball{
                Speed = authoring.Speed
            });
            AddBuffer<MyCollisionBuffer>();
        }
    }
}