using System.Numerics;

namespace GameLibrary.EntityLibrary.WeaponLibrary
{
    public class WeaponComponent : Weapon
    {
        public WeaponComponent(ILoader loader,Weapons type,float rechargeTime)
            :base(loader,type,rechargeTime)
        {
            ChargeSpeed = 0;
            CountCartridge = -1;
        }
        public WeaponComponent(ILoader loader,Weapons type, float rechargeTime, float chargeSpeed,int countCartridge)
            : base(loader,type, rechargeTime)
        {
            ChargeSpeed = chargeSpeed;
            CountCartridge = countCartridge;
        }

        public override void Launch(Vector3 position,Vector3 direction)
        {
            if (CountCartridge > 0 || CountCartridge < 0)
            {
                loader.InstantiateCartridge(Cartridge, position, direction);
                SetRechargeTime(RechargeTime);
                if (CountCartridge > 0) CountCartridge--;
            }
        }
    }
}