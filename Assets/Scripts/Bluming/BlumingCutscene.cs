using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlumingCutscene : MonoBehaviour
{
    public Animator Bluming;
    public float cutsceneDuration = 2f;

    private bool cutscenePlaying = true;

    void Start()
    {
        if (Bluming != null)
        {
            Bluming.SetTrigger("PlayCutscene");
        }

        StartCoroutine(EndCutsceneAfterDelay());
    }

    IEnumerator EndCutsceneAfterDelay()
    {
        yield return new WaitForSeconds(cutsceneDuration);

        EndCutscene();
    }

    void EndCutscene()
    {
        if (!Bluming) return;

        cutscenePlaying = false;

        if (Bluming != null)
        {
            Bluming.SetTrigger("EndCutscene");
        }

        Debug.Log("ÄÆ¾ÀÀÌ Á¾·áµÇ¾ú½À´Ï´Ù.");
    }
}