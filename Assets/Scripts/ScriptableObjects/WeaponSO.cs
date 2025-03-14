using UnityEngine;

public class WeaponSO : ScriptableObject
{
    [SerializeField]
    float _rateOfFire;
    [SerializeField]
    Proyectile _bulletPrefab;
    public float RateOfFire
    {
        get { return _rateOfFire; }
        private set { _rateOfFire = value; }
    }
    public Proyectile BulletPrefab
    {
        get { return _bulletPrefab; }
        private set { _bulletPrefab = value; }
    }

}
