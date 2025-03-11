using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    Transform _bulletSpawn;
    [SerializeField]
    WeaponSO _weaponInfo;
    Stack<Proyectile> _proyectilePool = new Stack<Proyectile>();

    public void ChangeWeapon(WeaponSO newWeapon)
    {
        _weaponInfo = newWeapon;
    }
    public void CreateBullet()
    {
        if (_proyectilePool.Count > 0)
        {

        }
        else
        {
            Proyectile newProyectile = Instantiate(_weaponInfo.BulletPrefab);
            newProyectile.OriginPool = _proyectilePool;
            newProyectile.transform.position = _bulletSpawn.position;
        }
    }
    public void Fire()
    {
        
    }
}
