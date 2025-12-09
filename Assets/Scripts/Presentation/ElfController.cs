using System;
using Abstractions;
using UnityEngine;

namespace Presentation
{
    public class ElfController : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float moveSpeed = 10f;

        [Header("Cane mechanics")] [SerializeField]
        private Transform canePivot;

        [SerializeField] private float maxTiltAngle = 30f;
        [SerializeField] private float tiltSpeed = 15f;

        private IInputService _inputService;

        public void Configure(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService == null) return;

            var move = _inputService.GetHorizontalMovement();
            transform.Translate(Vector3.right * (move * moveSpeed * Time.deltaTime));

            HandleCaneRotation(move);
        }

        private void HandleCaneRotation(float inputDirection)
        {
            var targetAngle = 0f;

            if (inputDirection > 0.1f)
            {
                targetAngle = -maxTiltAngle;
            }
            else if (inputDirection < -0.1f)
            {
                targetAngle = maxTiltAngle;
            }
            else
            {
                targetAngle = 0f;
            }

            var targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
            canePivot.localRotation = Quaternion.Lerp(
                canePivot.localRotation,
                targetRotation,
                Time.deltaTime * tiltSpeed
            );
        }
    }
}