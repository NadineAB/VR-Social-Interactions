using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Define variables 
    public Transform headsetCamera;
    public Vector3 Offset;
    Animator anim;
    Transform headBone;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        headBone = anim.GetBoneTransform(HumanBodyBones.Neck);
    }
    // Update is called once per frame
    void Update()
    {
        headBone.LookAt(headsetCamera.position);
        headBone.rotation = headsetCamera.rotation * Quaternion.Euler(Offset);

    }
}
