using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCharacterService 
{

    private GameObject bikeCharacter;
    private GameObject droneCharacter;
    public GameObject activeCharacter;
    public GameObject startCharacter;

    public void SetBikeCharacter(GameObject gameObject)
    {
        bikeCharacter = gameObject;

        if (startCharacter == bikeCharacter)
        {
            UpdateActiveCharacter(bikeCharacter);
        }
    }

    public void SetDroneCharacter(GameObject gameObject)
    {
        droneCharacter = gameObject;

        if (startCharacter == droneCharacter)
        {
            UpdateActiveCharacter(droneCharacter);
        }
    }

    public void UpdateActiveCharacter(GameObject character)
    {
        activeCharacter = character;

        if (character == droneCharacter)
        {
            ServiceProvider.Instance().camera.AttachTo(droneCharacter.GetComponent<SatelliteController>().viewPoint);
        }
        else
        {
            ServiceProvider.Instance().camera.AttachTo(bikeCharacter.GetComponent<BicycleController>().viewPoint);
        }
    }

    public GameObject GetOtherCharacter(GameObject character)
    {
        return character == bikeCharacter ? droneCharacter : bikeCharacter;
    }
}
