using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChanAnimationController : BasePlayableController<ChanAnimationPlayable>
{
    public ChanAnimationSO chanAnimationSO;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitPlayable() {
        base.InitPlayable();

    }
}
