using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallbackSelector : SelectorNode
{
    protected override void NodeFunction()
    {
        
        // Flag to see if at least one child node has run
        bool has_run_child_ = false;
        //Debug.Log("Fallback selecting " + this);
        // Iterate over each child node
        foreach (BaseNode node in children_)
        {
            // Start Node - does nothing if already running
            node.StartNode();

            // If the child node has already failed, move to the next one
            if (node.GetNodeState() == TreeNodeState.FAILURE)
                continue;

            // State we have run a child node - we are about to
            has_run_child_ = true;

            // Run the node and check if it succeeds. If it does set this node to have succeeded
            if (node.RunNode() == TreeNodeState.SUCCESS)
            {
                this.node_state_ = TreeNodeState.SUCCESS;
            }

            // Once a single child node has been run, break from the for loop
            break;
        }

        // If no child nodes have been run, this selector has failed as well
        if (!has_run_child_)
        {
            this.node_state_ = TreeNodeState.FAILURE;
        }
    }
}
