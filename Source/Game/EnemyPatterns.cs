using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// EnemyPatterns Script.
    /// </summary>
    public enum Pattern 
    {
    Static, Distance, Circle, Random, Cycle
    }   

    public class EnemyPatterns : Script
    {
        public Pattern pattern;
        public Actor player;
        public float radius = 100;
        public float delta = 50;
        public float speed = 1;
        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            player = Actor.Scene.FindActor("PlayerPhysics");
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
            var dist = Vector3.Distance(player.Position, Actor.Position);
            var dir = Vector3.Subtract(player.Position, Actor.Position).Normalized;
            dir.Y = 0;

            switch(pattern){
                case Pattern.Distance:
                    Actor.Position += dir * (dist-radius);
                    break;
                case Pattern.Circle:
                    Actor.Position += dir * (float)((dist-radius) * ((Math.Sin(Time.GameTime)) * delta));
                    break;
                case Pattern.Static:

                    break;
                case Pattern.Cycle:
                    var deltaC = Math.Sin(Time.GameTime/speed) * delta;
                    Actor.Position += dir * (float)((dist-radius-deltaC));
                break;
            }
        }
    }
}
