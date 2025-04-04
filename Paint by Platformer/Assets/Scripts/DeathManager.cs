using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public static class DeathManager
{
    public static int numDeaths;
    public static float timeInLevel;

    public static void InitGame()
    {
        numDeaths = 0;
        timeInLevel = 0;
    }

    public static void AddDeath()
    {
        numDeaths++;
    }
    public static void AddTime(float time)
    {
        timeInLevel += time;
    }

    public static int getDeaths()
    {
        return numDeaths;
    }
    public static float getTime()
    {
        return timeInLevel; 
    }
}
