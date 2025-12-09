using System;
using Abstractions;
using UnityEngine;

namespace Infrastructure
{
    public class PlayerOneInput : IInputService, IDisposable
    {
        private readonly Actions _input = new();
        
        public PlayerOneInput ()
        {
            _input.Player.Enable();
        }

        public float GetHorizontalMovement()
        {
            return _input.Player.Move.ReadValue<Vector2>().x;
        }

        public bool IsActionPressed()
        {
            return _input.Player.Interact.triggered;
        }

        public void Dispose()
        {
            _input.Player.Disable();
            _input?.Dispose();
        }
    }
}