using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_InShelterAction : FSM_State
{
    public override FSM.MachineState StateAction()
    {
        GetComponent<AIVariables>().updateHealth(2);

        GetComponent<AIVariables>().setVelocity(new Vector2(0, 0));

        if (isDistanceNear(GetComponent<AIVariables>().near_distance))
        {
            return FSM.MachineState.InShelter;
        }
        else
        {
            return FSM.MachineState.Idle;
        }
    }
}
