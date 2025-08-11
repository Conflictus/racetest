using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public bool isRecording;
    private List<GhostData> recordedData = new List<GhostData>();
    private float recordInterval = 0.1f;
    private float timeSinceLastRecord;

    void Update()
    {
        if (isRecording)
        {
            timeSinceLastRecord += Time.deltaTime;
            if (timeSinceLastRecord >= recordInterval)
            {
                recordedData.Add(new GhostData
                {
                    position = transform.position,
                    rotation = transform.rotation,
                    time = Time.time
                });
                timeSinceLastRecord = 0;
            }
        }
    }

    public void StartRecording()
    {
        recordedData.Clear();
        isRecording = true;
    }

    public void StopRecording()
    {
        isRecording = false;
        GhostSaveSystem.SaveGhost(recordedData);
    }
}
