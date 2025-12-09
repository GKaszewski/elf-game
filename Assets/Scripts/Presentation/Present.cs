using System;
using Core;
using UnityEngine;

namespace Presentation
{
    public class Present : MonoBehaviour
    {
        [SerializeField] private int points = 1;
        
        private PresentSpawner _spawner;
        
        public void Configure(PresentSpawner spawner)
        {
            _spawner = spawner;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                GameEvents.ReportPresentDropped();
                _spawner.ReturnToPool(this);
            }

            if (other.gameObject.CompareTag("Sleigh"))
            {
                GameEvents.ReportPresentCaught(points);
                _spawner.ReturnToPool(this);
            }
        }
    }
}