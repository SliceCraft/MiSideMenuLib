using HarmonyLib;
using MenuLib.API;

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