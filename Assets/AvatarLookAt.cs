/* AvatarLookAt class
// Forces the virtual character to look at the participant face
// Nadine Abu Rumman, UCL 2019*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AvatarLookAt : MonoBehaviour
{
    // Define variables 
    public Transform headsetCamera;
    public Vector3 Offset;
    Animator anim;
    Transform headBone;
    public bool currentlyReading = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        headBone = anim.GetBoneTransform(HumanBodyBones.Neck);
        currentlyReading = AutoExperimentInstruction.Reading;
    }
    // Update is called once per frame
    void Update()
    {
        currentlyReading = AutoExperimentInstruction.Reading;
        if (!currentlyReading)
        {
            headBone.LookAt(headsetCamera.position);
            headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
        }
    }
}