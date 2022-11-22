using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

namespace MyDOTSPhysics.ECS.Systems{
    public partial struct SpawnBallSystem : ISystem{
        
        public void OnCreate(ref SystemState state){
            state.RequireForUpdate<MyWorld>();
        }

        public void OnDestroy(ref SystemState state){
        }

        public void OnUpdate(ref SystemState state){

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space)){
                MyWorld MWComp = SystemAPI.GetSingleton<MyWorld>();

                EntityCommandBuffer ECB = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
                Entity NewEntity = ECB.Instantiate(MWComp.BallEntityPrefab);


                UniformScaleTransform New = new UniformScaleTransform{
                    Position = MWComp.SpawnPos,
                    Rotation = quaternion.identity,
                    Scale = 1.0f
                };
                
                ECB.SetComponent(NewEntity, new Translation(){
                    Value = MWComp.SpawnPos
                });
                
                ECB.SetComponent(NewEntity, new PhysicsVelocity{
                    Linear = new float3(3, 3, 3)
                });
                
            }



        }
    }
}