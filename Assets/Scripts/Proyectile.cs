using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Proyectile : MonoBehaviour
{
    Stack<Proyectile> _originPool;
    [SerializeField]
    int _damage;

    protected int Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }
    public Stack<Proyectile> OriginPool
    {
        private get { return _originPool; }
        set { _originPool = value; }
    }
    protected virtual void OnHit(Character hitedTarget)
    {

    }
    protected void Despawn()
    {
        OriginPool.Push(this);
        gameObject.SetActive(false);
    }
}
