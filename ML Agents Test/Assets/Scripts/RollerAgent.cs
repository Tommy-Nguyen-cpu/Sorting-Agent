
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class RollerAgent: Agent
{

    Rigidbody rBody;
    public Transform Target_Trash;
    public Transform Target_Recycle;
    public float forceMultiplier = 10;
    private float step_punishment = -.00001f;
    private float time = 0;
    private const float MAX_TIME = 300f;
    private float stationaryThreshold = 0.1f;
    private float stationaryTime = 0f;
    private float max_stationary_time = 2f;

    public List<GameObject> items_track;
    // Start is called before the first frame update
    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, -4.5f, 0);
        }

        // Move the target to a new spot
        // Target.localPosition = new Vector3(Random.value * 8 - 4, 1f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        base.CollectObservations(sensor);
        sensor.AddObservation(StepCount / (float)MaxStep);
        sensor.AddObservation(Target_Trash.localPosition);
        sensor.AddObservation(Target_Recycle.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.forward);
        sensor.AddObservation(transform.rotation);

        foreach (GameObject item in items_track)
        {
            Vector3 goal = Vector3.zero;
            if (item.name.Contains("Trash"))
                goal = Target_Trash.localPosition;
            else
                goal = Target_Recycle.localPosition;

            sensor.AddObservation(Vector3.Distance(goal, item.transform.localPosition));
            sensor.AddObservation(Vector3.Distance(item.transform.localPosition, transform.localPosition));
            sensor.AddObservation(item.name.Contains("Trash"));
        }


        // print("Rigid body is null? " + rBody == null);
        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = new Vector3(1,0,1);
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Penalize for distance.
        AddReward(step_punishment);

        // print("Cumulative Reward: " + GetCumulativeReward());
/*        if (GetCumulativeReward() < -10f)
        {
            time += 1;
        }
        else
        {
            time = 0;
        }*/

        // Check if the agent is considered stationary
        if (rBody.velocity.magnitude < stationaryThreshold)
        {
            stationaryTime += Time.fixedDeltaTime; // Increment the stationary time
        }
        else
        {
            stationaryTime = 0; // Reset if moving
        }
        if (stationaryTime > max_stationary_time)
        {
            AddReward(-1f);
            // transform.position = new Vector3(0, -4.5f, 0);
            // EndEpisode();
            
        }

/*        float minDist = 10000;
        foreach(var item in items_track)
        {
            float dis = Vector3.Distance(item.transform.position.normalized, transform.position.normalized);
            // print("normalized: " + Vector3.Distance(item.transform.position.normalized, transform.position.normalized));
            if (dis < minDist)
                minDist = dis;
        }

        // print("dis: " + minDist * .00001f);
        AddReward(.5f - minDist * .1f);*/
    }

    private float epsilon = 1f;
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if(Random.value < epsilon)
        {
            continuousActionsOut[0] = Random.value;
            continuousActionsOut[1] = Random.value;

            epsilon -= .0001f;
        }
        else
        {
            continuousActionsOut[0] = Input.GetAxis("Horizontal");
            continuousActionsOut[1] = Input.GetAxis("Vertical");
        }
/*        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");*/
        // Debug.Log("Horizontal input: " + Input.GetAxis("Horizontal"));
        // Debug.Log("Vertical input: " + Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        // print("Time: " + time);
        if (time >= MAX_TIME)
        {
            Debug.Log("Progress timeout reached. Ending episode.");
            transform.position = new Vector3(0, -4.5f, 0);
            time = 0f;
            EndEpisode();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Wall") || collision.gameObject.name == "Trash" || collision.gameObject.name == "Recycle")
        {
            AddReward(-.5f);
            // EndEpisode();
            // transform.localPosition = new Vector3(0, 1f, 0);
        }
        else if (collision.gameObject.name.Contains("Item"))
        {
            AddReward(.6f);
            var dir = (collision.contacts[0].point - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * 200f);
        }
    }

}

