using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Proyectile : MonoBehaviour
{
    Stack<Proyectile> _originPool;
    [SerializeField]
    uint _damage;
    Character _owner;


    protected uint Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }
    public Stack<Proyectile> OriginPool
    {
        private get { return _originPool; }
        set { _originPool = value; }
    }
    public Character Owner
    {
        protected get { return _owner; }
        set { _owner = value; }
    }
    protected virtual void OnHit(Character hitedTarget)
    {
        hitedTarget.HitPointsManager.Hurt(Damage);
    }
    protected void Despawn()
    {
        OriginPool.Push(this);
        gameObject.SetActive(false);
    }
}
