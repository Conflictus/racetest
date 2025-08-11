using UnityEngine;
using System.Collections.Generic;
public static class GhostSaveSystem
{
    private const string SAVE_KEY = "BestGhost";
    
    public static void SaveGhost(List<GhostData> data)
    {
        string json = JsonUtility.ToJson(new GhostDataWrapper(data));
        PlayerPrefs.SetString(SAVE_KEY, json);
    }
    
    public static List<GhostData> LoadGhost()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            GhostDataWrapper wrapper = JsonUtility.FromJson<GhostDataWrapper>(json);
            return wrapper.data;
        }
        return null;
    }
    
    [System.Serializable]
    private class GhostDataWrapper
    {
        public List<GhostData> data;
        public GhostDataWrapper(List<GhostData> data) => this.data = data;
    }
}
[System.Serializable]
public class GhostData
{
    public Vector3 position;
    public Quaternion rotation;
    public float time;
}