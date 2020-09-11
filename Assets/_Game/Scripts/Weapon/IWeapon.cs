namespace FG
{
    public interface IWeapon
    {
        void Shoot();
        string Name();
        WeaponShootingOrder ShootingOrder();
    }
}
