using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{

    // 200x300 px window will apear in the center of the screen.
    private Rect windowRect = new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 50);
    // Only show it if needed.
    private bool show = false;

    public void ExitClicked()
    {
        show = true;
    }

    void OnGUI()
    {
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "Do you really want to quit?");
    }

    // This is the actual window.
    void DialogWindow(int windowID)
    {
        float y = 20;

        if (GUI.Button(new Rect(5, y, windowRect.width / 2 - 10, 20), "Cancel"))
        {
            show = false;
        }

        if (GUI.Button(new Rect(windowRect.width / 2 + 5, y, windowRect.width / 2 - 10, 20), "Quit"))
        {
            Application.Quit();
            show = false;
        }
    }
}
