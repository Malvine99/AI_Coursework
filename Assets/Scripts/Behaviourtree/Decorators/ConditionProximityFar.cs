using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionProximityFar : ConditionDecorator
{
    public override bool getConditionResult()
    {
        Vector3 self_location_ = root_transform_.position;
        Vector3 player_location_ = GameObject.Find("Player").transform.position;

        float distance_ = Vector3.Distance(self_location_, player_location_);

        if (distance_ > root_transform_.GetComponent<AIVariables>().near_distance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
