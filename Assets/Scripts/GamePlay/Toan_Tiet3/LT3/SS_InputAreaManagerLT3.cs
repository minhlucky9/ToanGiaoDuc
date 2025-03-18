using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_InputAreaManagerLT3 : MonoBehaviour
{
    public List<SS_InputAreaLT3> inputAreas;
    public List<Text> InputTexts;

    List<int> numberSequence = new List<int>();

    public void setNumberSequence(List<int> sequence)
    {
        numberSequence.AddRange(sequence);
        setUpInputAreaState();
    }

    void setUpInputAreaState()
    {
        for (int i = 0; i < numberSequence.Count - 1; i++)
        {
            int fNum = numberSequence[i];
            int sNum = numberSequence[i + 1];

            switch (fNum.CompareTo(sNum))
            {
                case 1:         //fNum bigger than sNum
                    inputAreas[i].changeState(SS_InputAreaLT3.States.Bigger);
                    break;
                case 0:         //fNum equal sNum
                    inputAreas[i].changeState(SS_InputAreaLT3.States.Equal);
                    break;
                case -1:        //Fnum smaller than sNum
                    inputAreas[i].changeState(SS_InputAreaLT3.States.Smaller);
                    break;
            }
        }
    }

    public List<SS_InputAreaLT3> GetList()
    {
        return inputAreas;
    }
}
