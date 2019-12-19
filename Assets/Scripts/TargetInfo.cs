using EnliStandardAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetInfo : MonoBehaviour
{
    public Text infoTitleText;
    public Text infoMessageText;
    public AnimatorParamSet panelAnimation;
    public EasyTween infoButtonAnimation;
    public List<InfoPair> infoPairs;

    private InfoPair activeInfo;

    void Start()
    {
        for (int i = 0; i < infoPairs.Count; i++)
        {
            int index = i;
            infoPairs[i].target.TargetFound += (x) => Target_TargetFound(index);
            infoPairs[i].target.TargetLost += (x) => Target_TargetLost(index);
        }

        infoButtonAnimation.gameObject.SetActive(false);
        //SetInfo(0);
    }

    public void ShowHint(bool value)
    {
        panelAnimation.SetAnimatorValue(value);
        infoButtonAnimation.OpenCloseObjectAnimation(!value);
    }

    private void Target_TargetFound(int index)
    {
        SetInfo(index);
    }

    private void Target_TargetLost(int index)
    {
    }

    private void SetInfo(int index)
    {
        activeInfo = infoPairs[index];
        infoTitleText.text = activeInfo.infoTitle;
        infoMessageText.text = activeInfo.infoMessage;
        infoButtonAnimation.gameObject.SetActive(true);
    }

    private IEnumerator DelayAction(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

    public void GoToExternalSource()
    {
        if (activeInfo != null)
            Application.OpenURL(activeInfo.link);
    }


    [Serializable]
    public class InfoPair
    {
        public ImageTargetExtended target;
        public string infoTitle;
        public string infoMessage;
        public string link;
    }
}
