using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Time.timeScale -= 1;
        }
    }
}
