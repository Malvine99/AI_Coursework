using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConditionViableShelter : ConditionDecorator
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
                if (root_transform_.GetComponent<AIVariables>().isInRange(new Vector2(position.x, position.y), root_transform_.position, shelter_range_radius_))
                {
                    if (root_transform_.GetComponent<AIVariables>().isPathClear(position, root_transform_.position, GameObject.Find("Player").transform.position))
                    {
                        root_transform_.GetComponent<AIVariables>().setShelterGoal(position);
                        return true;
                    }
                }
            }
        }

        return false;
    }

}
