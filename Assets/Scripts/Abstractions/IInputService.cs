using UnityEngine;

namespace Abstractions
{
    public interface IInputService
    {
        float GetHorizontalMovement();
        bool IsActionPressed();
    }
}