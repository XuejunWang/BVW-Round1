using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController2 : WaveController {

    protected int nextShownIndex;

    protected override void StartWave()
    {
        Debug.Log("WaveController2.StartWave");
        bPunch = true;
        nextShownIndex = 0;
        StartCoroutine(DelayStartWave());
    }

    IEnumerator DelayStartWave()
    {
        yield return new WaitForSeconds(startWait);

        ShowNextHighlight();
    }

    public override void ShowNextHighlight()
    {
        //int nextShownIndex = 0;
        HighlightController currentHighlight = GetCurrentShownHighlight();
        if (currentHighlight)
        {
            currentHighlight.bCurrentShown = false;
            //nextShownIndex = currentHighlight.index + 1;
            nextShownIndex++;
        }
        if (nextShownIndex < arrAppearIndex.Length)
        {
            Debug.Log("nextShownIndex " + nextShownIndex);
            HighlightController nextHighlight = GetHighlight(nextShownIndex);
            //int index = System.Array.IndexOf(arrHighlight, nextHighlight);
            int index = nextHighlight.index;
            bossController.SetTextureIndex(index + 1);
            nextHighlight.bCurrentShown = true;
        }
        else
        {
            bossController.SetTextureIndex(0);
            bossController.NextWave();
        }
    }

    public override void CheckHighlight(GameObject highlight)
    {
        HighlightController currentHighlight = GetCurrentShownHighlight();
        Debug.Log("CheckHighlight " + currentHighlight);
        if (highlight == currentHighlight.gameObject && !currentHighlight.bPunched)
        {
            currentHighlight.bPunched = true;
            bossAnimator.SetTrigger("hurt");
            hurtAudio.Play();
            ShowNextHighlight();
            currentHighlight.PlayParticles();
        }
    }
}
