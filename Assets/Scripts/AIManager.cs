using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private GameObject AI_prefab_;

    private bool BT_script_ = false;
    public List<GameObject> AI_instances = new List<GameObject>();
    private Vector3 start_position_;
    public int available_id = -1;

    private int number_of_AIs_ = 10;
    private Vector2 min_position;
    private Vector2 max_position;


    // Start is called before the first frame update
    void Start()
    {
        //instantiate AI objects via prefab
        start_position_ = new Vector3(-15, -11, 0);

        for(int i = 0; i < number_of_AIs_; i++)
        {
            AI_instances.Add((GameObject)Instantiate(AI_prefab_, new Vector3(start_position_.x + (i * 4), start_position_.y, start_position_.z), new Quaternion(0, 0, 0, 0)));
            AI_instances[i].GetComponent<AIVariables>().id = i;
        }

        foreach (GameObject obj in AI_instances)
        {
            obj.GetComponent<FSM>().enabled = false;
            obj.GetComponent<FSM>().enabled = true;
        }
        available_id = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(available_id > -1)
        {
            AI_instances.Insert(available_id, (GameObject)Instantiate(AI_prefab_, new Vector3(Random.Range(min_position.x, max_position.x),Random.Range(min_position.y, max_position.y), 0), new Quaternion(0, 0, 0, 0)));
            AI_instances[available_id].GetComponent<AIVariables>().id = available_id;

            if (BT_script_)
            {
                AI_instances[available_id].GetComponent<RootNode>().enabled = false;
                AI_instances[available_id].GetComponent<RootNode>().enabled = true;
            }
            else
            {
                AI_instances[available_id].GetComponent<FSM>().enabled = false;
                AI_instances[available_id].GetComponent<FSM>().enabled = true;
            }

            available_id = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BT_script_ = !BT_script_;
            if (BT_script_)
            {
                //Debug.Log("enabled BT");
                for (int i = 0; i < number_of_AIs_; i++)
                {
                    if (AI_instances[i])
                    {
                        AI_instances[i].GetComponent<RootNode>().enabled = true;
                        AI_instances[i].GetComponent<AIVariables>().setWandering(false);
                    }
                }
            }
            else
            {
                //Debug.Log("enabled FSM");
                for (int i = 0; i < number_of_AIs_; i++)
                {
                    if (AI_instances[i])
                    {
                        AI_instances[i].GetComponent<FSM>().enabled = true;
                        AI_instances[i].GetComponent<AIVariables>().setWandering(false);
                    }
                }
            }
        }
    }
}
