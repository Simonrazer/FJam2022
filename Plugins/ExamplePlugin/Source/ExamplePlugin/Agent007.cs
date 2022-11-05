using System;
using System.Collections.Generic;
using FlaxEngine;

namespace ExamplePlugin
{
    /// <summary>
    /// CrowdSystem Script.
    /// </summary>
    /// <summary>
/// Navigation agents crowd system using <see cref="FlaxEngine.NavCrowd"/>.
/// </summary>
public class Agent007 : Script
{
    internal NavCrowd Crowd = null;
    internal int ID = -1;
    private Vector3 _targetPos;

    /// <summary>
    /// The target object to follow.
    /// </summary>
    public Actor MoveToTarget;
    public bool real_magic;

    /// <summary>
    /// The offset applied to the actor position on moving it.
    /// </summary>
    public Vector3 Offset = new Vector3(0, 100, 0);

    /// <summary>
    /// Agent properties.
    /// </summary>
    public NavAgentProperties Properties = new NavAgentProperties
    {
        Radius = 34.0f,
        Height = 144.0f,
        StepHeight = 35.0f,
        MaxSlopeAngle = 60.0f,
        MaxSpeed = 500.0f,
        CrowdSeparationWeight = 2.0f,
    };

    /// <inheritdoc />
    bool done = false;
    public override void OnEnable()
    {
        // Register
        //PluginManager.GetPlugin<MyPlugin>().AddAgent(this);
    }

    /// <inheritdoc />
    public override void OnDisable()
    {
        // Unregister
        PluginManager.GetPlugin<MyPlugin>().RemoveAgent(this);
    }

    /// <inheritdoc />
    public override void OnUpdate()
    {
        if(!done){
            if(real_magic)
                PluginManager.GetPlugin<MyPlugin>().AddAgent(this);
            else
                PluginManager.GetPlugin<MyPlugin2>().AddAgent(this);
            done = true;
            return;      
        }
        if (!MoveToTarget || !Crowd)
            return;
        var currentPos = Actor.Position;
        var targetPos = MoveToTarget.Position;

        // Check if need to change target position
        if (targetPos != _targetPos)
        {
            _targetPos = targetPos;
            Crowd.SetAgentMoveTarget(ID, targetPos);
        }

        // Update agent position (calculated by NavCrowd)
        targetPos = Crowd.GetAgentPosition(ID) + Offset;
        Actor.LocalPosition += (targetPos - currentPos);
    }
}
}
