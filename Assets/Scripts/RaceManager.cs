using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class RaceManager : MonoBehaviour
{
    public Transform startPosition;
    public static RaceManager Instance;
    public delegate void RaceEvent();
    public event RaceEvent OnRaceStarted;
    public GhostRecorder ghostRecorder;
    public GhostPlayer ghostPlayer;
    public RaceTimer raceTimer;

    public Button button;
    private bool raceStarted;
    private bool ghostLoaded;

    public bool IsFinished { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void StartRace()
    {
        raceStarted = true;
        raceTimer.StartTimer();
        ghostRecorder.StartRecording();
        button.gameObject.SetActive(false);
        OnRaceStarted?.Invoke();
    }

    public void FinishRace()
    {
        raceTimer.StopTimer();
        ghostRecorder.StopRecording();
        raceStarted = false;
        IsFinished = false;
        button.gameObject.SetActive(true);

    }

    public void ToggleGhost()
    {
        if (ghostLoaded)
        {
            ghostPlayer.isPlaying = !ghostPlayer.isPlaying;
        }
        else
        {
            ghostPlayer.LoadAndPlayGhost();
            ghostLoaded = true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !raceStarted && !IsFinished)
        {
            StartRace();
        }
    }
    public void RestartWithGhost()
    {
        ghostPlayer.LoadAndPlayGhost();
        ghostRecorder.gameObject.transform.position = startPosition.position;
    }
}
