using System.Collections;
using System.Collections.Generic;
using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using UnityEngine;

namespace MyDOTSPhysics
{
    public class MyWorldAuthoring : MonoBehaviour{
        public GameObject BallPrefab;
        public Transform SpawnBallsTransform;
    }


    public class MyWorldBaker : Baker<MyWorldAuthoring>{
        public override void Bake(MyWorldAuthoring authoring){
            AddComponent(new MyWorld{
                BallEntityPrefab = GetEntity(authoring.BallPrefab),
                SpawnPos = authoring.SpawnBallsTransform.position
            });
        }
    }
}
