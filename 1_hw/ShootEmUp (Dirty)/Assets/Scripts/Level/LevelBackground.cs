using System;
using LifeCycle;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour,
        ILifeCycle.ICreateListener,
        ILifeCycle.IFixedUpdateListener
    {
        [SerializeField] private Params _params;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _myTransform;


        void ILifeCycle.ICreateListener.OnCreate()
        {
            _startPositionY = _params._startPositionY;
            _endPositionY = _params._endPositionY;
            _movingSpeedY = _params._movingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(_positionX, _startPositionY, _positionZ);
            }

            _myTransform.position -= new Vector3(_positionX, _movingSpeedY * deltaTime, _positionZ);
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float _startPositionY;
            [SerializeField] public float _endPositionY;
            [SerializeField] public float _movingSpeedY;
        }
    }
}