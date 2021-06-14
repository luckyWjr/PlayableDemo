using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

[RequireComponent(typeof(Animator))]
public class PlaySingleAnimation : MonoBehaviour
{
    public AnimationClip idleClip;

    PlayableGraph m_graph;

    void Start() {
        m_graph = PlayableGraph.Create("ChanPlayableGraph");
        m_graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
        var idleClipPlayable = AnimationClipPlayable.Create(m_graph, idleClip);
        animationOutputPlayable.SetSourcePlayable(idleClipPlayable);
        m_graph.Play();

        //AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), idleClip, out PlayableGraph graph);
    }

    void OnDestroy() {
        m_graph.Destroy();
    }
}
