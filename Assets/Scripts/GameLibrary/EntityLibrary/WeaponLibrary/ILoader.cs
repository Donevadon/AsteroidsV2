using System.Numerics;

namespace GameLibrary.EntityLibrary.WeaponLibrary
{
    /// <summary>
    /// Загрузчик снарядов в орудие
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Получить снаряд к определённому орудию
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        ICartridge GetCartridge(Weapon gun);
        /// <summary>
        /// Инстанциировать объект снаряда
        /// </summary>
        /// <param name="cartridge"></param>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        void InstantiateCartridge(ICartridge cartridge,Vector3 position,Vector3 direction);
    }
}
