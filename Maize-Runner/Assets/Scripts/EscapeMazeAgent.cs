using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EscapeMazeAgent : Agent
{
    public float moveSpeed = 4f;
    bool check = true;
    Vector3 PlayerPosition = Vector3.zero;
    [SerializeField] private Transform targetTransform;
    public Material winMaterial;
    Material defaultMaterial;
    
    public override void OnEpisodeBegin()
    {
        
        if (check)
        {
             
            GameObject go = gameObject;
            Debug.Log(go.transform.position);
            PlayerPosition = go.transform.localPosition;
            transform.localPosition = PlayerPosition;  
            check = false;
            defaultMaterial = gameObject.GetComponent<MeshRenderer>().material;
        }
      
        else
        {
            LearningInstance li = this.transform.parent.GetComponent<LearningInstance>();
            li.createNewMaze();
            
            transform.localPosition = placeGameFigure();
            
        }
        
       
    }

    private Vector3 placeGameFigure()
    {
        InstanceInformation info = this.transform.parent.GetComponent<InstanceInformation>();
        int maxX = info.maze.GetUpperBound(1);
        int maxY = info.maze.GetUpperBound(0);
        int x = 0;
        int y = 0;
        do
        {
            x = Random.Range(maxX - 5, maxX - 1);
            y = Random.Range(0, maxY - 1);
        } while (info.maze[x, y] == 1);
        Debug.Log("Player instantiate at: " + (x) + " " + (y));

        return new Vector3(x, 0.5f, y);
   
    }

    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        var input = vectorAction.ContinuousActions;
        float moveX = input[0];
        float moveZ = input[1];
       // Debug.Log(vectorAction[0]);
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        AddReward(-.1f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
       
    }
 
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxisRaw("Horizontal");
        action[1] = Input.GetAxisRaw("Vertical");
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            
           SetReward(+10f);
            Debug.Log("Finish");
            gameObject.GetComponent<MeshRenderer>().material = winMaterial;
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            AddReward(-0.2f);
           // Debug.Log("HitWall");
        }

    }
}
