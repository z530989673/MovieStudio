using UnityEngine;
using System.Collections;

public class EnterGameEventHandler {
    public static void Handle(Event evt)
    {
        UIManager.Instance.EnterGame();
    }
}
