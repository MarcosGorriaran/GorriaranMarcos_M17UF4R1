using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]  
public class TravelBullet : BulletProyectile
{
    float _lifeTime;
    [SerializeField]
    float _speed;
    Rigidbody _bulletBody;
    Coroutine _bulletLifeCountdown;
    private void OnEnable()
    {
        _lifeTime = Range / _speed;
        _bulletBody.velocity = transform.forward * _speed;
        _bulletLifeCountdown = StartCoroutine(BulletCountdown(_lifeTime));
    }
    private void OnDisable()
    {
        if(_bulletLifeCountdown != null)
        {
            StopCoroutine(_bulletLifeCountdown);
        }
    }
    private IEnumerator BulletCountdown(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Despawn();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Character targetHit) && Owner != targetHit)
        {
            OnHit(targetHit);
            Despawn();
        }
    }
}
