using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class IACharacter : Character
{
    [SerializeField]
    StateSO _currentNode;
    [SerializeField]
    List<StateSO> _nodes;
    [SerializeField]
    TargetFinder _targetFinder;
    NavMeshAgent _agent;

    public TargetFinder TargetFinder 
    { 
        get { return _targetFinder; }
        private set { _targetFinder = value; }
    }
    public NavMeshAgent Agent
    {
        get { return _agent; }
        private set { _agent = value; }
    }
    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _targetFinder.onTargetedFound += CheckEndingConditions;
        _targetFinder.onTargetLost += CheckEndingConditions;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _targetFinder.onTargetedFound -= CheckEndingConditions;
        _targetFinder.onTargetLost -= CheckEndingConditions;
    }
    public override void Attack()
    {
        
    }
    /**
     * <summary>
     * Check each condition listed on the End Conditions variable of the ConditionSO currently being used on this component StateSO
     * and will call ExitCurrentNode method, when at least one of the conditions are met, to properly exit it and select a new one.
     * </summary>
     */
    public void CheckEndingConditions()
    {
        foreach (ConditionSO condition in _currentNode.EndConditions)
            if (condition.CheckCondition(this) == condition.answer) ExitCurrentNode();
    }
    /**
     * <summary>
     * Calling this method directly will force this component to exit its current State and loo
     * </summary>
     */
    public void ExitCurrentNode()
    {
        foreach (StateSO stateSO in _nodes)
        {
            if (stateSO.StartCondition == null)
            {
                EnterNewState(stateSO);
                break;
            }
            else
            {
                if (stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
                {
                    EnterNewState(stateSO);
                    break;
                }
            }
        }
        _currentNode.OnStateEnter(this);
    }
    /**
     * <summary>
     * Used to enter a new specified state and will call the End method of the old state and the entering state of new State
     * </summary>
     * <param name="state">
     * The new state the method will change the instance into
     * </param>
     */
    private void EnterNewState(StateSO state)
    {
        _currentNode.OnStateExit(this);
        _currentNode = state;
        _currentNode.OnStateEnter(this);
    }

    protected override void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnRevive()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnHPChange(int hpChange)
    {
        throw new System.NotImplementedException();
    }
}
