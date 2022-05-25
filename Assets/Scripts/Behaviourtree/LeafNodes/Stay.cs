using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : LeafNode
{
    protected override void NodeFunction()
    {
        root_transform_.GetComponent<AIVariables>().setRunningChance0(false);
        root_transform_.GetComponent<AIVariables>().setRunningChance1(false);
        root_transform_.GetComponent<AIVariables>().setWandering(false);

        root_transform_.GetComponent<AIVariables>().updateHealth(2);

        root_transform_.GetComponent<AIVariables>().setVelocity( new Vector2(0,0));

        this.node_state_ = TreeNodeState.SUCCESS;

    }
}
