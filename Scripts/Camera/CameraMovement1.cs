using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement1 : MonoBehaviour
{
    //PlayerSpine is set from the playerController on start
    public Transform AimPosition;
    public Transform ThirdPersonPosition;
    public Transform PlayerSpine;
    public Transform orientation;
    public Transform Firepoint;
    public Transform camFollowerTransform;
    public float RotationX;
    public float RotationY;
    public bool isControllingTank;
    public bool isControllingHuman;
    public float rotationSpeed;
    public GameObject PlayerCamera;
    public GameObject CurrentCamera;
    public CameraFollowPlayer cameraFollowerScript;
    private Quaternion from;
    private Quaternion to;
    public KeyCode ChangeCamPositionKey;
    public bool RotateSpine = false;
    public GameObject CrossHair;
    public CameraShake aimShake;
    public CameraShake idleShake;
   
    public  void Start()
    {
     CrossHair.transform.parent = null;

    }

    
    public void Update()
    {  
       if(Input.GetKeyDown(ChangeCamPositionKey))
       {
         ChangeCameraPos();
       }
       to = camFollowerTransform.rotation;
       if(RotateSpine && isControllingHuman)
       {
        if(Physics.Raycast(Firepoint.position,Firepoint.forward,out RaycastHit hit))
          {
            if(!hit.transform.gameObject.GetComponent<ProjectileExplode>())
            {
            Vector3 hitpoint = hit.point;
            CrossHair.transform.position = hitpoint;
            CrossHair.transform.rotation = Firepoint.transform.rotation;
            CrossHair.SetActive(true);
            }
            
          }
          else
          {
            CrossHair.SetActive(false);
          }
         RotationX = Mathf.Clamp(RotationX,camFollowerTransform.rotation.x -80f, camFollowerTransform.rotation.x +80f);
         //RotationY = Mathf.Clamp(RotationY,camFollowerTransform.rotation.y -80f, camFollowerTransform.rotation.y +80f);
       }
       if(!RotateSpine || isControllingTank)
       {
        RotationX = Mathf.Clamp(RotationX, -30f, 15f);
        
       }
    }
    public void LateUpdate()
    {
         from = orientation.rotation;
         //apply rotation to camera
         camFollowerTransform.rotation = Quaternion.Euler(RotationX, RotationY, 0);
         //Slerp the current position and desired position overtime
         if(isControllingTank)
         {
         orientation.rotation = Quaternion.Slerp(from, to, rotationSpeed * Time.deltaTime);
         }
         if(isControllingHuman)
         {
          orientation.rotation = Quaternion.Euler(0, RotationY, 0);
         }
         if(RotateSpine && isControllingHuman)
         {
            //TODO:make this a function called from the player controller when the aim key is first pressed to save on performance.
         transform.position = AimPosition.position;  
         PlayerSpine.rotation = Quaternion.Euler(RotationX, RotationY, 0);
        
          
         
        // CrossHair.SetActive(true);
        // CrossHair.transform.position = new Vector3(Firepoint.position.x,Firepoint.position.y,Firepoint.position.z);
        // CrossHair.transform.localRotation = Firepoint.localRotation;
         }
         else
         {
          CrossHair.SetActive(false);
          transform.position = ThirdPersonPosition.position;
         }

         //--------FOR:Instant movement along with camera-----------------
        // orientation.rotation = Quaternion.Euler(RotationX, RotationY, 0);
    }

    //TODO: I think this should be in the PlayerController
     public void ChangeCameraPos()
    {
        if(cameraFollowerScript.Setting <=2)
        {
        cameraFollowerScript.Setting +=1;
        }
        else
        {
        cameraFollowerScript.Setting = 1;
        }
      
    }
  
}
