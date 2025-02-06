using MenuLib.API;
using MenuLib.API.Factories;

namespace MenuLib.ModSettings;

public class SettingsManager
{
    internal static void Init()
    {
        API.Menu menu = MenuManager.Instance.Find("Location Menu");
        if (menu == null)
        {
            Plugin.Log.LogError("Couldn't find the main menu");
            return;            
        }
        
        API.Menu settingsMenu = MenuManager.Instance.Find("Location MainOptions");
        if (settingsMenu == null)
        {
            Plugin.Log.LogError("Couldn't find the settings menu");
            return;
        }
        
        new MenuOptionFactory()
            .SetName("TESTING1")
            .SetParent(menu)
            .SetOnClick(ClickTest)
            .SetNextLocation(menu)
            .PlaceOptionBefore(3)
            .Build();
        
        new MenuOptionFactory()
            .SetName("TESTING2")
            .SetParent(menu)
            .SetOnClick(ClickTest)
            .SetNextLocation(menu)
            .PlaceOptionBefore(6)
            .Build();

        API.Menu builtMenu = new MenuFactory()
            .SetObjectName("Location Custom")
            .SetBackButton(menu)
            .SetTitle("CUSTOM MENU")
            .Build();
        
        new MenuOptionFactory()
            .SetName("ModsSettings")
            .SetParent(settingsMenu)
            .SetOnClick(ClickTest)
            .SetNextLocation(builtMenu)
            .PlaceOptionBefore(3)
            .Build();
        
        new MenuOptionFactory()
            .SetName("ModsMain")
            .SetParent(builtMenu)
            .SetOnClick(ClickTest)
            .SetNextLocation(settingsMenu)
            .PlaceOptionBefore(0)
            .Build();
        
        new MenuOptionFactory()
            .SetName("ModsMain2")
            .SetParent(builtMenu)
            .SetOnClick(ClickTest)
            .SetNextLocation(settingsMenu)
            .PlaceOptionBefore(1)
            .Build();
    }

    private static void ClickTest()
    {
        Plugin.Log.LogInfo("YOOOO YOU CLICKED, INSANE");
    }
}