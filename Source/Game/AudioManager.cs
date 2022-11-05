using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// AudioManager Script.
    /// </summary>
    public class AudioManager : Script
    {

        public AudioSource EnemyBulletBounce;
        public AudioSource PlayerShot;
        public AudioSource[] Steps;
        public AudioSource[] Transformations;

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
            Debug.Log(PlayerShot.State + " " + PlayerShot.Time);
            if(Input.GetMouseButtonUp(MouseButton.Left)) PlayerShot.Stop();
        }

        public void PlayEnemyBulletBounce(){
            EnemyBulletBounce.Play();
        }

        public void PlayPlayerShot(){
            //PlayerShot.Time = 0;
            PlayerShot.Play();
        }

        public void PlayStep(int i){
            for(int j = 0; j < Steps.Length; j++) Steps[j].Stop();
            Steps[i].Play();
        }

        public void PlayTransformation(int i){
            Transformations[i].Play();
        }
    }
}
