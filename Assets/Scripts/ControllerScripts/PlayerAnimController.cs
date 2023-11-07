using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private AnimationClip idle;
    [SerializeField] private AnimationClip walk;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        idle.wrapMode = WrapMode.Loop;
        walk.wrapMode = WrapMode.Loop;
        animator.GetComponent<Animator>();
        animator.Play("Base Layer.metaring|Idle(1)", 0, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
