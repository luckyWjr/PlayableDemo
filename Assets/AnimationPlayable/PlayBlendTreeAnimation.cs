using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlayBlendTreeAnimation : MonoBehaviour
{
    public AnimationClip walkClip;
    public AnimationClip runClip;
    [Range(0, 1)] public float speed;

    PlayableGraph m_graph;
    AnimationMixerPlayable m_mixerPlayable;

    void Start()
    {
        m_graph = PlayableGraph.Create("ChanPlayableGraph");
        //m_graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
        m_mixerPlayable = AnimationMixerPlayable.Create(m_graph, 2);
        var walkClipPlayable = AnimationClipPlayable.Create(m_graph, walkClip);
        var runClipPlayable = AnimationClipPlayable.Create(m_graph, runClip);
        m_graph.Connect(walkClipPlayable, 0, m_mixerPlayable, 0);
        m_graph.Connect(runClipPlayable, 0, m_mixerPlayable, 1);
        animationOutputPlayable.SetSourcePlayable(m_mixerPlayable);

        walkClipPlayable.SetSpeed(1.3f);
        runClipPlayable.SetSpeed(0.8f);
        m_graph.Play();
    }

    void Update() {
        m_mixerPlayable.SetInputWeight(0, 1.0f - speed);
        m_mixerPlayable.SetInputWeight(1, speed);
    }

    void OnDestroy() {
        m_graph.Destroy();
    }
}
