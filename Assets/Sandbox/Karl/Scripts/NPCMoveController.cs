using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * NPC Move Controller:
 *  Author: Karl Truong
 * 
 * - Handles primary NPC movement.
 * - Originally from agentcontroller.cs script
 * 
 * TODO:
 * - Fix rotation bug when NPC is in talking state.
 */

public class NPCMoveController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _npcAgent;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Vector3 _targetDest;
    [SerializeField] private int _walkRadius = 10;
    [SerializeField] private float _npcSpeed = 5f;
    [SerializeField] private float _startAnimTime = 0.3f;
    [SerializeField] private float _stopAnimTime = 0.15f;
    [SerializeField] private float _allowAgentRotation = 0.1f;
    [SerializeField] private float _deltaVariance = 0.5f;
    [SerializeField] private bool _hasStopped;
    
    private Animator _animator;
    private int _stopCount;
    private float _currSpeed;

    public boundingstuff BoundArea;

    private void Start()
    {
        AgentInitializer();
        AgentSetters();
        Continue();
    }

    private void FixedUpdate()
    {
        if (IsInTalkingState())
        {
            Stop();                     // Call dialogue method/class for Agent from here
            transform.LookAt(_playerTransform);
        }
        else if (_npcAgent != null)
            CheckAgentMovement();

        SwitchTargetDestination();
    }

    private void CheckAgentMovement()
    {
        if (_currSpeed > _allowAgentRotation)
        {
            _animator.SetFloat("Blend", _currSpeed, _startAnimTime, Time.deltaTime);
            UpdateAgentDest();
        }
    }

    private void SwitchTargetDestination()
    {
        if (HasReachedDestination())
        {
            _animator.SetFloat("Blend", 0, _stopAnimTime, Time.deltaTime);
            _npcAgent.SetDestination(transform.position);
            _hasStopped = true;
            _stopCount++;
        }
    }

    private void UpdateAgentDest()
    {
        if (_hasStopped)
        {
            BoundArea.Update(transform, transform.position);
            _stopCount++;
            _stopCount = (_stopCount > 500) ? 0 : _stopCount;

            if (_stopCount == 0) Revert();
        }
        else if (_npcAgent.nextPosition != null)
            BoundArea.Update(transform, _npcAgent.nextPosition);
        else
            BoundArea.Update(transform, transform.position);
    }

    private void AgentInitializer()
    {
        _npcAgent = GetComponent<NavMeshAgent>();
        _npcAgent.speed = _npcSpeed;
        _currSpeed = _npcAgent.speed;
        _npcAgent.SetDestination(_targetDest);
    }

    private void AgentSetters()
    {
        BoundArea = new boundingstuff(_npcAgent.speed, 0.5f, 0.3f, transform, _npcAgent.nextPosition);
        _animator = GetComponent<Animator>();
    }

    private void Continue()
    {
        BoundArea.front = _npcAgent.speed;
        _currSpeed = _npcAgent.speed;
        _hasStopped = false;
    }

    private void Stop()
    {
        _hasStopped = true;
    }

    private void Revert()
    {
        Vector3 newDest = Initializer.getRandomDestination(_targetDest, _walkRadius);
        _npcAgent.SetDestination(newDest);
        _targetDest = newDest;
        _hasStopped = false;
        _animator.SetFloat("Blend", 0, _stopAnimTime, Time.deltaTime);
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(_npcAgent.destination, transform.position) <= _deltaVariance || _hasStopped;
    }

    private bool IsInTalkingState()
    {
        return GameManager.Instance.CurrentState == GameManager.GameState.TALKING;
    }

    public void PlayTalkingAnim()
    {
        _animator.SetBool("IsTalking", true);
    }

    public void StopTalkingAnim()
    {
        _animator.SetBool("IsTalking", false);
    }
}
