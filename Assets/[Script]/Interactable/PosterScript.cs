using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterScript : Interactable
{
    [SerializeField]
    Animator animator;
    AnimationClip[] allClip;

    protected override void OnInteraction()
    {
        if(animator)
        {
            int randomClip = Random.Range(0, allClip.Length - 1);
            animator.Play(allClip[randomClip].name);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.parent.GetComponentInChildren<Animator>();
        allClip = animator.runtimeAnimatorController.animationClips;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
