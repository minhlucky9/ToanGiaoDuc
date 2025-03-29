using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(BubbleDialog))]
[TrackClipType(typeof(BubbleDialogClip))]
public class BubbleDialogTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
		return ScriptPlayable<BubbleDialogBehaviour>.Create(graph, inputCount);
    }
}