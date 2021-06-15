using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationState
{
    public string animationName;
    public AnimationClip animationClip;
}

[CreateAssetMenu(menuName = "ScriptableObject/ChanAnimation")]
public class ChanAnimationSO : ScriptableObject, IListToDict
{
    public List<AnimationState> animationStateList;
    public Dictionary<string, AnimationState> animationStateDict;

    public void ListToDict() {
        if(animationStateDict != null)
            return;
        foreach(var state in animationStateList)
            animationStateDict[state.animationName] = state;
    }

    public AnimationState Get(string animationName) {
        if(animationStateDict.ContainsKey(animationName))
            return animationStateDict[animationName];
        Debug.LogError($"animationName:{animationName} not found");
        return new AnimationState();
    }
}
