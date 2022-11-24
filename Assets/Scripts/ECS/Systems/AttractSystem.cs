using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace MyDOTSPhysics.ECS.Systems{
    public partial class AttractSystem : SystemBase{

        protected override void OnUpdate(){

            Entity A = GetSingletonEntity<Attractor>(); 
            
            float DeltaTime = SystemAPI.Time.DeltaTime;

            Translation ACenter = GetComponent<Translation>(A);
            
            foreach ((RefRW<PhysicsVelocity> PVel, RefRO<Translation> Trans, RefRO<Ball> Ball) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRO<Translation>, RefRO<Ball>>()){
                float3 Dir = math.normalize(ACenter.Value - Trans.ValueRO.Value);
                PVel.ValueRW.Linear = Dir * DeltaTime * Ball.ValueRO.Speed;
            }
        }
    }
}