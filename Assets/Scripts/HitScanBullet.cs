using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanBullet : BulletProyectile
{
    private void OnEnable()
    {
        Character hitedTarget = LookForTarget();
        if(hitedTarget != null)
        {
            OnHit(hitedTarget);
        }
    }
    private Character LookForTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(ray,out RaycastHit hit, Range) && hit.collider != null && hit.collider.gameObject.TryGetComponent(out Character hitChar) && hitChar != Owner)
        {
            return hitChar;
        }
        return null;
    }
}
