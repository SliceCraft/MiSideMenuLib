using System;
using System.Collections.Generic;

namespace MenuLib.API.Events;

public class InitializedEvent
{
    private static List<Action> actions = new List<Action>();
    
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
                Plugin.Log.LogError("Encountered the following error while invoking Initialized event listener");
                Plugin.Log.LogError(e);
            }
        }
    }
}