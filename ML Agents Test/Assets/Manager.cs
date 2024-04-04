using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject Prefab_Agent;
    public Transform Target_Trash;
    public Transform Target_Recycle;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject obj = Instantiate(Prefab_Agent);
            obj.GetComponent<RollerAgent>().Target_Trash = Target_Trash;
            obj.GetComponent<RollerAgent>().Target_Recycle = Target_Recycle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
