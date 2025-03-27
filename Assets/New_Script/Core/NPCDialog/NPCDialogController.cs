using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPC { GiaoVien, Hai, Lan };
public class NPCDialogController : MonoBehaviour
{
    public NPCDialogAnimation GiaovienAnimation;
    public NPCDialogAnimation HaiAnimation;
    public NPCDialogAnimation LanAnimation;
    //
    List<DialogData> dialog;

    private void Awake()
    {
        //
        GameSceneManager.instance.onStepStart += (stepData) =>
        {
            RunGuideDialog(stepData.guideDialogs);
        };
    }

    public void RunGuideDialog(List<DialogData> dialogDatas)
    {
        dialog = dialogDatas;
        StartCoroutine(RunAllNPCDialog());
    }

    IEnumerator RunAllNPCDialog()
    {
        for(int i = 0; i < dialog.Count; i ++)
        {
            DialogData currentDialog = dialog[i];
            NPCDialogAnimation dialogAnimation = GetNPCDialogAnimation(currentDialog.npc);
            
            yield return dialogAnimation.GuideTalkAnimation(currentDialog.audioClip, currentDialog.dialogText);
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    public NPCDialogAnimation GetNPCDialogAnimation(NPC npc)
    {
        switch(npc)
        {
            case NPC.GiaoVien:
                return GiaovienAnimation;
                
            case NPC.Hai:
                return HaiAnimation;
                
            case NPC.Lan:
                return LanAnimation;
                
        }

        return null;
            
    }
}

[Serializable]
public struct DialogData
{
    public AudioClip audioClip;
    public string dialogText;
    public NPC npc;
}