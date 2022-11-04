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
    Static, Distance, Circle, Random, Cycle, DingDing, BigCircle
    }   

    public class EnemyPatterns : Script
    {
        public Pattern pattern;
        public Actor player;
        public float radius = 100;
        public float delta = 50;
        public float speed = 1;
        Vector3 sPos;
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
            sPos = Actor.Position;
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

            var sdist = Vector3.Distance(player.Position, sPos);
            var sdir = Vector3.Subtract(player.Position, sPos).Normalized;
            sdir.Y = 0;
            dir.Y = 0;
            var dirSW = new Vector3(dir.Z, 0, -dir.X);
            switch(pattern){
                case Pattern.Distance:
                    Actor.Position += dir * (dist-radius);
                    break;
                case Pattern.Circle:
                    var circleVec = new Vector3(Math.Sin(Time.GameTime*speed)*delta,0,Math.Cos(Time.GameTime*speed)*delta);
                    sPos += sdir * (sdist-radius);
                    Actor.Position = sPos + circleVec;
                    break;
                case Pattern.Static:

                    break;
                case Pattern.Cycle:
                    var deltaC = Math.Sin(Time.GameTime*speed) * delta;
                    Actor.Position += dir * (float)((dist-radius+deltaC));
                    break;
                case Pattern.BigCircle:
                    Actor.Position += dir * (float)((dist-radius));
                    Actor.Position += dirSW * speed;
                    break;

                case Pattern.DingDing:
                    var deltaX = Math.Sin(Time.GameTime*speed) * delta;
                    var deltaY = Math.Sin(Time.GameTime*speed) * delta;
                    Actor.Position += dir * (float)((dist-radius+deltaX));
                    Actor.Position += dir * (float)((dist-radius+deltaY));
                    break;
            }
        }
    }
}
