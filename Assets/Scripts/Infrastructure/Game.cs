using Infrastructure.Services;
using Infrastructure.States;
using Logic;

namespace Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }

        public GameStateMachine StateMachine { get; }
    }
}