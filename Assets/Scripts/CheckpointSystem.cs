using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class CheckpointSystem : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    private int nextCheckpointIndex;
    public int lapsCompleted { get; private set; }
    public int totalLaps = 1;
    public int checkpointsPassed { get; private set; } // Счетчик пройденных чекпоинтов

    
    [SerializeField] TMP_Text laps;
    private void Start()
    {
        checkpointsPassed = 0;
        nextCheckpointIndex = 0;
        RaceManager.Instance.OnRaceStarted += SetCheckpoints;

        foreach (var checkpoint in checkpoints)
        {
            checkpoint.OnCheckpointPassed += HandleCheckpointPassed;
            checkpoint.SetVisible(true); // Делаем все чекпоинты видимыми при старте
        }
    }
    void SetCheckpoints() {
        
        checkpointsPassed = 0;
        nextCheckpointIndex = 0;
        lapsCompleted = 0;
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.OnCheckpointPassed += HandleCheckpointPassed;
            checkpoint.SetVisible(true);
        }
    }
    private void OnDestroy()
    {
        if (RaceManager.Instance != null)
        {
            RaceManager.Instance.OnRaceStarted -= SetCheckpoints;
        }
    }
    void Update()
    {
        laps.text = $"{lapsCompleted}/{totalLaps}";
    }
    private void HandleCheckpointPassed(Checkpoint checkpoint)
    {
        if (checkpoints.IndexOf(checkpoint) == nextCheckpointIndex)
        {
            // Делаем чекпоинт невидимым
            checkpoint.SetVisible(false);
            
            // Увеличиваем счетчики
            checkpointsPassed++;
            nextCheckpointIndex = (nextCheckpointIndex + 1) % checkpoints.Count;
            
            if (nextCheckpointIndex == 0)
            {
                
                lapsCompleted++;
                if (lapsCompleted >= totalLaps)
                {
                    RaceManager.Instance.FinishRace();
                }
                else
                {
                    // При новом круге делаем все чекпоинты снова видимыми
                    foreach (var cp in checkpoints)
                    {
                        cp.SetVisible(true);
                    }
                }
            }
        }
    }

    public void ResetCheckpoints()
    {
        checkpointsPassed = 0;
        nextCheckpointIndex = 0;
        lapsCompleted = 0;
        
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.SetVisible(true);
        }
    }
}