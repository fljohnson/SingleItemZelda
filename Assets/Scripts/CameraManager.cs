using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    protected static CameraManager _instance = null;

    public float cameraSpeed;

    Vector3 newCameraPosition;

    public GameObject[] PrimaryItemHud;
    public GameObject[] SecondaryItemHud;

    public int currentPrimaryItem = 4;
    public int currentSecondaryItem = 4;

    public static CameraManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CameraManager>();
                if (_instance == null)
                {
                    // still no MasterScript present, raise awareness:
                    Debug.LogError("An instance of type CamerasManager is needed in the scene, but there is none!");
                }
            }
            return _instance;
        }
    }

    private void Update()
    {
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, newCameraPosition, cameraSpeed);
    }


    public void ChangeCamera(Vector3 cameraPosition)
    {
        newCameraPosition = cameraPosition;
    }

    //switch out the item UI.
    public void UpdatePrimaryItem(string newItem)
    {
        PrimaryItemHud[currentPrimaryItem].SetActive(false);
        PrimaryItemHud[ReadItem(newItem)].SetActive(true);
        currentPrimaryItem = ReadItem(newItem);
    }
    public void UpdateSecondaryItem(string newItem)
    {
        SecondaryItemHud[currentSecondaryItem].SetActive(false);
        SecondaryItemHud[ReadItem(newItem)].SetActive(true);
        currentSecondaryItem = ReadItem(newItem);
    }


    //match the item name to its appropriate array number.
    int ReadItem(string readMe)
    {
        if(readMe == "Key")
        {
            return 0;
        }
        else if (readMe == "Bridge")
        {
            return 1;
        }
        else if (readMe == "Treasure")
        {
            return 2;
        }
        else if (readMe == "Lantern")
        {
            return 3;
        }
        else if (readMe == null)
        {
            return 4;
        }
        else
        {
            return 4;
        }
    }
}
