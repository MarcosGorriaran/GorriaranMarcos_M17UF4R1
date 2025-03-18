using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class BulletProyectile : Proyectile
{
    [SerializeField]
    float _range;

    protected float Range
    {
        get { return _range; }
        private set { _range = value; }
    }


}
