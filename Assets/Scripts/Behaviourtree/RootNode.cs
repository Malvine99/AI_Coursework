using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : MonoBehaviour
{
    BaseNode node;
   // private int debugCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        node = this.GetComponent<BaseNode>();
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Color.magenta;
        GetComponent<FSM>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (node.GetNodeState() == BaseNode.TreeNodeState.RUNNING)
        {
            node.RunNode();
        }
        else
        {
            //Debug.Log("resetting");
            node.ResetNode();
            node.StartNode();
        }

        //if (GetComponent<AIVariables>().id == 1)
        //{
        //    debugCount++;

        //    if (debugCount >= 100)
        //    {

        //        Debug.Log("BT time: " + Time.realtimeSinceStartupAsDouble);
        //        debugCount = 0;
        //    }
        //}
    }
}
