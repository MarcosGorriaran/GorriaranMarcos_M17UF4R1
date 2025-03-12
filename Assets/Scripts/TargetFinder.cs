using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    [SerializeField]
    public Group targetGroup;
    [SerializeField]
    public bool seeTroughObstacles;
    private bool targetFound;
    public delegate void TargetFoundAction(Collider objectFound);
    public event TargetFoundAction onTargetedFound;
    public delegate void TargetLostAction();
    public event TargetLostAction onTargetLost;


    void FixedUpdate()
    {
        SearchTargets();
    }
    private void SearchTargets()
    {
        Collider[] elementsFound = LookTargetMethod();
        elementsFound = elementsFound.Where(IsTargatable).ToArray();
        try
        {

            if (elementsFound.Count() > 0 && !targetFound)
            {
                Debug.Log(gameObject.name + " \"TargetFound\"");
                onTargetedFound.Invoke(SelectHighestThreat(elementsFound));
                targetFound = true;
            }
            else if (elementsFound.Count() <= 0 && targetFound)
            {
                Debug.Log(gameObject.name + " \"TargetLost\"");
                onTargetLost.Invoke();
                targetFound = false;
            }
        }
        catch (NullReferenceException)
        {

        }
    }
    protected abstract Collider[] LookTargetMethod();
    private bool IsTargatable(Collider element)
    {
        bool result = element.TryGetComponent(out ITargetable targetable);
        if (result)
        {
            result = targetable.GroupMember == targetGroup;
            if(result && !seeTroughObstacles)
            {
                result = LineOfShightCheck(element);
            }
        }
        return result;
    }
    private bool LineOfShightCheck(Collider element)
    {
        Vector2 distanceVector = element.transform.position - transform.position;
        float distance = Vector2.Distance(element.transform.position, transform.position);
        Vector2 direction = distanceVector.normalized;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, distance);
        return hit.Where((obj => !obj.transform.TryGetComponent<ITargetable>(out _))).Count() == 0;
    }
    private Collider SelectHighestThreat(Collider firstElement, Collider secondElement)
    {
        return firstElement.GetComponent<ITargetable>().ThreatLevel > secondElement.GetComponent<ITargetable>().ThreatLevel ? firstElement : secondElement;
    }
    private Collider SelectHighestThreat(Collider[] elements)
    {
        return elements.Aggregate(SelectHighestThreat);
    }
    protected abstract void OnDrawGizmos();
}
