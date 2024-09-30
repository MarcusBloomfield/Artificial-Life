using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] float bounds;
    MeshRenderer mRenderer;
    Vector3 lastPosition;
    private void Start()
    {
        lastPosition = transform.position;
        mRenderer = GetComponent<MeshRenderer>();
        InvokeRepeating("CheckDistance", 1, 1);
        InvokeRepeating("CullStatic", 10, .1f);
    }
    void CheckDistance()
    {
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) > bounds * 2)
        {
            gameObject.transform.position = new Vector3(Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2));
        }
    }
    void CullStatic()
    {
        if (mRenderer.enabled)
        {
            if (Vector3.Distance(lastPosition, gameObject.transform.position) < .1)
            {
                mRenderer.enabled = false;
                if (Vector3.Distance(gameObject.transform.position, Vector3.zero) > bounds * 2)
                {
                    gameObject.transform.position = new Vector3(Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2));
                }
            }
        }
        else
        {
            if (Vector3.Distance(lastPosition, gameObject.transform.position) > 1)
            {
                mRenderer.enabled = true;
                lastPosition = gameObject.transform.position;
            }
        }

    }
}
