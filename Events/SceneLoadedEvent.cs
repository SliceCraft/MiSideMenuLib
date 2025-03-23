using MenuLib.API;
using UnityEngine.SceneManagement;

namespace MenuLib.Events;

public static class SceneLoadedEvent
{
    public static void RegisterEvent()
    {
        SceneManager.sceneLoaded += (UnityEngine.Events.UnityAction<Scene, LoadSceneMode>)OnSceneLoaded;
    }
    
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneMenu")
        {
            MenuManager.Initialize();
        }
    }
}