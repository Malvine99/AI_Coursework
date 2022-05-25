using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDecorator : SelectorNode
{
    private bool condition_ = false;

    // Update is called once per frame
    protected override void NodeFunction()
    {
        condition_ = getConditionResult();

        if (condition_)
        {
            if (children_[0] != null)
            {
                BaseNode child_ = children_[0];
                child_.StartNode();
                TreeNodeState child_state_ = child_.RunNode();
                if (child_state_ == TreeNodeState.SUCCESS)
                {
                    this.node_state_ = TreeNodeState.SUCCESS;
                }
            }
        }
        else{
            this.node_state_ = TreeNodeState.FAILURE;
            children_[0].ForceStateChange(TreeNodeState.FAILURE);
        }
    }

    public virtual bool getConditionResult()
    {
        return false;
    }
}
