namespace GameLibrary
{
    interface IDataUpdater
    {
        void Add(IEntity entity);
        void Remove(IEntity entity);
        void RemoveAll();
        void RemoveAt(Entity entityType);
        IEntity[] FindEntityAt<T>() where T : IEntity;
        IEntity[] GetAllExcept(IEntity entity);
    }
}
