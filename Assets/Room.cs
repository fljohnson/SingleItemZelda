using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	public Vector3 cameraPosition = Vector3.zero;
	public Vector3 cameraOrientationDegrees = Vector3.zero; 
	public static Player player;
	
	//private static C mainCamTransform =Camera.main.transform;
	
    // Start is called before the first frame update
    void Start()
    {
		//this is not always guaranteed to be called (2019.4.11f1)
    }
    
    public void MoveCamera() {
		/*
		Debug.Log("New room:Move camera to "+cameraPosition.ToString("F2"));
		Debug.Log("Its pitch,yaw, and roll "+cameraOrientationDegrees.ToString("F2"));
		*/
		
		if(player == null ) 
			player = GameObject.FindWithTag("Player").GetComponent<Player>();

        CameraManager.Instance.ChangeCamera(cameraPosition);
        Camera.main.transform.eulerAngles = cameraOrientationDegrees;
		player.WarpToNextRoom();
	}
}
