using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float totalSeconds;
    private float elapsedSeconds;
    private bool running;
    private int previousCountdownValue;
    private bool started;
    private bool isStopped;

    public bool Running => running;
    public bool IsStopped { get => isStopped; set { isStopped = value; } }

    public float Duration
    {
		set
        {
			if (!running)
            {
				totalSeconds = value;
			}
		}
	}

	public bool Finished
    {
		get { return started && !running; } 
	}

    void Update()
    {
		if (running)
        {
			elapsedSeconds += Time.deltaTime;

			if (elapsedSeconds >= totalSeconds)
            {
				running = false;
				
			}
		}
	}
	
	public void Run()
    {
		if (totalSeconds > 0)
        {
			started = true;
			running = true;
            isStopped = false;
			elapsedSeconds = 0;
		}
	}

	public void Stop()
    {
		started = false;
		running = false;
        isStopped = true;
	}
}
