using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] float bounds;
    private void Start()
    {
        InvokeRepeating("CheckDistance", 1, 1);
    }
    void CheckDistance()
    {
        if (Vector3.Distance(gameObject.transform.position, Vector3.zero) > bounds * 2)
        {
            gameObject.transform.position = new Vector3(Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2), Random.Range(-bounds / 2, bounds / 2));
        }
    }
}
