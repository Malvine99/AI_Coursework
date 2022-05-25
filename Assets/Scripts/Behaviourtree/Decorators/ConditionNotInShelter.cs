using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNotInShelter : ConditionDecorator
{
    public override bool getConditionResult()
    {

        if (!root_transform_.GetComponent<AIVariables>().getInShelter())
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
