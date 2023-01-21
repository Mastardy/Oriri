using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private float duration;
    
    private float ratio;
    
    public float Move()
    {
        ratio += Time.deltaTime / duration;
        
        transform.position = Vector3.Lerp(startPosition, endPosition, ratio);

        if (ratio < 1) return Time.deltaTime;
        else return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(startPosition, 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endPosition, 0.3f);
        Gizmos.color = Color.white;
    }
}
