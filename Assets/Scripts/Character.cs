using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, ITargetable
{
    [SerializeField]
    Group _groupMember;
    [SerializeField]
    [InspectorName("Starting threat level")]
    float _threatLevel;
    public Group GroupMember { get => _groupMember; set => _groupMember = value; }
    public float ThreatLevel { get => _threatLevel; set => _threatLevel = value; }

    public abstract void Attack();

}
