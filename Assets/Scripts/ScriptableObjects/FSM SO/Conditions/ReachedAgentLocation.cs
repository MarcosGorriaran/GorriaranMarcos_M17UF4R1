using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu(fileName = "ReachedAgentLocation", menuName = "ScriptableObjects/Conditions/ReachedAgentLocation")]
public class ReachedAgentLocation : ConditionSO
{
    protected override bool ProcessCheck(IACharacter ec)
    {
        float dist = ec.Agent.remainingDistance;
        return (dist != Mathf.Infinity && ec.Agent.pathStatus == NavMeshPathStatus.PathComplete && ec.Agent.remainingDistance == 0);
    }
}
