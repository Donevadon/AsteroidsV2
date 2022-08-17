using Cartridges;
using GameLibrary;
using GameLibrary.EntityLibrary;
using GameLibrary.EntityLibrary.WeaponLibrary;
using UnityEngine;

public class CartridgeLoader : MonoBehaviour,ILoader
{
    public ICartridge GetCartridge(Weapon gun)
    {
        switch (gun.Type)
        {
            case Weapons.BulletWearon:
                return new Bullet();
            case Weapons.LazerWeapon:
                return new Lazer();
            default:
                throw new System.Exception("Неверно указан тип");
        }
    }

    public void InstantiateCartridge(ICartridge cartridge, System.Numerics.Vector3 position, System.Numerics.Vector3 direction)
    {
        GameSystem.GetInstance().Spawner.SpawnEntity(cartridge.Type,position,direction,null);
    }
}
