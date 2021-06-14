using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayableAddAnimatorController : MonoBehaviour
{
    public RuntimeAnimatorController animatorController;
    public AnimationClip runLeftClip;
    public AnimationClip runRightClip;
    public AnimationClip eyeCloseClip;
    public AvatarMask faceAvatarMask;
    [Range(-1, 1)] public float directWeight;
    [Range(0, 1)] public float faceWeight;


    PlayableGraph m_graph;
    AnimationLayerMixerPlayable m_layerMixerPlayable;
    AnimationMixerPlayable m_mixerPlayable;

    void Start() {
        m_graph = PlayableGraph.Create("ChanPlayableGraph");

        var animationOutputPlayable = AnimationPlayableOutput.Create(m_graph, "AnimationOutput", GetComponent<Animator>());
        //blend 三个动画，想左跑、向右跑、以及AnimatorController里的向前跑
        m_mixerPlayable = AnimationMixerPlayable.Create(m_graph, 3);
        //根据AnimatorController创建AnimatorControllerPlayable
        var controllerPlayable = AnimatorControllerPlayable.Create(m_graph, animatorController);
        var runLeftClipPlayable = AnimationClipPlayable.Create(m_graph, runLeftClip);
        var runRightClipPlayable = AnimationClipPlayable.Create(m_graph, runRightClip);
        m_graph.Connect(runLeftClipPlayable, 0, m_mixerPlayable, 0);
        m_graph.Connect(controllerPlayable, 0, m_mixerPlayable, 1);
        m_graph.Connect(runRightClipPlayable, 0, m_mixerPlayable, 2);

        //动作和面部表情分层
        m_layerMixerPlayable = AnimationLayerMixerPlayable.Create(m_graph, 2);        
        var eyeCloseClipPlayable = AnimationClipPlayable.Create(m_graph, eyeCloseClip);
        m_graph.Connect(m_mixerPlayable, 0, m_layerMixerPlayable, 0);
        m_graph.Connect(eyeCloseClipPlayable, 0, m_layerMixerPlayable, 1);
        m_layerMixerPlayable.SetLayerMaskFromAvatarMask(1, faceAvatarMask);
        m_layerMixerPlayable.SetInputWeight(0, 1);

        animationOutputPlayable.SetSourcePlayable(m_layerMixerPlayable);
        m_graph.Play();
    }

    void Update() {
        //Blend的权重
        float leftWeight = directWeight < 0 ? -directWeight : 0;
        float rightWeight = directWeight > 0 ? directWeight : 0;
        float forwardWeight = 1 - leftWeight - rightWeight;
        m_mixerPlayable.SetInputWeight(0, leftWeight);
        m_mixerPlayable.SetInputWeight(1, forwardWeight);
        m_mixerPlayable.SetInputWeight(2, rightWeight);

        //面部动作的权重
        m_layerMixerPlayable.SetInputWeight(1, faceWeight);
    }

    void OnDestroy() {
        m_graph.Destroy();
    }
}
