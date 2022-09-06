using UnityEngine;

public class PlayerLimiter : MonoBehaviour
{
    [SerializeField] int bounds;

    private void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x) > bounds || Mathf.Abs(gameObject.transform.position.y) > bounds || Mathf.Abs(gameObject.transform.position.z) > bounds)
        {
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, -bounds, bounds), Mathf.Clamp(gameObject.transform.position.y, -bounds, bounds), Mathf.Clamp(gameObject.transform.position.z, -bounds, bounds));
        }
    }
}
