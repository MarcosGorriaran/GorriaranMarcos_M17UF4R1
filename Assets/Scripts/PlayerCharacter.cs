using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacter : Character, PlayerControls.IFireActions, PlayerControls.IMovementActions
{
    Rigidbody _playerBody;
    [SerializeField]
    float _speed;
    [SerializeField]
    float _accelerationForce;
    Coroutine _moveCoroutine;
    void Awake()
    {
        _playerBody = GetComponent<Rigidbody>();
    }
    public void OnFireWeapon(InputAction.CallbackContext context)
    {
        Weapon.Fire();
        Debug.Log("FiredTriggered");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ExecuteChangeOnMovement(context.ReadValue<Vector2>().normalized);
        }
        else if (context.canceled)
        {
            StopMovement();
        }
    }
    private void ExecuteChangeOnMovement(Vector2 normalizedAxis)
    {
        StopMovement();
        _moveCoroutine = StartCoroutine(ConstantMovement(normalizedAxis));
    }
    private void StopMovement()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
            _playerBody.velocity = Vector3.zero;
        }
    } 
    private IEnumerator ConstantMovement(Vector2 normalizedAxis)
    {
        while (true)
        {
            _playerBody.velocity = new Vector3(normalizedAxis.y, 0, normalizedAxis.x)*_speed;
            yield return null;
        }
    }
    protected override void OnDeath()
    {
        
    }

    protected override void OnHPChange(int hpChange)
    {
        
    }

    protected override void OnRevive()
    {
        
    }
}
