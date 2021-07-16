using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceProvider
{
    public static ServiceProvider _instance = null;

    public CameraService camera;
    public ActiveCharacterService activeCharacter;

    public static ServiceProvider Instance()
    {
        if (_instance == null)
        {
            _instance = new ServiceProvider();
        }
        return _instance;
    }
}
