using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hints : MonoBehaviour
{
    public Image hintImage;
    public Toggle hintToggle;
    public float delayTargetFound = 0.5f;
    public EasyTween[] hintAnimations;
    public List<HintPair> hintPairs;

    private Coroutine delayCoroutine;
    private int currentHintIndex = -1;

    void Start()
    {
        for (int i = 0; i < hintPairs.Count; i++)
        {
            int index = i;
            hintPairs[i].target.TargetFound += (x) => Target_TargetFound(index);
            hintPairs[i].target.TargetLost += (x) => Target_TargetLost(index);
        }

        currentHintIndex = 0;
        SetHint(currentHintIndex);
    }

    public void ShowHint(bool value)
    {
        hintToggle.isOn = value;
    }

    public void HintToggleValueChanged(bool value)
    {
        if (value)
        {
            SetHint(currentHintIndex);
        }

        foreach (var item in hintAnimations)
        {
            item.OpenCloseObjectAnimation(value);
        }
    }

    private void Target_TargetFound(int index)
    {
        if (delayCoroutine != null)
            StopCoroutine(delayCoroutine);

        StartCoroutine(DelayAction(delayTargetFound,
            delegate
            {
                currentHintIndex = index + 1;
                if (currentHintIndex >= hintPairs.Count)
                {
                    currentHintIndex = 0;
                    hintToggle.gameObject.SetActive(false);
                }

                Debug.Log("Target_TargetFound, index: " + index + ", hintujem index: " + currentHintIndex);

                ShowHint(false);
            }));
    }

    private void Target_TargetLost(int index)
    {
        if (delayCoroutine != null)
            StopCoroutine(delayCoroutine);
        delayCoroutine = null;
    }

    private void SetHint(int index)
    {
        if (index >= 0 && index < hintPairs.Count)
        {
            hintImage.sprite = hintPairs[index].hintImage;
        }
        else
        {
            hintImage.sprite = null;
        }
    }

    private IEnumerator DelayAction(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }
}

[System.Serializable]
public class HintPair
{
    public ImageTargetExtended target;
    public Sprite hintImage;
}
