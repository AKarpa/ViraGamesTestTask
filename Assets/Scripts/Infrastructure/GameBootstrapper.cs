using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain curtain;
        private Game _game;

        private static GameBootstrapper _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
                
                _game = new Game(this, curtain);
                _game.StateMachine.Enter<BootstrapState>();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}