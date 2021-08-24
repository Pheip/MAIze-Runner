using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class EscapeMazeAgent : Agent
{
    private float moveSpeed = 4f;
    bool check = true;
    Vector3 Player = Vector3.zero;
    Vector3 OldPosition = Vector3.zero;

    [SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin()
    {
        
        if (check)
        {
            GameObject go = gameObject;
            Debug.Log(go.transform.position);
            Player = go.transform.position;
            transform.localPosition = Player;
            OldPosition = Player;
            check = false;
        }
      
        if (!check)
        {
            transform.localPosition = OldPosition;
        }
        
       
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        float moveX = vectorAction[0];
        float moveZ = vectorAction[1];
       // Debug.Log(vectorAction[0]);
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
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(+10f);
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            //EndEpisode();
        }

    }
}
