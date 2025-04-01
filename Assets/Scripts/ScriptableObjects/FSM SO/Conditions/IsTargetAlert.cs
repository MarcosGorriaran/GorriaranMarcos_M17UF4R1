using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IsTargetAlert", menuName = "ScriptableObjects/Conditions/IsTargetAlert")]
public class IsTargetAlert : ConditionSO
{
    protected override bool ProcessCheck(IACharacter ec)
    {
        return ec.TargetFinder.FoundTarget == null && ec.TargetFinder.LastKnownLocation != null;
    }
}
