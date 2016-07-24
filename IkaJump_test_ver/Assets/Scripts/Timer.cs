using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    public int timeLimit = 30;

    public float timeRemaining = 0;

    bool timerStarted = false;


	void Start () {
	
	}
	
	void Update () {
	    if (timerStarted && timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
            Debug.Log(timeRemaining);
        }
	}

    public void StartTimer()
    {
        timerStarted = true;
        Debug.Log("Timer starts!!");
    }

    public void ResetTimer()
    {
        timeRemaining = timeLimit;
        timerStarted = false;
        Debug.Log("Timer is resetted!!");
    }

     public void StopTimer()
    {
        timerStarted = false;
        Debug.Log("Timer is stopped!!");
    }

    public float GetTime()
    {
        return timeRemaining;
    }
}
