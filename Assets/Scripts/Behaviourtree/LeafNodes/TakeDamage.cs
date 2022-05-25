using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : LeafNode
{
    [SerializeField] private int damage_;
    protected override void NodeFunction()
    {
       // Debug.Log("taking damage");
        if (!root_transform_.GetComponent<AIVariables>().getInShelter())
        {
            root_transform_.GetComponent<AIVariables>().updateHealth(damage_);
            this.node_state_ = TreeNodeState.SUCCESS;
        } 
        else
        {
            this.node_state_ = TreeNodeState.FAILURE;
        }
    }
}
