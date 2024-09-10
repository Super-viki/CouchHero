using CouchHero;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private RuntimeController _runtimeController;
    private void Start()
    {
        _runtimeController = RuntimeController.Instance;
        
        _runtimeController.LoadModule();
        _runtimeController.InitializeModules();
    }
}
