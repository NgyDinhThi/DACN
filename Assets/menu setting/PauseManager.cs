using System;
using UnityEngine;

public static class PauseManager
{
    public static bool IsPaused { get; private set; }

    public static event Action<bool> OnPauseChanged;

    public static void Pause()
    {
        if (IsPaused) return;
        IsPaused = true;

        Time.timeScale = 0f; 
        OnPauseChanged?.Invoke(true);
    }

    public static void Resume()
    {
        if (!IsPaused) return;
        IsPaused = false;

        Time.timeScale = 1f; 
        OnPauseChanged?.Invoke(false);
    }

    public static void TogglePause()
    {
        if (IsPaused) Resume();
        else Pause();
    }
}
