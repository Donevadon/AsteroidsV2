using System;
using System.Numerics;

namespace GameLibrary.EntityLibrary
{
    public class ShipControl
    {
        public ObjectMovement Movement { get; }
        public ObjectRotation Rotation { get; }
        public WeaponManager GunManager { get; }
        public ShipControl(Vector3 moveDirection,ref Action Object_Move,Vector3 rotateDirection,ref Action Object_Rotate)
        {
            Movement = new ObjectMovement(moveDirection,
                ref Object_Move);
            Rotation = new ObjectRotation(rotateDirection,
                ref Object_Rotate);
            GunManager = new WeaponManager((x) => Movement.Position_Updated += x,
                (y) => Rotation.Euler_Updated += y);
        }
        public ShipControl(Vector3 moveDirection, ref Action Object_Move,Vector3 rotateDirection, ref Action<Vector3,bool> Object_Rotate)
        {
            Movement = new ObjectMovement(moveDirection, 
                ref Object_Move);
            Rotation = new ObjectRotation(rotateDirection, 
                ref Object_Rotate,
                (x) => Movement.Position_Updated += x,
                (x) => Movement.Direction_Updated += x);
        }

        public void GunsRechargeAndAddCartridge(float Time)
        {
            foreach(var gun in GunManager.ShipGuns)
            {
                gun.Recharge(Time);
                gun.AddCartridge();
            }
        }
    }
}
