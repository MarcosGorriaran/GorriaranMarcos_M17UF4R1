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

    /**
     * <summary>
     * This method is responsible of changing the data of the proyectile to avoid destroying proyectiles inside a pool.
     * </summary>
     */
    public void CopyProyectileData(Proyectile newProyectileInfo)
    {
        StartCoroutine(AwaitForViableChange(newProyectileInfo));
    }
    protected virtual IEnumerator AwaitForViableChange(Proyectile newProyectileInfo)
    {
        yield return new WaitUntil(() => gameObject.activeSelf); 
        Damage = newProyectileInfo.Damage;
    }
    protected void Despawn()
    {
        OriginPool.Push(this);
        gameObject.SetActive(false);
    }
}
