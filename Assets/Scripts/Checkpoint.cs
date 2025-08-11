using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public delegate void CheckpointEvent(Checkpoint checkpoint);
    public event CheckpointEvent OnCheckpointPassed;

    [SerializeField] private MeshRenderer meshRenderer; 
    [SerializeField] private Collider triggerCollider; 
    private void Awake()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
        
        if (triggerCollider == null)
            triggerCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCheckpointPassed?.Invoke(this);
        }
    }

    public void SetVisible(bool visible)
    {
        meshRenderer.enabled = visible;
        triggerCollider.enabled = visible;
    }
}
