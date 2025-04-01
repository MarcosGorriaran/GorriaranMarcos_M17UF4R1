using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Alert", menuName = "ScriptableObjects/State/Alert")]
public class AlertState : StateSO
{
    public override void OnStateEnter(IACharacter ec)
    {
        Vector3 lastLoc = ec.TargetFinder.LastKnownLocation.Value;
        ec.Agent.SetDestination(lastLoc);
    }

    public override void OnStateExit(IACharacter ec)
    {
    }

    public override void OnStateUpdate(IACharacter ec)
    {
        float dist = ec.Agent.remainingDistance;
        if (dist != Mathf.Infinity && ec.Agent.pathStatus == NavMeshPathStatus.PathComplete && ec.Agent.remainingDistance == 0)
        {
            ec.TargetFinder.ClearLastKnownLocation();
            ec.CheckEndingConditions();
        }
    }
}
