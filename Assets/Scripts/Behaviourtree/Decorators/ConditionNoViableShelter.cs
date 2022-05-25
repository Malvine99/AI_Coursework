using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConditionNoViableShelter : ConditionDecorator
{
    [SerializeField] private float shelter_range_radius_;
    [SerializeField] private Tilemap shelter_map_;

    private void Start()
    {
        shelter_map_ = GameObject.Find("Shelter").GetComponent<Tilemap>();

        // Add the BaseNodes from all children to the list
        children_ = new List<BaseNode>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            BaseNode node = this.transform.GetChild(i).gameObject.GetComponent<BaseNode>();
            if (node != null)
            {
                children_.Add(node);
            }
        }

    }

    public override bool getConditionResult()
    {

        foreach (var position in shelter_map_.cellBounds.allPositionsWithin)
        {
            if (shelter_map_.HasTile(position))
            {
                if (isInRange(position, root_transform_.position))
                {
                    if (isPathClear(position, root_transform_.position, GameObject.Find("Player").transform.position))
                    {
                        root_transform_.GetComponent<AIVariables>().setShelterGoal(new Vector3(0, 0, 0));
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool isInRange(Vector3Int shelter_position, Vector3 self_position)
    {
        if (Vector3.Distance(shelter_position, self_position) < shelter_range_radius_)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool isPathClear(Vector3Int shelter_position, Vector3 self_position, Vector3 player_position)
    {
        if (Vector3.Distance(shelter_position, self_position) < Vector3.Distance(shelter_position, player_position))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
