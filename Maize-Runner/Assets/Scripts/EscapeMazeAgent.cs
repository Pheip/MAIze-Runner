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
    bool hitWall;

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
            Debug.Log(targetTransform.localPosition + " LOKAL");
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
      
        transform.localPosition += new Vector3(moveX, 0.0f, moveZ) * Time.deltaTime * moveSpeed;
        AddReward(-0.001f);     
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        InstanceInformation info = this.transform.parent.GetComponent<InstanceInformation>();
        float normX = transform.localPosition.x / info.maze.GetUpperBound(1);
        float normZ = transform.localPosition.z / info.maze.GetUpperBound(0);
        sensor.AddObservation(new Vector3(normX,0,normZ));
        sensor.AddObservation(hitWall);

        //sensor.AddObservation(distance);


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
            
            AddReward(+1f);
            Debug.Log("Finish");
            gameObject.GetComponent<MeshRenderer>().material = winMaterial;
            EndEpisode();
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            AddReward(-0.001f);
           //Debug.Log("HitWall");
        }
        if (other.TryGetComponent<Reward>(out Reward reward))
        {
            AddReward(+0.001f);
            Destroy(other.gameObject);
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            hitWall = true;
            AddReward(-0.001f);
            //Debug.Log("LStay");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            hitWall = false;
            //Debug.Log("Left");
        }
    }

}
