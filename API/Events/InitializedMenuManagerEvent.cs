using System;
using System.Collections.Generic;

namespace MenuLib.API.Events;

public class InitializedMenuManagerEvent
{
    private static List<Action> actions = new List<Action>();

    // I would recommend against using this event.
    // When modifying the menu it's better to do this in the InitializedEvent
    // This event will invoke right after generating the cache while InitializedEvent invokes
    // when the game and this library have finished their setup.
    public static void AddEventListener(Action listener)
    {
        actions.Add(listener);
    }

    internal static void Invoke()
    {
        foreach (Action action in actions)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                Plugin.Log.LogError("Encountered the following error while invoking InitializedMenuManager event listener");
                Plugin.Log.LogError(e);
            }
        }
    }
}