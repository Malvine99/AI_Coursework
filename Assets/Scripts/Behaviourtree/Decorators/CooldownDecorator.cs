using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDecorator : SelectorNode
{
    [SerializeField] private float block_time_;
    private float time_left_ = 0;

    private void Update()
    {
        time_left_ -= Time.deltaTime;
        if(time_left_ < 0)
        {
            time_left_ = 0;
        }
    }
    protected override void NodeFunction()
    {

        // If the node is allowed to run
        if (time_left_ <= 0)
        {
            // Get first child node (assume others don't exist)
            BaseNode child_ = children_[0];

            // Start Node - This does nothing if its already running
            child_.StartNode();

            // Run the Node and get its state
            TreeNodeState child_state_ = child_.RunNode();

            // If the child node stops running, reset the timer and update the decorator with the child nodes state
            if (child_state_ != TreeNodeState.RUNNING)
            {
                this.node_state_ = child_state_;
                time_left_ = block_time_;
            }
        }
        else
        {
            this.node_state_ = TreeNodeState.FAILURE;
            children_[0].ForceStateChange(TreeNodeState.FAILURE);
        }

    }
}
