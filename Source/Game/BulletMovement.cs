using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{

    public enum BulletType{
        Linear, Curved, Bomb    
    }

    /// <summary>
    /// BulletMovement Script.
    /// </summary>
    public class BulletMovement : Script
    {

        RigidBody rb;
        public BulletType type;
        public bool canBounce;
        public Vector3 originalDirection;
        public float originalSpeed;
        

        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
            rb = Actor.As<RigidBody>();
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {

        }

        public void create(Vector3 direction, float speed){
            rb.AddForce(speed*direction);
            originalDirection = direction;
            originalSpeed = speed;
        }

        public void bounce(bool xDirection){
            Vector3 newDir;
            rb.AddForce(-originalSpeed*originalDirection);

            if(xDirection) newDir = new Vector3(originalDirection.X, originalDirection.Y, -originalDirection.Z);
            else newDir = new Vector3(-originalDirection.X, originalDirection.Y, originalDirection.Z);

            create(newDir, originalSpeed);
        }

        //dummy method, either destroy on Wall contact or bounce, depending on bullet
        public void Collision(){
            if(canBounce){
                Debug.Log("BOUNCE");
                bounce(true);
            }
            else{
                Destroy(Actor);
            }
        }

        public void setType(BulletType bt){
            type = bt;
        }

        public void setCanBounce(bool cb){
            canBounce = cb;
        }
    }
}
