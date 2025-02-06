using MenuLib.API;
using MenuLib.ModSettings;
using UnityEngine.SceneManagement;

namespace MenuLib.Events;

public class SceneLoadedEvent
{
    public static void RegisterEvent()
    {
        SceneManager.sceneLoaded += (UnityEngine.Events.UnityAction<Scene, LoadSceneMode>)OnSceneLoaded;
    }
    
    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneMenu")
        {
            // This variable won't be stored since we don't need to.
            // MenuManager is a singleton and that logic is handled inside the MenuManagers constructor.
            new MenuManager();
        }
    }
}