using UnityEngine;

namespace CouchHero.Managers
{
    public class ResourceController
    {
        private static ResourceController _instance;
        public static ResourceController Instance
        {
            get
            {
                return _instance ??= new ResourceController();
            }
        }

        public ScriptableObject LoadScriptableObject(string scriptableObjectName)
        {
            return Resources.Load<ScriptableObject>(scriptableObjectName);
        }
    }
}