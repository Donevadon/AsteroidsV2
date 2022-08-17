using GameLibrary.Engine;
using System;
using System.Numerics;
using System.Threading;
using Cartridges;

namespace GameLibrary
{
    public class GameSystem
    {
        private static GameSystem gameSystem;

        public static SynchronizationContext Context { get; private set; }
        public ISpawnEntity Spawner { get; private set; }
        public int Score { get; private set; }

        private IDataUpdater DataUpdateSystem { get; set; }

        private event Action Program_Closed;

        private GameSystem()
        {
            Context = SynchronizationContext.Current;
            DataUpdateSystem = DataUpdater.GetInstance(ref Program_Closed);
            Spawner = new CreatorEntity(DataUpdateSystem);
        }
        /// <summary>
        /// Выпустить луч и уничтожить все пересечённые вражеские объекты
        /// </summary>
        /// <param name="callingEntity"></param>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="size"></param>

        public static void Raycast(IEntity callingEntity, Vector3 position, Vector3 direction, float distance, float size)
        {
            Ray.Raycast(callingEntity,position,direction,distance,size);
        }
        public static GameSystem GetInstance()
        {
            if (gameSystem is null) gameSystem = new GameSystem();
            return gameSystem;
        }
        public void AddScore(int score)
        {
            Score += score;
        }
        public void ResetScore()
        {
            Score = 0;
        }
        public void InvokeCloseProgram()
        {
            Program_Closed();
        }
        public void ReloadEntity(Action<IEntity> SpawnEntity)
        {
            IEntity[] cartridges = DataUpdateSystem.FindEntityAt<Cartridge>();
            foreach (var cartridge in cartridges)
            {
                try
                {
                    cartridge.Destroy();
                }
                catch
                {
                    
                }
            }
            IEntity[] entities = DataUpdateSystem.FindEntityAt<GameEntity>();
            DataUpdateSystem.RemoveAll();
            foreach (var entity in entities)
            {
                try
                {
                    entity.Destroy();
                    SpawnEntity(entity);
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// Перезапуск системы и выполнение пользовательских инструкций перезапуска уровня
        /// </summary>
        /// <param name="restart"></param>
        public void Restart(Action restart)
        {
            DataUpdateSystem.RemoveAll();
            ResetScore();
            restart();
        }

    }
}
