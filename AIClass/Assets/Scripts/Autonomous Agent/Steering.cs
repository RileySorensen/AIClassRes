using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position) - agent.transform.position);
        return force;
    }
    public static Vector3 Flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));
        return force;
    }
    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 ndirection = direction.normalized;
        Vector3 desired = ndirection * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }
    public static Vector3 Wander(AutonomousAgent agent)
    {
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.wanderDisplacement, agent.wanderDisplacement);
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        Vector3 point = rotation * (Vector3.forward * agent.wanderRadius);
        Vector3 forward = agent.transform.forward * agent.wanderDistance;

        Vector3 force = CalculateSteering(agent, forward + point);

        return force;
    }
}
