using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace AutumnYard.ExamplePointAndClick
{
    public class NavMovement_WithMouse : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Camera _cam;

        void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _cam = Camera.main;
        }

        void Update()
        {
#if UNITY_ANDROID
            if (Touch.activeTouches.Count > 0)
            {
                var asd = Touch.activeTouches[0];
                if (Physics.Raycast(_cam.ScreenPointToRay(asd.screenPosition), out RaycastHit hit, 100))
                {
                    _agent.destination = hit.point;
                }
            }
#endif
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (Physics.Raycast(_cam.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, 100))
                {
                    _agent.destination = hit.point;
                }
            }

        }
    }
}
