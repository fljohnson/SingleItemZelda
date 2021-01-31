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
}
