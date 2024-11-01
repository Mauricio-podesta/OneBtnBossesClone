using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VIctoryScene : MonoBehaviour
{
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI newRecordText;

    private float totalTime;
    private float bestTime;

    void Start()
    {
        newRecordText.gameObject.SetActive(false);
    }

    public void ShowVictoryScreen(float time)
    {
        totalTime = time;
        totalTimeText.text = FormatTime(totalTime);
        CheckBestTime();
    }

    private void CheckBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (totalTime < bestTime)
        {
            bestTime = totalTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            newRecordText.gameObject.SetActive(true);
        }

        bestTimeText.text = "Best Time: " + FormatTime(bestTime);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int centiseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:0}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }
}
