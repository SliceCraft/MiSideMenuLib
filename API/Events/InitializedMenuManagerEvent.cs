using System;
using System.Collections.Generic;

namespace MenuLib.API.Events;

public static class InitializedMenuManagerEvent
{
    private static readonly List<Action> Actions = [];

    // I would recommend against using this event.
    // When modifying the menu it's better to do this in the InitializedEvent
    // This event will invoke right after generating the cache while InitializedEvent invokes
    // when the game and this library have finished their setup.
    public static void AddEventListener(Action listener)
    {
        Actions.Add(listener);
    }

    internal static void Invoke()
    {
        foreach (Action action in Actions)
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