using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    public enum TreeNodeState { WAITING, RUNNING, SUCCESS, FAILURE };

    protected TreeNodeState node_state_ = TreeNodeState.WAITING;

    protected Transform root_transform_ = null;


    public BaseNode.TreeNodeState RunNode()
    {
        // If the node is running, run its override function
        root_transform_ = this.transform.root;
        if (node_state_ == TreeNodeState.RUNNING)
        {
            NodeFunction();
        }

        return node_state_;
    }

    protected virtual void NodeFunction()
    {
        // Do nothing, base class which needs different sections for selectors and leaf nodes
    }

    public virtual void ResetNode()
    {
        node_state_ = TreeNodeState.WAITING;
        //Debug.Log("reset" + this);
    }

    public void StartNode()
    {
        // Only allow node to run if it is waiting to run. 
        // Otherwise we need to reset the node
        if (node_state_ == TreeNodeState.WAITING)
        {
            node_state_ = TreeNodeState.RUNNING;
        }
    }

    public BaseNode.TreeNodeState GetNodeState()
    {
        return node_state_;
    }

    public virtual void ForceStateChange(TreeNodeState newState)
    {
        // Allow for other nodes for force a state change. Make this virtual so it can be overwritten with instance specific code
        node_state_ = newState;
    }

    
}
