namespace FG
{
    public interface IWeapon
    {
        void Enable();
        void Disable();
        bool CheckEnable();
        string Name();
        WeaponShootingOrder ShootingOrder();
        void Shoot();
    }
}
