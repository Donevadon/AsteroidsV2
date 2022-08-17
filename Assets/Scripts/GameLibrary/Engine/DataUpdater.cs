using System;
using System.Collections.Generic;
using System.Threading;

namespace GameLibrary.Engine
{
    class DataUpdater : IDataUpdater
    {
        private readonly int CycleInterval = 20;
        private static List<IEntity> gameEntities;
        private static DataUpdater dataUpdater;
        private ICollider2D Collider = new Collider();
        private Thread UpdateThread;
        private Thread CollisionThread;

        private DataUpdater(ref Action program_Closed)
        {
            UpdateThread = new Thread(Update);
            CollisionThread = new Thread(Collision);
            gameEntities = new List<IEntity>();
            program_Closed += Stop;
            Start();
        }

        public static DataUpdater GetInstance(ref Action program_Closed)
        {
            if (dataUpdater is null) dataUpdater = new DataUpdater(ref program_Closed);
            return dataUpdater;
        }
        public static DataUpdater GetInitialInstance()
        {
            return dataUpdater;
        }

        public void Add(IEntity entity)
        {
            gameEntities.Add(entity);
        }

        public void Remove(IEntity entity)
        {
            gameEntities.Remove(entity);
        }

        private void Start()
        {
            UpdateThread.Start();
            CollisionThread.Start();
        }

        private void Stop()
        {
            UpdateThread.Abort();
            CollisionThread.Abort();
        }

        private void Update()
        {
            while (true)
            {
                    for (int i = 0; i < gameEntities.Count; i++)
                    {
                        try
                        {
                            gameEntities[i].UpdateData();
                        }
                        catch 
                        {
                            
                        }
                    }
                Thread.Sleep(CycleInterval);
            }
        }

        private void Collision()
        {
            while (true)
            {
                for (int i = 0; i < gameEntities.Count; i++)
                {
                    try
                    {
                        Collider.CheckCollider(gameEntities[i], GetAllExcept(gameEntities[i]));
                    }
                    catch 
                    {
                        
                    }
                }
                Thread.Sleep(CycleInterval);
            }

        }

        public void RemoveAll()
        {
            gameEntities.RemoveAll((x) => true);
        }

        public IEntity[] GetAllExcept(IEntity entity)
        {
            return gameEntities.FindAll((x) => x != entity).ToArray();
        }

        public void RemoveAt(Entity entityType)
        {
            gameEntities.RemoveAll((x) => x.Type == entityType);
        }

        public IEntity[] FindEntityAt<T>() where T : IEntity
        {
            return gameEntities.FindAll((x) => x is T).ToArray();
        }
    }
}
