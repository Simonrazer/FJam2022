using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// ControllerBoing Script.
    /// </summary>
    
    public class ControllerBoing : Script
    {

        [Tooltip("HealthPoints of the Player")]
        public int HealthPoints{get; set;} = 5;

        public Prefab PlayerBullet;

        public bool isBat {get; set;} = false;
        public float speed {get; set;} = 5000;

        [Tooltip("Dampening when no input")]
        public float SlowBreak { get; set; } = 15.0f;
        
        [Tooltip("Dampening when no input")]
        public float SpeedBreak { get; set; } = 10.0f;
        
        [Tooltip("Dash cooldown Time")]
        public float DashTime { get; set; } = 2.0f;

        [Tooltip("Dash Multiplier")]
        public float DashMultiplier { get; set; } = 40.0f;

        /// <inheritdoc/>
        RigidBody rb;
        AudioManager AM;
        Random rnd;
        Actor slashActor;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            rb = Actor.As<RigidBody>();
            AM = Actor.Scene.FindActor("AudioManager").FindScript<AudioManager>();
            rnd = new Random();
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        float lastDashTime = 0;
        float lastSlashCooldown = 0;
        bool isSlashing = false;
        Vector3 slashDir = Vector3.Zero;

        float StepTimer = 0;
        int lastRandomStep = -1;

        public override void OnUpdate()
        {
            float multp = 1.0f;
            if (Input.GetAction("Dash") && Time.GameTime - lastDashTime > DashTime){
                lastDashTime = Time.GameTime;
                multp = DashMultiplier;
            }

            Vector3 wantedDir = new Vector3(Input.GetAxis("Horizontal")*speed*multp, 0 , Input.GetAxis("Vertical")*speed*multp);
            if(!wantedDir.IsZero){
                rb.LinearDamping = SpeedBreak;
                rb.AddForce(wantedDir);

                StepTimer += Time.DeltaTime;
                if(StepTimer >= 0.5 && !isBat){
                    Debug.Log("Stepsound");
                    int StepID = -1;
                    do {
                       StepID = rnd.Next(0,5);
                    }
                    while(lastRandomStep == StepID);
                    AM.PlayStep(StepID);
                    lastRandomStep = StepID;
                    StepTimer = 0;
                }

            }
            else{
                rb.LinearDamping = SlowBreak;
            }

            //shooting
            if(Input.GetMouseButtonDown(MouseButton.Left)){
                Vector2 ScreenMiddle = Screen.Size / 2;
                Vector2 shootDir = Input.MousePosition - ScreenMiddle;
                shootDir.Normalize();
                
                Actor Bullet = PrefabManager.SpawnPrefab(PlayerBullet, new Vector3(Actor.Position.X, 50, Actor.Position.Z));
                BulletMovement bm = Bullet.FindScript<BulletMovement>();
                bm.setCanBounce(false);
                bm.setType(BulletType.Linear);
                bm.create(new Vector3(shootDir.X, 0 ,-shootDir.Y), 50000);
                AM.PlayPlayerShot();
                
            }
        }

        public void transformation(){
            if(isBat){
                isBat = false;
                speed = 5000;
                DashMultiplier = 40.0f;
                //change model to vampire
            }
            else{
                isBat = true;
                speed = 7500;
                DashMultiplier = 1.0f;
                //change model to bat
            }
        }



    }
}
