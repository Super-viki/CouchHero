using UnityEngine;

namespace CouchHero.Data
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "CouchHero/LevelsData", order = 0)]
    public class LevelsData : ScriptableObject
    {
        public string[] levelNames;
    }
}