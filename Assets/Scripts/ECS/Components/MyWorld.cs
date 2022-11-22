using Unity.Entities;
using Unity.Mathematics;

namespace MyDOTSPhysics.ECS.Components{
    public struct MyWorld : IComponentData{
        public Entity BallEntityPrefab;
        public float3 SpawnPos;
        
    }
}