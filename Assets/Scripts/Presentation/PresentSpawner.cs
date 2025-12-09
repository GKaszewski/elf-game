using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Presentation
{
    public class PresentSpawner : MonoBehaviour
    {
        [Header("Pooling")]
        [SerializeField] private Present presentPrefab;
        [SerializeField] private int poolSize = 20;
        
        [Header("Spawning")]
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 1.5f;
        [SerializeField] private float burstChance = 0.3f;
        [SerializeField] private Vector2 initialDirection = Vector2.right;
        
        private Queue<Present> _pool = new();
        private List<Present> _activePresents = new();
        private bool _isSpawning;

        private void Awake()
        {
            for (var i = 0; i < poolSize; i++)
            {
                CreateNewPresent();
            }
        }

        private Present CreateNewPresent()
        {
            var p = Instantiate(presentPrefab, transform);
            p.gameObject.SetActive(false);
            p.Configure(this);
            
            _pool.Enqueue(p);
            return p;
        }

        private Present GetPresent()
        {
            if (_pool.Count == 0) CreateNewPresent();
            
            var p = _pool.Dequeue();
            p.gameObject.SetActive(true);
            _activePresents.Add(p);
            return p;
        }

        public void ReturnToPool(Present p)
        {
            if (!p.gameObject.activeSelf) return;
            
            p.gameObject.SetActive(false);
            _activePresents.Remove(p);
            _pool.Enqueue(p);
        }

        public void StartSpawning()
        {
            _isSpawning = true;
            GameEvents.GameOver += StopSpawning;
            StartCoroutine(SpawnRoutine());
        }

        public void StopSpawning()
        {
            _isSpawning = false;
            GameEvents.GameOver -= StopSpawning;
            StopAllCoroutines();
            
            
            foreach (var p in _activePresents.ToArray()) ReturnToPool(p);
            _activePresents.Clear();
        }

        private IEnumerator SpawnRoutine()
        {
            while (_isSpawning)
            {
                SpawnOne();

                if (Random.value < burstChance)
                {
                    yield return new WaitForSeconds(0.2f);
                    SpawnOne();
                }

                //TODO: Difficulty scaling
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnOne()
        {
            var p = GetPresent();
            
            var rb = p.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            
            var index = Random.Range(0, spawnPoints.Length);
            p.transform.position = spawnPoints[index].position;
            p.transform.rotation = Quaternion.identity;
            rb.AddForce(initialDirection + new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)), ForceMode2D.Impulse);
        }
    }
}