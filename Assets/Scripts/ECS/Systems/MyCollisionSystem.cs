using MyDOTSPhysics.ECS.Aspects;
using MyDOTSPhysics.ECS.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace MyDOTSPhysics.ECS.Systems{
    
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateBefore(typeof(PhysicsSystemGroup))] // we are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
    public partial struct MyCollisionSystem : ISystem{
        
        private BufferLookup<MyCollisionBuffer> MyCollisionBufferFromEntity;

        public void OnCreate(ref SystemState state){
            MyCollisionBufferFromEntity = state.GetBufferLookup<MyCollisionBuffer>();
        }

        public void OnDestroy(ref SystemState state){

        }

        public void OnUpdate(ref SystemState state){
            
            MyCollisionBufferFromEntity.Update(ref state);
            
            SimulationSingleton SimSingleton = SystemAPI.GetSingleton<SimulationSingleton>();

            foreach (var Entry in SystemAPI.Query<DynamicBuffer<MyCollisionBuffer>>()){
                Entry.Clear();
            }
            
            JobHandle CHandle = new MyCollisionSystemJob{
                Collisions = MyCollisionBufferFromEntity
            }.Schedule(SimSingleton, state.Dependency);
            
            JobHandle THandle = new MyTriggerEventsJob(){ }.Schedule(SimSingleton, state.Dependency);
            
            CHandle.Complete();
            THandle.Complete();

            EntityCommandBuffer ECB = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            
            foreach (var Entry in SystemAPI.Query<MyCollisionAspect>()){
                if (!Entry.CollisionBuffer.IsEmpty){
                    ECB.DestroyEntity(Entry.E);
                }
            }
            
        }
        
        private struct MyCollisionSystemJob : ICollisionEventsJob{

            public BufferLookup<MyCollisionBuffer> Collisions;

            public void Execute(CollisionEvent collisionEvent){


                if (Collisions.HasBuffer(collisionEvent.EntityA)){
                    DynamicBuffer<MyCollisionBuffer> BufferA = Collisions[collisionEvent.EntityA];
                    BufferA.Add(new MyCollisionBuffer{ E = collisionEvent.EntityB});      
                }

                if (Collisions.HasBuffer(collisionEvent.EntityB)){
                    DynamicBuffer<MyCollisionBuffer> BufferB = Collisions[collisionEvent.EntityB];
                    BufferB.Add(new MyCollisionBuffer{ E = collisionEvent.EntityA});
                }

                Debug.LogWarning($"Collision: {collisionEvent.EntityA} with {collisionEvent.EntityB}");
            }
            
        }

        private struct MyTriggerEventsJob : ITriggerEventsJob{
            public void Execute(TriggerEvent triggerEvent){
                Debug.LogWarning($"Trigger: {triggerEvent.EntityA} with {triggerEvent.EntityB}");
            }
        }



    }
}