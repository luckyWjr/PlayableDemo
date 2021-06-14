using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayLayerAnimation : MonoBehaviour
{
    public AnimationClip runClip;
    public AnimationClip eyeCloseClip;
    public AvatarMask faceAvatarMask;

    PlayableGraph m_graph;

    void Start()
    {
        m_graph = PlayableGraph.Create("ChanPlayableGraph");

        var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
        var layerMixerPlayable = AnimationLayerMixerPlayable.Create(m_graph, 2);
        var runClipPlayable = AnimationClipPlayable.Create(m_graph, runClip);
        var eyeCloseClipPlayable = AnimationClipPlayable.Create(m_graph, eyeCloseClip);
        m_graph.Connect(runClipPlayable, 0, layerMixerPlayable, 0);
        m_graph.Connect(eyeCloseClipPlayable, 0, layerMixerPlayable, 1);
        animationOutputPlayable.SetSourcePlayable(layerMixerPlayable);

        //layerMixerPlayable.SetLayerMaskFromAvatarMask(1, faceAvatarMask);
        layerMixerPlayable.SetInputWeight(0, 1);
        layerMixerPlayable.SetInputWeight(1, 0.5f);
        m_graph.Play();
    }

    void OnDestroy() {
        m_graph.Destroy();
    }
}
