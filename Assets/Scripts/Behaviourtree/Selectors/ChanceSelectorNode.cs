using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceSelectorNode : SelectorNode
{

    private int percentage_;
    TreeNodeState child_state_;
    protected override void NodeFunction()
    {
        //set percent chance between 2 child nodes if not already running 1
        if (!root_transform_.GetComponent<AIVariables>().getRunningChance0() && !root_transform_.GetComponent<AIVariables>().getRunningChance1())
        {
            percentage_ = Random.Range(1, 100);
            if (percentage_ <= 50)
            {
                children_[0].StartNode();
                child_state_ = children_[0].RunNode();
                root_transform_.GetComponent<AIVariables>().setRunningChance0(true);
            }
            else if (percentage_ > 50)
            {
                children_[1].StartNode();
                child_state_ = children_[1].RunNode();
                root_transform_.GetComponent<AIVariables>().setRunningChance1(true);
            }
            else
            {
                this.node_state_ = TreeNodeState.FAILURE;
            }
        }
        else if (root_transform_.GetComponent<AIVariables>().getRunningChance0())
        {
            child_state_ = children_[0].RunNode();
        }
        else if (root_transform_.GetComponent<AIVariables>().getRunningChance1())
        {
            child_state_ = children_[1].RunNode();
        }

        if (child_state_ == TreeNodeState.SUCCESS)
        {
            this.node_state_ = TreeNodeState.SUCCESS;
            root_transform_.GetComponent<AIVariables>().setRunningChance0(false);
            root_transform_.GetComponent<AIVariables>().setRunningChance1(false);

        }
        else if (child_state_ == TreeNodeState.FAILURE)
        {
            this.node_state_ = TreeNodeState.FAILURE;
            root_transform_.GetComponent<AIVariables>().setRunningChance0(false);
            root_transform_.GetComponent<AIVariables>().setRunningChance1(false);
        }

    }


}
