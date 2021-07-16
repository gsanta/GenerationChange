using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public GameObject startCharacter;

    void Awake()
    {
        Debug.Log("Main Start " + startCharacter.name);

        ServiceProvider.Instance().camera = new CameraService(Camera.main);
        ServiceProvider.Instance().activeCharacter = new ActiveCharacterService();
        ServiceProvider.Instance().activeCharacter.startCharacter = startCharacter;
    }

    void LateUpdate()
    {
        ServiceProvider.Instance().camera.Update();
    }
}
