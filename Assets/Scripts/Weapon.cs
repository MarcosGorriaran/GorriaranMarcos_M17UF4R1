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

    public WeaponSO WeaponInfo {
        get 
        {
            return _weaponInfo;
        }
        private set
        {
            _weaponInfo = value;
        }
    }

    public void ChangeWeapon(WeaponSO newWeapon)
    {
        WeaponInfo = newWeapon;
    }
    public void CreateBullet()
    {
        Proyectile newProyectile;
        if (_proyectilePool.Count > 0)
        {
            newProyectile = _proyectilePool.Pop();
            
        }
        else
        {
            newProyectile = Instantiate(WeaponInfo.BulletPrefab);
            newProyectile.OriginPool = _proyectilePool;
        }
        newProyectile.transform.position = _bulletSpawn.position;
    }
    public void Fire()
    {
        
    }
}
