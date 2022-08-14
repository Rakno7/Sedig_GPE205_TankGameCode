using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : Controller
{
    public int playerNumber = 0;
    public bool isIncompacitated = false;
    
    public GameObject PawnPrefab;
    public Animator anim;
    //CAMERASTUFF-------------------------
    public GameObject CurrentCamera;
    public Transform camFollowerTransform; 
    public Transform orientation;
    private float animRotationSpeed = 4;
    private float horizontalInput,verticalInput;
    public float SensitivityX, SensitivityY;
    public float RotationX, RotationY;
    public float rotationSpeed = 1f;
    public GameObject PlayerCamera;
    public bool isControllingHuman;
    public bool isControllingTank;
    private float MoveX;
    private float MoveY;

    //KEYBOARD_MOVEMENT_KEYS-----------------------
    public KeyCode JumpKey;
    public KeyCode AimKey;
    public KeyCode walkKey;
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode moveLeftKey;
    public KeyCode moveRightKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode AttackKey;
    public KeyCode EnterVehicleKey;
    public bool isGrounded()
        {
            if(Physics.Raycast(pawn.transform.position,-pawn.transform.up,2f))
            {
                Debug.Log("Grounded");
                anim.SetBool("isFalling",false);
                return true;
            }
            else
            {
                anim.SetBool("isFalling",true);
                Debug.Log("NOT Grounded");
                return false;
            }
        }
    
    private void Awake()
    {
        

    }
    public override void Start()
    {
       pawn = Instantiate(PawnPrefab, transform.position,Quaternion.identity).GetComponent<HumanPawn>();
       pawn.controller = this;
       pawn.controller.GetComponent<PlayerController>().orientation = pawn.Orientation.transform;
       anim = pawn.GetComponent<HumanPawn>().anim;
       GameManager.instance.SetPlayerNumbers();
       
        

       if (GameManager.instance != null)
        {

         if (GameManager.instance.players != null)
          {  
             GameManager.instance.players.Add(this);
          }
          if (GameManager.instance.humans != null)
          {  
             GameManager.instance.humans.Add(pawn.GetComponent<HumanPawn>());
          }
        }

        


        
        CurrentCamera = Instantiate(PlayerCamera);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        //set the spine for aiming. The spine is rotated by calling the Spine rotation function when aiming on the camera movement script.
        CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerSpine = pawn.GetComponent<HumanPawn>().spineToRotate;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().Firepoint = pawn.attacker.GetComponent<HumanAttacker>().Firepoint;
        pawn.gameObject.GetComponent<Health>().playerHUD = CurrentCamera.GetComponentInChildren<HudUi>();

        GameManager.instance.SetPlayerNumbers();
        

        base.Start();
    }

    
    public override void Update()
    {
         CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerCamera = PlayerCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().CurrentCamera = CurrentCamera;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = isControllingHuman;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = isControllingTank;
       
         
         if(!isIncompacitated)
         {
            if(playerNumber == 1)
            {
            ProcessInputs();
            }
            if(playerNumber == 2)
            {
            ProcessControllerInputs();
            }
         }

         //TODO: When both players are knocked out set to game over state, then reset the game.
        base.Update();
    }
    

    public override void ProcessInputs()
    {
         float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensitivityX;
          float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensitivityY;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationY += mouseX;
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationX -= mouseY;
        //DODO:Set position of listener only when actions are taken so when multiplayer is added each player can have 3d audio.
         GameManager.instance.listenerObj.transform.position = pawn.gameObject.transform.position;
       if(isGrounded())
       {

       }
       
       if(Input.GetKey(AttackKey))
       {
        anim.SetBool("isShooting",true);
       }
       else
       {
        anim.SetBool("isShooting",false);
       }
       if(Input.GetKey(AimKey))
       {
        anim.SetBool("isAiming",true);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = true;
       }
       else
       {
         anim.SetBool("isAiming",false);
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = false;
       }

       if(Input.GetKey(walkKey) || Input.GetKey(moveBackwardKey) || Input.GetKey(AimKey))
       {
        anim.SetBool("isRunning",false);
         pawn.moveSpeed = pawn.walkSpeed;
       }
       else if(pawn !=null && pawn.GetComponent<HumanPawn>()&& !Input.GetKey(moveBackwardKey) && !Input.GetKey(AimKey))
       {
        anim.SetBool("isRunning",true);
         pawn.moveSpeed = pawn.runSpeed;
       }

      if(Input.GetKey(moveForwardKey))
       {
           pawn.MoveForward();
           anim.SetBool("isMoving", true);
           
           MoveY += animRotationSpeed * Time.deltaTime;
           MoveY = Mathf.Clamp(MoveY, 0, 1);
           
           anim.SetFloat("moveDirY", MoveY);
           
       }
       if(!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey))
       {
           if(MoveY > 0)
           {
             MoveY -= animRotationSpeed * Time.deltaTime;
             MoveY = Mathf.Clamp(MoveY, 0, 1);
           }
           if(MoveY < 0)
           {
             MoveY += animRotationSpeed * Time.deltaTime;
             MoveY = Mathf.Clamp(MoveY, -1, 0);
           }
           
           anim.SetFloat("moveDirY", MoveY);
       }
       
      if (Input.GetKey(moveBackwardKey)) 
      {
          anim.SetBool("isMoving", true);
          pawn.MoveBackwards();
          MoveY -= animRotationSpeed * Time.deltaTime;
          MoveY = Mathf.Clamp(MoveY, -1, 0);
           
           anim.SetFloat("moveDirY", MoveY);
      }  
      if(Input.GetKey(moveLeftKey))
       {
           anim.SetBool("isMoving", true);
           pawn.MoveLeft();
          
           MoveX -= animRotationSpeed * Time.deltaTime;
           MoveX = Mathf.Clamp(MoveX, -1, 1);
           anim.SetFloat("moveDirX", MoveX);
       }
      if (Input.GetKey(moveRightKey)) 
      {
          anim.SetBool("isMoving", true);
          pawn.MoveRight();
          MoveX += animRotationSpeed * Time.deltaTime; 
          MoveX = Mathf.Clamp(MoveX, -1, 1);
          anim.SetFloat("moveDirX", MoveX);
      }  
      if(!Input.GetKey(moveLeftKey) && !Input.GetKey(moveRightKey))
      {
         if (MoveX > 0)
         {
          MoveX -= animRotationSpeed * Time.deltaTime;
          MoveX = Mathf.Clamp(MoveX, 0, 1);
         }
         if (MoveX < 0)
         {
          MoveX += animRotationSpeed * Time.deltaTime;
          MoveX = Mathf.Clamp(MoveX, -1, 0);
         }
          
          anim.SetFloat("moveDirX", MoveX);
      }
      if(!Input.GetKey(moveLeftKey) && !Input.GetKey(moveRightKey) &&!Input.GetKey(moveForwardKey) && !Input.GetKey(moveBackwardKey))
      {
         anim.SetBool("isMoving", false);
      }
      
      if (Input.GetKey(rotateClockwiseKey)) 
      {
          pawn.RotateClockwise();
      }  
      if (Input.GetKey(rotateCounterClockwiseKey)) 
      {
          pawn.RotateCounterClockwise();
      }
      if(Input.GetKey(AttackKey))
      {
          pawn.Attack();
      }
      else
      {
        if(pawn != null)
        pawn.MakeNoise(2);
      }
      if(Input.GetKeyDown(EnterVehicleKey))
      {
          pawn.EnterVehicle();
      }
      if(Input.GetKeyDown(JumpKey) && isGrounded())
      {
           pawn.JumpUp();
      }
      
     
    }

    public override void ProcessControllerInputs()
    {
        
        float mouseX = Input.GetAxisRaw("AimHorizontal") * Time.deltaTime * SensitivityX;
        float mouseY = Input.GetAxisRaw("AimVertical") * Time.deltaTime * SensitivityY;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationY += mouseX;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().RotationX -= mouseY;
        //audio listener follows player one
        GameManager.instance.listenerObj.transform.position = pawn.gameObject.transform.position;
       
        if(isGrounded())
      {
        
         
      }
      
       if(Input.GetAxisRaw("ShootGun") > 0)
       {
        anim.SetBool("isShooting",true);
        pawn.Attack();
        anim.SetBool("isRunning",false);
        pawn.moveSpeed = pawn.walkSpeed; 
       }
       else
       {
        anim.SetBool("isShooting",false);
       }
      
       if(Input.GetAxisRaw("AimGun") > 0)
       {
        anim.SetBool("isAiming",true);
        CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = true;
         anim.SetBool("isRunning",false);
        pawn.moveSpeed = pawn.walkSpeed; 
       }
       else
       {
         anim.SetBool("isAiming",false);
         CurrentCamera.GetComponentInChildren<CameraMovement1>().RotateSpine = false;
       }

      
//movement-------------------------------------------------------------------------->
    Vector3 Movement = new Vector3(Input.GetAxisRaw("MoveHorizontal"),Input.GetAxisRaw("MoveVertical"),0);
   
    if(Movement.y > 0){pawn.MoveForward();}
    if(Movement.y < 0){pawn.MoveBackwards();}
    if(Movement.x < 0){pawn.MoveLeft();}
     if(Movement.x > 0){pawn.MoveRight();}
    
      if(Movement.y > 0 && Movement.y < 0.5f || Movement.y < 0 ||Movement.x > 0 && Movement.x < 0.5f)
       { 
           anim.SetBool("isMoving", true);
           anim.SetBool("isRunning",false);
           pawn.moveSpeed = pawn.walkSpeed; 
       }
       if(Movement.y > 0.5f || Movement.x > 0.5f && Movement.y >= 0 || Movement.x < -0.5f && Movement.y >= 0)
       {
          anim.SetBool("isRunning",true);
         pawn.moveSpeed = pawn.runSpeed;
       }
       if(Movement.x == 0 && Movement.y == 0)
       {
        anim.SetBool("isMoving", false);
        anim.SetBool("isRunning",false);
       }
       
       anim.SetFloat("moveDirY", Movement.y);
       anim.SetFloat("moveDirX", Movement.x);


    //Tank-------------------------------------------------------------------------------
      if(Input.GetAxisRaw("MoveHorizontal")>0) 
      {
          pawn.RotateClockwise();
      }  
      if(Input.GetAxisRaw("MoveHorizontal")<0)
      {
          pawn.RotateCounterClockwise();
      }
      
//movement---------------------------------------------------------------------------->  
       if(Input.GetButtonDown("Interact"))
       {
        pawn.EnterVehicle(); 
       }

       if(Input.GetButtonDown("Jump") && isGrounded())
        {
            pawn.JumpUp();
        }

    }



    public void SetCameraSettings()
    {
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation; 
    }
    public void ResetCameraSettings()
    {
       Destroy(CurrentCamera);
       RotationX = 0;
       RotationY = 0;
       pawn = Instantiate(PawnPrefab, transform.position,Quaternion.identity).GetComponent<HumanPawn>();
       pawn.controller = this;
       orientation = pawn.Orientation.transform;
       anim = pawn.GetComponent<HumanPawn>().anim;
       isControllingTank = false;
       isControllingHuman = true;

       if (GameManager.instance != null)
        {

         if (GameManager.instance.players != null)
          {  
             GameManager.instance.players.Add(this);
          }
          if (GameManager.instance.humans != null)
          {  
             GameManager.instance.humans.Add(pawn.GetComponent<HumanPawn>());
          }
        }

        CurrentCamera = Instantiate(PlayerCamera);
        GameManager.instance.ResetSplitScreenCameraSettings();
        CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingTank = false;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().isControllingHuman = true;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting1 = pawn.CameraSetting1;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting2 = pawn.CameraSetting2;
        CurrentCamera.GetComponent<CameraFollowPlayer>().CameraSetting3 = pawn.CameraSetting3;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().orientation = orientation;
        //set the spine for aiming. The spine is rotated by calling the Spine rotation function when aiming on the camera movement script.
        CurrentCamera.GetComponentInChildren<CameraMovement1>().PlayerSpine = pawn.GetComponent<HumanPawn>().spineToRotate;
        CurrentCamera.GetComponentInChildren<CameraMovement1>().Firepoint = pawn.attacker.GetComponent<HumanAttacker>().Firepoint;
        pawn.gameObject.GetComponent<Health>().playerHUD = CurrentCamera.GetComponentInChildren<HudUi>();
        pawn.gameObject.GetComponent<Health>().playerHUD.SetScoreCount(Score);

    }

    public void OnDestroy()
    { 
        if (GameManager.instance != null) 
        {   
            if (GameManager.instance.players != null) 
            {
                GameManager.instance.players.Remove(this);
                Destroy(CurrentCamera);
                //later on this should temporarily spawn a new camera following the ai which killed the player until the player respawns.
                Destroy(gameObject);
            }
        }
    }

   
}
