using CouchHero.Data;
using CouchHero.Managers;
using Modules.Managers;
using UnityEngine;

namespace CouchHero
{
    public class RuntimeController : MonoBehaviour
    {
        private static RuntimeController _instance;
        public static RuntimeController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("RuntimeController").AddComponent<RuntimeController>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }
        
        private GameManager _gameManager;
        private MessageManager _messageManager;
        private SceneController _sceneController;
        private ResourceController _resourceController;

        public void LoadModule()
        {
            _gameManager = GameManager.Instance;
            _messageManager = MessageManager.Instance;
            _sceneController = SceneController.Instance;
            _resourceController = ResourceController.Instance;
        }

        public void InitializeModules()
        {
            LevelsData levelsData =
                _resourceController.LoadScriptableObject("ScriptableObjects/LevelsData.asset") as LevelsData;
            _sceneController.Initialize(levelsData);
            
            _gameManager.PlayGame();
        }
    }
}