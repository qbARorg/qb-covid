using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

namespace DataAndSaveSystem
{
    public static class GameData
    {
        //Volume
        [Range(0f, 100f)]
        public static float Volume_Main = 100f;
        [Range(0f, 100f)]
        public static float Volume_Voice = 100f;
        [Range(0f, 100f)]
        public static float Volume_Effects = 100f;
        [Range(0f, 100f)]
        public static float Volume_Ambient = 100f;
        [Range(0f, 100f)]
        public static float Volume_Music = 100f;

        //Graphics
        [Range(30f, 300f)]
        public static int frameRateLimiter = 300;
        public static bool vSync = false;
        public static FullScreenMode windowed = FullScreenMode.ExclusiveFullScreen;
        public static int boidsCount = 30;

        //Camera
        [Range(50f, 120f)]
        public static float playerFOV = 80f;

        //Controls
        public static bool playerYAxisInverted = false;
        public static bool playerControlsPosChanged = false;
        public static bool snapSpeedJoystick = true;
        public static bool snapMovJoystick = false;

        //Best scores
        public static int redBestScore = 0;
        public static int whiteBestScore = 0;
        public static int plataletBestScore = 0;


        public static void SaveGame()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/save.fun";
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData();

            formatter.Serialize(stream, data);

            stream.Close();
        }

        public static PlayerData LoadGame()
        {
            string path = Application.persistentDataPath + "/save.fun";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                data.SetGlobals();

                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found in" + path);
                ResetCfgComponentsFunction();
                return null;
            }
        }

        public static void ResetGameFunction()
        {
            //Scores
            redBestScore = 0;
            whiteBestScore = 0;
            plataletBestScore = 0;
        }

        public static void ResetCfgComponentsFunction()
        {
            Volume_Main = 100f;
            Volume_Voice = 100f;
            Volume_Effects = 100f;
            Volume_Ambient = 100f;
            Volume_Music = 100f;

            //Graphics
            frameRateLimiter = 300;
            vSync = false;
            windowed = FullScreenMode.ExclusiveFullScreen;
            boidsCount = 30;

            playerFOV = 80f;

            //Controls
            playerYAxisInverted = false;
            playerControlsPosChanged = false;
            snapSpeedJoystick = true;
            snapMovJoystick = false;
        }
    }
}