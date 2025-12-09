using System;
using Abstractions;
using Core;
using Core.Systems;
using Infrastructure;
using UnityEngine;

namespace Presentation
{
    public class GameBootstrap : MonoBehaviour
    {
        private ScoreSystem _scoreSystem;
        private LivesSystem _livesSystem;
        private TimeAttackSystem _timeAttackSystem;
        private PersistenceSystem _persistenceSystem;
        
        private IDisposable _p1Input;
        private IDisposable _p2Input;
        
        [Header("Game Settings")]
        [SerializeField] private int startValue= 3;
        [SerializeField] private bool twoPlayerMode = false;
        [SerializeField] private GameMode mode = GameMode.Arcade;
     
        
        [Header("References")]
        [SerializeField] private ElfController elfPrefab;
        [SerializeField] private Transform spawnP1;
        [SerializeField] private Transform spawnP2;
        [SerializeField] private PresentSpawner spawner;

        private void Start()
        {
            _scoreSystem = new ScoreSystem();
            _livesSystem = new LivesSystem(startValue);
            
            var saveKey = mode == GameMode.Arcade ? "HS_Arcade" : "HS_TimeAttack";
            _persistenceSystem = new PersistenceSystem(new PlayerPrefsPersistence(), saveKey);
            
            switch (mode)
            {
                case GameMode.Arcade:
                    _livesSystem = new LivesSystem(startValue);
                    break;
                case GameMode.TimeAttack:
                    _timeAttackSystem = new TimeAttackSystem(startValue);
                    break;
            }
            
            _p1Input = new PlayerOneInput();
            _p2Input = new PlayerTwoInput();
            
            SpawnElf(_p1Input as IInputService, spawnP1.position, Color.white);
            
            if (twoPlayerMode)
            {
                SpawnElf(_p2Input as IInputService, spawnP2.position, Color.green);
            }
            
            spawner.StartSpawning();
        }

        private void Update()
        {
            if (mode == GameMode.TimeAttack && _timeAttackSystem != null)
            {
                _timeAttackSystem.Tick(Time.deltaTime);
            }
        }

        private void SpawnElf(IInputService input, Vector3 position, Color tint)
        {
            var elf = Instantiate(elfPrefab, position, Quaternion.identity);
            elf.Configure(input);
            elf.GetComponentInChildren<SpriteRenderer>().color = tint;
        }

        private void OnDestroy()
        {
            _scoreSystem?.Dispose();
            _livesSystem?.Dispose();
            _timeAttackSystem?.Dispose();
            _persistenceSystem?.Dispose();
            _p1Input?.Dispose();
            _p2Input?.Dispose();
        }
    }
}