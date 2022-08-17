using System;
using System.Numerics;

namespace GameLibrary.EntityLibrary.WeaponLibrary
{
    /// <summary>
    /// Оружие устанавливаемое с помощью GunManager
    /// </summary>
    public abstract class Weapon
    {
        private float rechargeTime;
        private ICartridge cartridge;
        protected ILoader loader;
        protected float timeToRecharge;

        public bool isRecharged => rechargeTime > 0;
        /// <summary>
        /// Скорость заполнения прогресса создания снаряда
        /// </summary>
        protected float ChargeSpeed { get; set; }
        /// <summary>
        /// Прогресс создания нового снаряда от 0 до 100
        /// </summary>
        protected float Charge { get; set; }
        /// <summary>
        /// Возвращает оставшееся времени перезарядки
        /// </summary>
        public float RechargeTime { get; private set; }
        /// <summary>
        /// Записывает и возвращает количество доступных снарядов
        /// </summary>
        public int CountCartridge { get; protected set; }
        public Weapons Type { get; }
        /// <summary>
        /// Получить используемый снаряд
        /// </summary>
        protected ICartridge Cartridge 
        { 
            get
            {
                if (cartridge is null) cartridge = loader.GetCartridge(this);
                return cartridge;
            } 
        }

        /// <summary>
        /// Событие обновления данных о количестве снарядов
        /// </summary>
        public virtual event Action<int,Weapons> CountCartridge_Updated;

        public Weapon(ILoader loader,Weapons type,float rechargeTime)
        {
            Type = type;
            RechargeTime = rechargeTime;
            this.loader = loader;
        }

        /// <summary>
        /// установить время перезарядки после выстрела
        /// </summary>
        /// <param name="rechargeTime"></param>
        protected void SetRechargeTime(float rechargeTime)
        {
            this.rechargeTime = rechargeTime;
        }
        /// <summary>
        /// Перезарядить оружие
        /// </summary>
        /// <param name="time"></param>
        public void Recharge(float time)
        {
            if (rechargeTime > 0) rechargeTime -= time; 
        }
        /// <summary>
        /// Добавить снаряд за определённое количество времени
        /// </summary>
        public virtual void AddCartridge() 
        {
            if (ChargeSpeed > 0) 
            {
                if (Charge >= 100)
                {
                    CountCartridge++;
                    CountCartridge_Updated?.Invoke(CountCartridge, Type);
                    Charge = 0;
                }
                else if (Charge >= 0) Charge += ChargeSpeed;
            }
        }
        /// <summary>
        /// Запустить снаряд
        /// </summary>
        /// <returns></returns>
        public abstract void Launch(Vector3 position, Vector3 direction);

    }
}