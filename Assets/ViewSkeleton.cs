using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewSkeleton : MonoBehaviour
{

    public Transform rootNode;
    public Transform[] childNodes;

    void OnDrawGizmos()
    {
        if (rootNode != null)
        {
            if (childNodes == null)
            {
                //get all joints to draw
                PopulateChildren();
            }


            foreach (Transform child in childNodes)
            {

                if (child == rootNode)
                {
                    //list includes the root, if root then larger, green cube
                    Gizmos.color = Color.blue;
                    Gizmos.DrawCube(child.position, new Vector3(1.0f, 1.0f, 1.0f));
                }
                else
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(child.position, child.parent.position);
                    Gizmos.DrawCube(child.position, new Vector3(.045f, .045f, .045f));
                }
            }

        }
    }

    public void PopulateChildren()
    {
        childNodes = rootNode.GetComponentsInChildren<Transform>();
    }
}
