using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class BasePlayableController<T> : MonoBehaviour where T : class, IPlayableBehaviour, new()
{
    protected PlayableGraph m_playableGraph;
    protected AnimationPlayableOutput m_animationOutputPlayable;
    public T playableBehaviour;

    public virtual void InitPlayable()  {
        m_playableGraph = PlayableGraph.Create(this.name);
        m_animationOutputPlayable = AnimationPlayableOutput.Create(m_playableGraph, "AnimationOutput", GetComponent<Animator>());
        var scriptPlayable = ScriptPlayable<T>.Create(m_playableGraph);
        playableBehaviour = scriptPlayable.GetBehaviour();
    }
}
