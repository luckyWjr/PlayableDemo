using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class AnimationBlendPlayable : PlayableBehaviour
{
    public float firstClipWeight;
    AnimationMixerPlayable m_mixerPlayable;
    PlayableGraph m_playableGraph;

    public void Init(AnimationClip clip1, AnimationClip clip2, float weight) {
        m_playableGraph.Connect(AnimationClipPlayable.Create(m_playableGraph, clip1), 0, m_mixerPlayable, 0);
        m_playableGraph.Connect(AnimationClipPlayable.Create(m_playableGraph, clip2), 0, m_mixerPlayable, 1);
        weight = Mathf.Clamp01(weight);
        m_mixerPlayable.SetInputWeight(0, weight);
        m_mixerPlayable.SetInputWeight(1, 1.0f - weight);
    }

    public override void OnPlayableCreate(Playable playable) {
        Debug.Log("OnPlayableCreate");
        base.OnPlayableCreate(playable);

        m_playableGraph = playable.GetGraph();
        m_mixerPlayable = AnimationMixerPlayable.Create(m_playableGraph, 2);
        m_playableGraph.Connect(m_mixerPlayable, 0, playable, 0);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info) {
        Debug.Log("OnBehaviourPlay");
        base.OnBehaviourPlay(playable, info);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info) {
        Debug.Log("OnBehaviourPause");
        base.OnBehaviourPause(playable, info);
    }


    public override void PrepareFrame(Playable playable, FrameData info) {
        base.PrepareFrame(playable, info);
    }
}
