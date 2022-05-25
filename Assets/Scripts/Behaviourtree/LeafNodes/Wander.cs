using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : LeafNode
{
    [SerializeField] private float movement_range_;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private bool grazing_ = false;
    private float elapsed_time_ = 0f;
    private float lerp_time_ = 0;
    private Vector2 startPosition;
    private Vector2 goal;
    protected override void NodeFunction()
    {
        root_transform_.GetComponent<AIVariables>().setInShelter(false);

        if (!root_transform_.GetComponent<AIVariables>().getWandering())
        {

            startPosition = root_transform_.position;

            goal = root_transform_.GetComponent<AIVariables>().makeNewGoal(root_transform_, movement_range_);

        }


        if (goal.x != 0 && goal.y != 0)
        {
            elapsed_time_ += Time.deltaTime;
            lerp_time_ += Time.deltaTime / cooldown;
            root_transform_.position = Vector2.Lerp(startPosition, goal, lerp_time_);

            if (elapsed_time_ > cooldown)
            {
                elapsed_time_ = 0f;
                lerp_time_ = 0f;
                this.node_state_ = TreeNodeState.SUCCESS;

                root_transform_.GetComponent<AIVariables>().resolveWander(root_transform_, grazing_);
            }

        }
        else
        {
            this.node_state_ = TreeNodeState.FAILURE;
        }

    }
}
