using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EscapeMazeAgent : Agent
{
    public float moveSpeed = 1f;
    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = Vector3.zero;
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float moveZ = vectorAction[1];
        Debug.Log(vectorAction[0]);
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxisRaw("Horizontal");
        actionsOut[1] = Input.GetAxisRaw("Vertical");
        Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    private void OnTriggerEnter(Collider other)
    {
        SetReward(+1f);
        EndEpisode();
    }
}
