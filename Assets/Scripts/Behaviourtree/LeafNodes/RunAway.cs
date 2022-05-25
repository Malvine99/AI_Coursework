using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : LeafNode
{
    protected override void NodeFunction()
    {
        root_transform_.GetComponent<AIVariables>().setRunningChance0(false);
        root_transform_.GetComponent<AIVariables>().setRunningChance1(false);
        root_transform_.GetComponent<AIVariables>().setWandering(false);


        float goalX = GameObject.Find("Player").transform.position.x - root_transform_.position.x;
        float goalY = GameObject.Find("Player").transform.position.y - root_transform_.position.y;

        Vector2 direction = new Vector2(goalX, goalY);
        direction.Scale(new Vector2(-1, -1));
        direction.Normalize();


        root_transform_.GetComponent<AIVariables>().setVelocity(direction * root_transform_.GetComponent<AIVariables>().run_speed * Time.deltaTime);

        this.node_state_ = TreeNodeState.SUCCESS;
    }
}

