using System;
using UnityEngine;

namespace PaccoPaccotini
{
    [Serializable]
    public class Wheel
    {
        public Transform WheelPosition;
        public float SpringStrength = 50f;
        public float SpringDamping = 10f;
        
        private float _springOffset = 0f;
        
    }
}