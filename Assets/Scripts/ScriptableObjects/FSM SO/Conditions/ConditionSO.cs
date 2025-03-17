using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ConditionSO : ScriptableObject
{
    public abstract bool CheckCondition(IACharacter ec);
    public bool answer;

}
