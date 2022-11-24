using MyDOTSPhysics.ECS.Components;
using Unity.Entities;

namespace MyDOTSPhysics.ECS.Aspects{
    public readonly partial struct MyCollisionAspect : IAspect{
        public readonly Entity E;

        public readonly DynamicBuffer<MyCollisionBuffer> CollisionBuffer;

    }
}