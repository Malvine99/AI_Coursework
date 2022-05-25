using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_State : MonoBehaviour
{
    public virtual FSM.MachineState StateAction() { return FSM.MachineState.Idle; }

    public bool isDistanceNear(float near_distance)
    {
        Vector3 self_location_ = this.transform.position;
        Vector3 player_location_ = GameObject.Find("Player").transform.position;

        float distance_ = Vector3.Distance(self_location_, player_location_);
        if (distance_ < near_distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isDamageDistance(float damage_distance)
    {
        Vector3 self_location_ = this.transform.position;
        Vector3 player_location_ = GameObject.Find("Player").transform.position;

        float distance_ = Vector3.Distance(self_location_, player_location_);

        if (distance_ < damage_distance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
