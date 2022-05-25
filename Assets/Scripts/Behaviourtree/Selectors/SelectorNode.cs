using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BaseNode
{
    protected List<BaseNode> children_;

    // Start is called before the first frame update
    void Start()
    {
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

    public override void ResetNode()
    {
        // If a seletor node is asked to reset, reset all child nodes
        base.ResetNode();
        if (children_ != null)
        {
            foreach (BaseNode child in children_)
            {
                child.ResetNode();
            }
        }
    }
}
