using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float turnSpeed = 102;
    [SerializeField] float moveSpeed = 102;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        Look();
        Move();
    }
    void Look()
    {
        LookLeftAndRight();
        LookUpAndDown();
    }
    void Move()
    {
        gameObject.transform.position += camera.transform.forward * Input.GetAxis("W/S") * Time.unscaledDeltaTime * moveSpeed;
        gameObject.transform.position += gameObject.transform.right * Input.GetAxis("A/D") * Time.unscaledDeltaTime * moveSpeed;
        gameObject.transform.position += gameObject.transform.up * Input.GetAxis("Jump") * Time.unscaledDeltaTime * moveSpeed;
    }
    void LookUpAndDown()
    {
        Vector3 CameraRotation = camera.transform.rotation.eulerAngles;

        Cursor.lockState = CursorLockMode.Locked;
        CameraRotation.x -= Input.GetAxis("Mouse Y");
        CameraRotation.x = ClampAngle(CameraRotation.x, -80, 80);
        camera.transform.rotation = Quaternion.Euler(CameraRotation);
    }
    void LookLeftAndRight()
    {
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + turnSpeed * Input.GetAxis("Mouse X") * Time.unscaledDeltaTime, gameObject.transform.eulerAngles.z);
    }
    float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
