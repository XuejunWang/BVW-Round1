using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController3 : WaveController2
{
    public override void ShowNextHighlight()
    {
        Debug.Log("ShowNextHighlight");
        //int nextShownIndex = 0;
        HighlightController currentHighlight = GetCurrentPunchHighlight();
        if (currentHighlight)
        {
            currentHighlight.SetCurrentPunch(false);
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
            nextHighlight.SetCurrentPunch(true);
        }
        else
        {
            bossController.SetTextureIndex(0);
            bossController.NextWave();
        }
    }

    public override void CheckHighlight(GameObject highlight)
    {
        HighlightController currentHighlight = GetCurrentPunchHighlight();
        Debug.Log("CheckHighlight " + currentHighlight);
        if (currentHighlight && highlight == currentHighlight.gameObject && !currentHighlight.bPunched)
        {
            currentHighlight.bPunched = true;
            bossController.SetTextureIndex(0);
            bossAnimator.SetTrigger("hurt");
            hurtAudio.Play();
            //ShowNextHighlight();
            currentHighlight.PlayParticles();
        }
    }

    public override void ResetCurrentPunch()
    {
        bossController.SetTextureIndex(0);
        ////HighlightController highlightController = GetHighlight(0);
        ////highlightController.SetCurrentPunch(true);
        //for (int i = 0; i < arrHighlight.Length; i++)
        //{
        //    HighlightController highlight = arrHighlight[i];
        //    highlight.bPunched = false;
        //}
        bossAnimator.SetTrigger("attack");
        attackAudio.Play();
        //StartWave();
        StartCoroutine(ShowPreviousHighlight());
    }

    IEnumerator ShowPreviousHighlight()
    {
        yield return new WaitForSeconds(1);

        HighlightController nextHighlight = GetHighlight(nextShownIndex);
        //int index = System.Array.IndexOf(arrHighlight, nextHighlight);
        int index = nextHighlight.index;
        bossController.SetTextureIndex(index + 1);
        nextHighlight.SetCurrentPunch(true);
    }
}
