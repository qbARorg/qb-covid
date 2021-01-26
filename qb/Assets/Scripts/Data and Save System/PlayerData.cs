using DataAndSaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    //Volume
    public float Volume_Main = 100f;
    public float Volume_Voice = 100f;
    public float Volume_Effects = 100f;
    public float Volume_Ambient = 100f;
    public float Volume_Music = 100f;

    //Graphics
    public int frameRateLimiter = 300;
    public bool vSync = true;
    public FullScreenMode windowed = FullScreenMode.ExclusiveFullScreen;
    public int boidsCount = 30;

    //Camera
    public float playerFOV = 80f;

    //Controls
    public bool playerYAxisInverted = false;
    public bool playerControlsPosChanged = false;
    public bool snapSpeedJoystick = true;
    public bool snapMovJoystick = false;

    //Score System
    public int redBestScore = 0;
    public int whiteBestScore = 0;
    public int plataletBestScore = 0;

    public PlayerData ()
    {
        Volume_Main = GameData.Volume_Main;
        Volume_Voice = GameData.Volume_Voice;
        Volume_Effects = GameData.Volume_Effects;
        Volume_Ambient = GameData.Volume_Ambient;
        Volume_Music = GameData.Volume_Music;

        frameRateLimiter = GameData.frameRateLimiter;
        vSync = GameData.vSync;
        windowed = GameData.windowed;
        boidsCount = GameData.boidsCount;

        playerFOV = GameData.playerFOV;

        playerYAxisInverted = GameData.playerYAxisInverted;
        playerControlsPosChanged = GameData.playerControlsPosChanged;
        snapSpeedJoystick = GameData.snapSpeedJoystick;
        snapMovJoystick = GameData.snapMovJoystick;

        redBestScore = GameData.redBestScore;
        whiteBestScore = GameData.whiteBestScore;
        plataletBestScore = GameData.plataletBestScore;
    }

    public void SetGlobals()
    {
        GameData.Volume_Main = Volume_Main;
        GameData.Volume_Voice = Volume_Voice;
        GameData.Volume_Effects = Volume_Effects;
        GameData.Volume_Ambient = Volume_Ambient;
        GameData.Volume_Music = Volume_Music;

        GameData.frameRateLimiter = frameRateLimiter;
        GameData.vSync = vSync;
        GameData.windowed = windowed;
        GameData.boidsCount = boidsCount;

        GameData.playerFOV = playerFOV;

        GameData.playerYAxisInverted = playerYAxisInverted;
        GameData.playerControlsPosChanged = playerControlsPosChanged;
        GameData.snapSpeedJoystick = snapSpeedJoystick;
        GameData.snapMovJoystick = snapMovJoystick;
        
    }
}
