using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    public class Agent007 : Script
{
    /// <summary>
    /// The target object to follow.
    /// </summary>
    public Actor MoveToTarget;

    /// <summary>
    /// The offset applied to the actor position on moving it.
    /// </summary>
    public Vector3 Offset = new Vector3(0, 100, 0);

    /// <summary>
    /// Agent movement speed (units/second).
    /// </summary>
    public float Speed = 500.0f;

    private Vector3 _targetPos;
    private Vector3[] _path;
    private float _pathLength;
    private float _pathPosition;

    /// <inheritdoc />
    public override void OnUpdate()
    {
        if (!MoveToTarget)
        {
            Debug.LogError("No target to move.");
            return;
        }

        var currentPos = Actor.Position;
        var targetPos = MoveToTarget.Position;

        // Check if reached target location
        if (Vector3.Distance(ref currentPos, ref targetPos) < 2)
            return;

        // Check if need to build a new path
        if (targetPos != _targetPos)
        {
            _targetPos = targetPos;
            _pathPosition = 0;
            _pathLength = 0;
            if (!Navigation.FindPath(currentPos, targetPos, out _path))
            {
                Debug.LogWarning("Failed to find path to the target.");
                return;
            }

            // Move the start/end points to navmesh floor
            if (_path.Length != 0)
                Navigation.ProjectPoint(_path[0], out _path[0]);
            if (_path.Length > 1)
                Navigation.ProjectPoint(_path[_path.Length - 1], out _path[_path.Length - 1]);

            // Compute path length
            for (int i = 1; i < _path.Length; i++)
                _pathLength += Vector3.Distance(ref _path[i - 1], ref _path[i]);
        }

        // Skip if has no path
        if (_path == null)
            return;

        // Move
        var pathProgress = Mathf.Min(_pathLength * _pathPosition + Time.DeltaTime * Speed, _pathLength);
        _pathPosition = pathProgress / _pathLength;

        // Calculate position on path
        float segmentsSum = 0;
        for (int i = 0; i < _path.Length - 1; i++)
        {
            var segmentLength = Vector3.Distance(ref _path[i], ref _path[i + 1]);
            if (segmentsSum <= pathProgress && segmentsSum + segmentLength >= pathProgress)
            {
                float t = (pathProgress - segmentsSum) / segmentLength;
                targetPos = Vector3.Lerp(_path[i], _path[i + 1], t) + Offset;
                Actor.AddMovement(targetPos - currentPos);
                break;
            }

            segmentsSum += segmentLength;
        }
    }
}
}
