using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class ChanAnimationPlayable : PlayableBehaviour
{
    AnimationMixerPlayable m_mixerPlayable;
    public AnimationMixerPlayable mixerPlayable => m_mixerPlayable;

    PlayableGraph m_playableGraph;
    public PlayableGraph playableGraph => m_playableGraph;

    public override void OnPlayableCreate(Playable playable) {
        base.OnPlayableCreate(playable);

        m_playableGraph = playable.GetGraph();
        m_mixerPlayable = AnimationMixerPlayable.Create(m_playableGraph);
    }

    public override void PrepareFrame(Playable playable, FrameData info) {
        base.PrepareFrame(playable, info);
    }
}
