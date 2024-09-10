using Modules.Managers;
using UnityEngine;

public enum GameState
{
    None,
    MainMenu,
    LevelSelection,
    Settings,
    InGame,
    Pause,
    GameOver
}

namespace CouchHero.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("[GameManager]").AddComponent<GameManager>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                
                return _instance;
            }
        }
        
        public GameState GameState { get; private set; }

        private void Awake()
        {
            GameState = GameState.None;
        }

        public void PlayGame()
        {
            GameState = GameState.MainMenu;
            SceneController.Instance.LoadMainMenuScene();
        }

        public void EnterLevel(int levelIndex)
        {
            GameState = GameState.InGame;
            SceneController.Instance.LoadGameLevel(levelIndex);
        }
    }
}