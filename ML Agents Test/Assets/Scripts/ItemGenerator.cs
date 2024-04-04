using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public float MAX_RANGE_X = 45.0f;
    public float MIN_RANGE_X = -45.0f;
    public float MAX_RANGE_Z = 45.0f;
    public float MIN_RANGE_Z = -45.0f;
    public float Probability_of_Item = .5f;
    public GameObject TrashItem;
    public GameObject RecycleItem;
    public RollerAgent agent_script;


    public GameObject Trash;
    public GameObject Recycle;
    public GameObject Agent;
    public int Num_Agents = 30;

    private List<GameObject> Items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GenerateNewItem();
        }

        GenerateAgent();
    }

    public enum ObjectType{
        TRASH, RECYCLE
    }
    public void GenerateNewItem()
    {
        float random_prob = Random.Range(0f, 1f);
        if (random_prob > Probability_of_Item)
        {
            GameObject instantiatedObject = Instantiate(TrashItem, new Vector3(Random.Range(MIN_RANGE_X, MAX_RANGE_X), -4.5f, Random.Range(MIN_RANGE_Z, MAX_RANGE_Z)), Quaternion.identity);
            CollisionItem script = instantiatedObject.GetComponent<CollisionItem>();
            script.generator = this;
            script.agent = agent_script;
            Items.Add(instantiatedObject);
        }
        else
        {
            GameObject instantiatedObject = Instantiate(RecycleItem, new Vector3(Random.Range(MIN_RANGE_X, MAX_RANGE_X), -4.5f, Random.Range(MIN_RANGE_Z, MAX_RANGE_Z)), Quaternion.identity);
            CollisionItem script = instantiatedObject.GetComponent<CollisionItem>();
            script.generator = this;
            script.agent = agent_script;
            Items.Add(instantiatedObject);
        }
    }

    public void GenerateAgent()
    {
        for(int i = 0; i < Num_Agents; i++)
        {
            GameObject Instantiated_agent = Instantiate(Agent);
            RollerAgent script = Instantiated_agent.GetComponent<RollerAgent>();
            script.Target_Trash = Trash.transform;
            script.Target_Recycle = Recycle.transform;
            script.items_track = Items;
        }
    }
}
