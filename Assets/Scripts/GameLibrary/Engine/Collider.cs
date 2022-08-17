using GameLibrary.EntityLibrary;
using System.Collections.Generic;
using System.Numerics;

namespace GameLibrary.Engine
{
    class Collider : ICollider2D
    {
        public void CheckCollider(IEntity entity,params IEntity[] entities)
        {
            foreach (var foundEntity in entities)
            {
                if (CompareEntities(entity, foundEntity))
                {
                    entity.OnCollision(foundEntity);
                }
            }
        }

        public void CheckRaycast(Vector3[] points,float size, List<IEntity> entities)
        {
            foreach (var point in points)
            {
                for(int i = 0; i < entities.Count; i++)
                    if (CompareEntities(point, size, entities[i]))
                    {
                        switch (entities[i]) 
                        {
                            case IGameEntity gameEntity when gameEntity.Type != Entity.Player:
                                gameEntity.Dead();
                                break;
                        }
                        entities.Remove(entities[i]);
                    }
            }
        }


        private bool CompareEntities(IEntity entity0, IEntity entity1)
        {
            return entity0.Position.Distance(entity1.Position) <= entity0.RadiusCollider + entity1.RadiusCollider;
        }

        private bool CompareEntities(Vector3 positionPoint,float pointSize, IEntity entity)
        {
            return positionPoint.Distance(entity.Position) <= pointSize + entity.RadiusCollider;
        }

    }
}
