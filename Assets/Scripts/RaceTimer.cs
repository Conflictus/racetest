using UnityEngine;
using TMPro;
public class RaceTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    
    private float _currentTime;
    private bool _isRacing;
    
    // Свойство для доступа к текущему времени с возможностью контроля
    public float CurrentTime
    {
        get => _currentTime;
        private set
        {
            _currentTime = value;
            UpdateTimerDisplay(); // Обновляем UI при изменении времени
        }
    }

    public bool IsRacing
    {
        get => _isRacing;
        private set => _isRacing = value;
    }

    private void Update()
    {
        if (IsRacing)
        {
            CurrentTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        CurrentTime = 0f;
        IsRacing = true;
    }

    public void StopTimer()
    {
        IsRacing = false;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(CurrentTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        int milliseconds = (int)(time * 100) % 100;
        return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
