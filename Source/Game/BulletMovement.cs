using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{

    enum BulletType{
        Linear, Curved, Bomb    
    }

    /// <summary>
    /// BulletMovement Script.
    /// </summary>
    public class BulletMovement : Script
    {

        RigidBody rb;
        BulletType type;
        bool canBounce
        

        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
        }

        public void create(Vector3 direction, float speed){
            rb.AddForce(speed*direction);
        }

        //needs collision method, either bounce or destroy
        public void bounce(bool xDirection){

        }

        public void setType(BulletType bt){
            type = bt;
        }

        public void setCanBounce(bool cb){
            canBounce = cb
        }
    }
}
