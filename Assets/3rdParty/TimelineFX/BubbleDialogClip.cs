
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class BubbleDialogClip : PlayableAsset
{
    public string subtitleText;
    public AudioClip audioClip;
    public bool isLoopAudio;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BubbleDialogBehaviour>.Create(graph);
        BubbleDialogBehaviour bubleDialogBehaviour = playable.GetBehaviour();

        bubleDialogBehaviour.subtitleText = subtitleText;
        bubleDialogBehaviour.audioClip = audioClip;
        bubleDialogBehaviour.isLoopAudio = isLoopAudio;

        return playable;
    }

}

public class BubbleDialogBehaviour : PlayableBehaviour
{
    public string subtitleText;
    public AudioClip audioClip;
    public bool isLoopAudio;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

        BubbleDialog dialog = playerData as BubbleDialog;
        
        //visible
        int inputCount = playable.GetInputCount();
        bool hasInput = false;
        for (int i = 0; i < inputCount; i++)
        {
            if (playable.GetInputWeight(i) > 0)
            {
                hasInput = true;
                break;
            }
        }

        if (!hasInput)
        {
            //subtitle
            dialog.textUI.text = subtitleText;

            //audio

            //dialog.audioSource.time += info.deltaTime;
            //Debug.Log(dialog.audioSource.isPlaying);
            if (dialog.audioSource.isActiveAndEnabled && !dialog.audioSource.isPlaying && audioClip != null)
            {
                double time = playable.GetTime();
                dialog.audioSource.clip = audioClip;
                double audioStartTime = time % audioClip.length;
 
                if((isLoopAudio || time < audioClip.length) && info.seekOccurred)
                {
                    Debug.Log(time + " " + audioClip.length + " " + audioStartTime);
                    dialog.audioSource.time = (float)audioStartTime;
                    dialog.audioSource.Play();
                }
            }

            if(dialog.audioSource.isPlaying)
            { 
                return;
            }
        }

        dialog.gameObject.SetActive(hasInput);

    }
}

