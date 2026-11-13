using System;
using UnityEngine;

namespace PaccoPaccotini
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Wheel[] _wheels;
        
        private Rigidbody _rb;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            foreach (var wheel in _wheels)
            {
                Gizmos.DrawSphere(wheel.WheelPosition.position, 0.2f);
                Gizmos.DrawRay(wheel.WheelPosition.position , wheel.WheelPosition.up);
            }
        }
    }
}