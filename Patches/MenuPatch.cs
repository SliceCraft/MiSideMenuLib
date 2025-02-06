using HarmonyLib;
using MenuLib.API;
using MenuLib.ModSettings;

namespace MenuLib.Patches;

[HarmonyPatch(typeof(Menu))]
public class MenuPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(Menu.Start))]
    public static void StartPatch()
    {
        SettingsManager.Init();
    }
}