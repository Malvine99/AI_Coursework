using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FSM : MonoBehaviour
{
    //private int debugCount = 0;

    public enum MachineState { Idle, Hungry, CloseToPlayer, InShelter };
    private MachineState current_state_;

    // Start is called before the first frame update
    void Start()
    {
        current_state_ = MachineState.Idle;
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    private void OnEnable()
    {
        GetComponent<RootNode>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.cyan;
        current_state_ = MachineState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state_)
        {
            case MachineState.Idle:
                current_state_ = GetComponent<FSM_IdleAction>().StateAction();
                break;
            case MachineState.Hungry:
                current_state_ = GetComponent<FSM_HungryAction>().StateAction();
                break;
            case MachineState.CloseToPlayer:
                current_state_ = GetComponent<FSM_CloseToPlayerAction>().StateAction();
                break;
            case MachineState.InShelter:
                current_state_ = GetComponent<FSM_InShelterAction>().StateAction();
                break;
            default:
                Debug.Log("Invalid state");
                break;
        }

        //if (GetComponent<AIVariables>().id == 1)
        //{
        //    debugCount++;

        //    if (debugCount >= 10)
        //    {

        //        Debug.Log("Time: " + Time.realtimeSinceStartupAsDouble + " State: " + current_state_);
        //        debugCount = 0;
        //    }
        //}
    }

}
