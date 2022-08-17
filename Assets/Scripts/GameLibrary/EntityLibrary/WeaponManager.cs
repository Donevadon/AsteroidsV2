using GameLibrary.EntityLibrary.WeaponLibrary;
using System;
using System.Dynamic;
using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    public class WeaponManager
    {
        private Weapon selectGun;
        public Weapon[] ShipGuns { get; set; }
        private Vector3 Position { get; set; }
        private Vector3 Direction { get; set; }

        public WeaponManager(Action<Action<Vector3>> UpdatePosition_Delegate,Action<Action<Vector3>> UpdateDirection_Delegate)
        {
            UpdatePosition_Delegate(UpdatePosition);
            UpdateDirection_Delegate(UpdateDirection);
        }
        /// <summary>
        /// Выстрелить из указанного оружия в указанном направлении
        /// </summary>
        /// <param name="gun"></param>
        /// <param name="direction"></param>
        public void Shoot(Weapons gun)
        {
            if (selectGun?.Type != gun) selectGun = FindGun(gun);
            if (selectGun?.isRecharged == false)
            {
                selectGun.Launch(Position,Direction);
            }
        }
        private void UpdatePosition(Vector3 position)
        {
            Position = position;
        }
        private void UpdateDirection(Vector3 direction) 
        {
            Direction = direction;
        }
        private Weapon FindGun(Weapons weapon)
        {
            foreach(var gun in ShipGuns)
            {
                if (gun.Type == weapon) return gun;
            }
            throw new System.Exception("Данное оружие отсутствует");
        }
    }
}
