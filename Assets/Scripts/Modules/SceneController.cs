using UnityEngine;
using UnityEngine.SceneManagement;
using CouchHero.Constants;
using CouchHero.Data;

namespace Modules.Managers
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;
        public static SceneController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("[SceneController]").AddComponent<SceneController>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                
                return _instance;
            }
        }

        public int LevelsNumber => _levelsData.levelNames.Length;

        private LevelsData _levelsData;

        public void Initialize(LevelsData levelsData)
        {
            _levelsData = levelsData;
        }
        
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(GameConstants.SceneConstants.SceneMainMenu);
        }

        public void LoadOptionsScene()
        {
            SceneManager.LoadScene(GameConstants.SceneConstants.SceneOptionsMenu);
        }

        public void LoadLevelSelectMenu()
        {
            SceneManager.LoadScene(GameConstants.SceneConstants.SceneLevelSelectMenu);
        }

        public void LoadGameLevel(int level)
        {
            if (level < 0 || level >= LevelsNumber)
            {
                return;
            }
            
            SceneManager.LoadScene(_levelsData.levelNames[level]);
        }
    }
}