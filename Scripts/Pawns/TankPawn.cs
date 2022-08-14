using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn 
{
    public AudioSource Audio;
    public AudioClip RotateSound;
    public AudioClip MoveSound;
    public AudioClip IdleSound;
    public GameObject Driver;
    public float CannonShotVolume = 40;

    //Movement volume could be multiplied by velocity of the pawn
    public float MovementVolume = 5;
    public override void Start()
    {   
        
        if (GameManager.instance != null)
        {
         if (GameManager.instance.Vehicles != null)
          {
            GameManager.instance.Vehicles.Add(this);
          }
        }
        base.Start();
        Audio.clip = IdleSound;
         
         
    }

    public override void Update()
    {
        base.Update();
       
    }


    //we will override the base class method and define how we want tanks to move specifically.
    public override void MoveForward()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        if(!Audio.isPlaying)
         {
            Audio.Play();
         }
        mover.Move(transform.forward,moveSpeed);
        MakeNoise(MovementVolume);
       // Debug.Log("Move Forward");
    }
    public override void MoveBackwards()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        if(!Audio.isPlaying)
         {
            Audio.Play();
         }
        mover.Move(transform.forward, -moveSpeed);
        MakeNoise(MovementVolume);
        //Debug.Log("Move Backward");
    }

    public override void RotateClockwise()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn!");
            return;
        }
        if(!Audio.isPlaying)
         {
            Audio.Play();
         }
        mover.Rotate(turnSpeed);
        //Debug.Log("Rotate Clockwise");
    }
    public override void JumpUp()
    {
        return;
    }

    public override void RotateCounterClockwise()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        if(!Audio.isPlaying)
         {
            Audio.Play();
         }

        mover.Rotate(-turnSpeed);
       // Debug.Log("Rotate Counter Clockwise");
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
         if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in TankPawn()!");
            return;
        }
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    public override void Attack()
    {
         if(attacker == null)
        {
            Debug.LogWarning("Warning: No Attacker in TankPawn()!");
            return;
        }
        
        attacker.Attack(transform.forward, AttackSpeed);
        MakeNoise(CannonShotVolume);
    }
    public override void MoveLeft()
    {
        return;
    }
    public override void MoveRight()
    {
        return;
    }

    public override void MakeNoise(float Amount)
    {
         if(noiseMaker == null)
        {
            Debug.LogWarning("Warning: noiseMaker in TankPawn()!");
            return;
        }
       // Vector3 velocity = GetComponent<Rigidbody>().velocity.normalized;
        float totalSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        noiseMaker.volumeDistance = Amount * totalSpeed;
    }

    public override void EnterVehicle()
    {
           GameManager.instance.humans.Remove(Driver.GetComponent<HumanPawn>());        
           Destroy(Driver);
           Driver = null;
           Vector3 ExitLocation = new Vector3(transform.position.x - 3,transform.position.y + 3,transform.position.z);
           controller.GetComponent<PlayerController>().ResetCameraSettings();
           controller.pawn.transform.position = ExitLocation; 
           
    }


    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {

         if (GameManager.instance.Vehicles != null)
          {
            GameManager.instance.Vehicles.Remove(this);
          }
        }
    }

}
