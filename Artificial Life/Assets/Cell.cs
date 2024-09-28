using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Rigidbody body;
    public float distance;
    public float effectRange;
    public Cell target;
    public float force;
    [SerializeField] float bounds;
    Vector3 lastPosition;
    MeshRenderer mRenderer;
    private void Start()
    {
        lastPosition = transform.position;
        mRenderer = GetComponent<MeshRenderer>();
        InvokeRepeating("CheckDistance", 1, 1);
        InvokeRepeating("CullStatic", 10, .1f);
    }
    void CheckDistance()
    {
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) > bounds)
        {
            gameObject.transform.position = -gameObject.transform.position;
        }
    }
    void CullStatic()
    {
        if (mRenderer.enabled)
        {
            if (Vector3.Distance(lastPosition, gameObject.transform.position) < .25)
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
    public void Move()
    {
        if (distance > 0 && distance < effectRange)
        {
           gameObject.transform.LookAt(target.transform.position);
           body.velocity += gameObject.transform.forward * force;
        }
    }
}
