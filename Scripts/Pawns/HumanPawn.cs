using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPawn : Pawn
{
    public Animator anim;
    public bool canEnterVehicle = false;
    public GameObject VehicleToEnter;
    public Transform spineToRotate;
    public AudioPlayer audioPlayer;

    public float AttackVolume = 50;

    //Movement volume could be multiplied by velocity of the pawn
    public float MovementVolume = 2;

    public override void Start()
    {
        //turn off the Ragdoll until death
        SetRagdollRigidbodyState(true);
        SetRagdollColliderState(false);
        //base refers to the base class, this will run the start function on the base class.
        base.Start();
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
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }
        mover.Move(transform.forward,moveSpeed);
        MakeNoise(MovementVolume);
       // Debug.Log("Move Forward");
    }
    public override void JumpUp()
    {
        if(mover == null)
        {
            return;
        }
        mover.Jump(transform.up, 500);

    }

    public override void MoveBackwards()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }

        mover.Move(transform.forward, -moveSpeed);
        MakeNoise(MovementVolume);
        //Debug.Log("Move Backward");
    }
    public override void MoveLeft()
    {
        if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }
        mover.Move(transform.right, -moveSpeed);
        MakeNoise(MovementVolume);
    }
    public override void MoveRight()
    {
       if(mover == null)
        {
            Debug.LogWarning("Warning: No Mover in HumanPawn()!");
            return;
        }
        mover.Move(transform.right, moveSpeed);
        MakeNoise(MovementVolume);
    }

    public override void RotateClockwise()
    {
        return;
    }

    public override void RotateCounterClockwise()
    {
         return;
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    public override void Attack()
    {
         if(attacker == null)
        {
            Debug.LogWarning("Warning: No Attacker in HumanPawn()!");
            return;
        }
        MakeNoise(AttackVolume);
        attacker.Attack(transform.forward, AttackSpeed);
    }

    public override void MakeNoise(float Amount)
    {
        noiseMaker.volumeDistance = Amount;
    }

    public override void EnterVehicle()
    {   //only do this if pawn is actually in range.
    if(VehicleToEnter==null) return;
        if(!canEnterVehicle || VehicleToEnter.GetComponentInParent<TankPawn>().Driver !=null)
        {
            return;
        }
        if(controller.GetComponent<PlayerController>())
        {
        controller.pawn = VehicleToEnter.GetComponentInParent<Pawn>();
        controller.GetComponent<PlayerController>().orientation = VehicleToEnter.GetComponentInParent<Pawn>().Orientation.transform;
        VehicleToEnter.GetComponentInParent<Pawn>().controller = controller;
        //set the driver so we can re-enable the human pawn when they exit the vehicle
        VehicleToEnter.GetComponentInParent<TankPawn>().Driver = gameObject;
        
        controller.GetComponent<PlayerController>().isControllingHuman = false;
        controller.GetComponent<PlayerController>().isControllingTank = true;
        gameObject.SetActive(false);
        controller.GetComponent<PlayerController>().SetCameraSettings();
        }

        if(controller.GetComponent<AiController>())
        {
        controller.pawn = VehicleToEnter.GetComponentInParent<Pawn>();
        
        VehicleToEnter.GetComponentInParent<Pawn>().controller = controller;
        //set the driver so we can reenable the human player later;
        VehicleToEnter.GetComponentInParent<TankPawn>().Driver = gameObject;
        
        controller.GetComponent<AiController>().isControllingHuman = false;
        controller.GetComponent<AiController>().isControllingTank = true;
        gameObject.SetActive(false);
        } 
    }
        public void SetRagdollRigidbodyState(bool state) 
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach(Rigidbody rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = state;
            }
            GetComponent<Rigidbody>().isKinematic = !state;
        }
        public void SetRagdollColliderState(bool state) 
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = state;
            }
            GetComponent<Collider>().enabled = !state;
        }

      
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponentInParent<HumanPawn>())
            {
                HumanPawn pawn = other.GetComponentInParent<HumanPawn>();
                if(pawn != this)
                {
                   if(pawn.controller.GetComponent<PlayerController>())
                   {
                       HumanHealth health = gameObject.GetComponent<HumanHealth>();
                       health.PrepRevive();
                   } 
                }
            }
        }

        
        
}
