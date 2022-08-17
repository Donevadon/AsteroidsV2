using GameLibrary;
using UnityEngine;

class Factory : MonoBehaviour,IEntityFactory
{
    IEntity IEntityFactory.GetEntity(Entity entity)
    {
        string path = $"Prefabs/Entity/{(Visualization.is3D ? "3D" : "2D")}/";
        return Instantiate(Resources.Load<GameObject>(path + entity.ToString())).GetComponent<IEntity>();
    }
}
