using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class TargetFinder : MonoBehaviour
{
    [SerializeField]
    public Group[] targetGroups;
    [SerializeField]
    public bool seeTroughObstacles;
    private bool targetFound;
    private Collider _foundTarget;
    public delegate void TargetFoundAction();
    public event TargetFoundAction onTargetedFound;
    public delegate void TargetChangeAction();
    public event TargetChangeAction onTargetedChange;
    public delegate void TargetLostAction();
    public event TargetLostAction onTargetLost;

    public Collider FoundTarget { get { return _foundTarget; } private set { _foundTarget = value; } }

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

            if (elementsFound.Count() > 0)
            {
                Debug.Log(gameObject.name + " \"TargetFound\"");
                Collider actualFoundTarget = SelectHighestThreat(elementsFound);
                targetFound = true;
                if(actualFoundTarget != FoundTarget)
                {
                    onTargetedChange?.Invoke();
                }
                FoundTarget = actualFoundTarget;
                if (!targetFound)
                {
                    onTargetedFound?.Invoke();
                }
            }
            else if (elementsFound.Count() <= 0 && targetFound)
            {
                Debug.Log(gameObject.name + " \"TargetLost\"");
                onTargetLost?.Invoke();
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
            result = targetGroups.Contains(targetable.GroupMember);
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
    //I want the class children to represent the area its looking for targets.
    protected abstract void OnDrawGizmos();
}
