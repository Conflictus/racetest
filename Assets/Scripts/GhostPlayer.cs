using UnityEngine;
using System.Collections.Generic;
public class GhostPlayer : MonoBehaviour
{
    private List<GhostData> ghostData;
    private int currentIndex;
    private float playbackStartTime;
    public bool isPlaying;

    public void LoadAndPlayGhost()
    {
        ghostData = GhostSaveSystem.LoadGhost();
        if (ghostData != null && ghostData.Count > 0)
        {
            currentIndex = 0;
            playbackStartTime = Time.time;
            isPlaying = true;
            transform.position = ghostData[0].position;
            transform.rotation = ghostData[0].rotation;
        }
    }

    void Update()
    {
        if (!isPlaying) return;

        float currentTime = Time.time - playbackStartTime;
        
        // Находим ближайшие точки для интерполяции
        while (currentIndex < ghostData.Count - 1 && ghostData[currentIndex + 1].time < currentTime)
        {
            currentIndex++;
        }

        if (currentIndex < ghostData.Count - 1)
        {
            float t = (currentTime - ghostData[currentIndex].time) / 
                     (ghostData[currentIndex + 1].time - ghostData[currentIndex].time);
            
            transform.position = Vector3.Lerp(
                ghostData[currentIndex].position,
                ghostData[currentIndex + 1].position,
                t);
                
            transform.rotation = Quaternion.Lerp(
                ghostData[currentIndex].rotation,
                ghostData[currentIndex + 1].rotation,
                t);
        }
        else
        {
            isPlaying = false;
        }
    }
}

