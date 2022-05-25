using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNotHungry : ConditionDecorator
{
    public override bool getConditionResult()
    {
        if (!root_transform_.GetComponent<AIVariables>().isHungry())
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
