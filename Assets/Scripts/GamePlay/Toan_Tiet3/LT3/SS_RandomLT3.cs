using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SS_RandomLT3 : MonoBehaviour
{
    public List<Transform> Spawpoints;
    public List<Text> spawnpointsNumber;
    public List<Sprite> possibleSprites;
    public float waitTimeSound = 2.25f;
    public bool fixedIncre;
    public bool fixedDecre;
    private int randomNumber = 0;
    [HideInInspector] public List<int> numberSequence = new List<int>();

    private void Start()
    {
        run();
        if (fixedDecre) { randomNumber = 11; }
    }

    void run()
    {
        for(int i = 0; i < Spawpoints.Count; i++)
        {
            if (!fixedIncre && !fixedDecre)
            {
                randomNumber = Random.Range(0, possibleSprites.Count) + 1;
            }
            else if (fixedIncre)
            {
                randomNumber++;
            }
            else if (fixedDecre)
            {
                randomNumber--;
            }
            numberSequence.Add(randomNumber);

            Spawpoints[i].GetComponent<SpriteRenderer>().sprite = possibleSprites[randomNumber - 1];
            spawnpointsNumber[i].text = randomNumber.ToString();
        }

        GetComponentInChildren<SS_InputAreaManagerLT3>().setNumberSequence(numberSequence);
    }

}
