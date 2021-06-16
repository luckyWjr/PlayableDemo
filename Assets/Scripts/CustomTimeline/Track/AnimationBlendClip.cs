using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationBlendClip : PlayableAsset
{
    public AnimationClip firstClip;
    public AnimationClip secondClip;
    [Range(0, 1)] public float firstClipWeight;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var scriptPlayable = ScriptPlayable<AnimationBlendPlayable>.Create(graph, 1);
        var animationBlendPlayable = scriptPlayable.GetBehaviour();
        animationBlendPlayable.Init(firstClip, secondClip, firstClipWeight);
        return scriptPlayable;
    }
}
