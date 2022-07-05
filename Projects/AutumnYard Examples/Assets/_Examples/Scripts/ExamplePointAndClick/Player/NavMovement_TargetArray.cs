using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

namespace AutumnYard.ExamplePointAndClick
{
    public sealed class NavMovement_TargetArray : MonoBehaviour
    {
        [SerializeField] private Transform[] goals;
        private int _currentGoal;
        private NavMeshAgent _agent;

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        [Button("Move to next goal")]
        [ContextMenu("Move to next goal")]
        private void MoveToNextGoal()
        {
            _agent.destination = goals[_currentGoal].position;
            _currentGoal++;
            if (_currentGoal >= goals.Length)
                _currentGoal = 0;
        }
    }
}
