using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItem : MonoBehaviour
{
    public RollerAgent agent;
    public ItemGenerator generator;
    private void OnCollisionEnter(Collision collision)
    {
        if(agent != null)
        {
            if ((name.Contains("RecycleItem") & collision.gameObject.name == "Recycle") ||
    name.Contains("TrashItem") & collision.gameObject.name == "Trash")
            {
                print("Item Hit Target!");
                agent.AddReward(1000f);
                agent.EndEpisode();
                agent = null;
                transform.position = new Vector3(Random.Range(generator.MIN_RANGE_X, generator.MAX_RANGE_X), -4.5f, Random.Range(generator.MIN_RANGE_Z, generator.MAX_RANGE_Z));
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            else if ((name.Contains("RecycleItem") & collision.gameObject.name == "Trash") ||
                name.Contains("TrashItem") & collision.gameObject.name == "Recycle")
            {
                print("Item Hit Wrong Target...");
                // agent.AddReward(-5f);
                // agent.EndEpisode();
                agent = null;
                transform.position = new Vector3(Random.Range(generator.MIN_RANGE_X, generator.MAX_RANGE_X), -4.5f, Random.Range(generator.MIN_RANGE_Z, generator.MAX_RANGE_Z));
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        
        if (collision.gameObject.name.Contains("Agent"))
        {
            agent = collision.gameObject.GetComponent<RollerAgent>();
        }
        else if (collision.collider.name.Contains("Wall"))
        {
            // If the item hits a wall and the agent is the one to cause it, penalize the agent.
/*            if(agent != null)
            {
                agent.AddReward(-.01f);
            }*/
            agent = null;
            transform.position = new Vector3(Random.Range(generator.MIN_RANGE_X, generator.MAX_RANGE_X), -4.5f, Random.Range(generator.MIN_RANGE_Z, generator.MAX_RANGE_Z));
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
