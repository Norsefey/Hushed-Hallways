using UnityEngine;
public class FPSController : MonoBehaviour
{
    // VARIABLES
    private Rigidbody rb; // player's rigidbody
    private Camera playerCamera; // player's camera
    [SerializeField] float moveSpeed = 5f; // speed of the player's movement
    [SerializeField] float maxVelocityChange = 10f; // max velocity change of the player's movement
    [SerializeField] float mouseSensitivity = 2f; // sensitivity of the player's camera
    [SerializeField] float maxLookAngle = 50f; // max look angle of the player's camera
    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // AWAKE AND INITIALIZATION
    private void Awake() { // grab player's rigidbody and camera
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor to the center of the screen
    }
    private void Update() // camera movement
    {
        MoveCamera();
    }
    private void FixedUpdate() // consistent phsyics calculation of movement
    {
        MoveCharacter();
    }
    private void MoveCamera() // move the camera based on mouse input
    {
        yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity; // calculate yaw by mouse x
        pitch -= mouseSensitivity * Input.GetAxis("Mouse Y"); // calculate pitch by mouse y
        pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle); // clamp the pitch to the max look angle
        transform.localEulerAngles = new Vector3(0, yaw, 0); // apply yaw to the player
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0); // apply pitch to the camera
    }
    private void MoveCharacter()
    {
        // Calculate velocity instead of movement quantity
        Vector3 targetVelocity = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity) * moveSpeed; // transform the velocity to world space by move speed
        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity; // get the player's current velocity
        Vector3 velocityChange = (targetVelocity - velocity); // calculate the velocity change
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange); // clamp the x velocity change to the max velocity change
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange); // clamp the z velocity change to the max velocity change
        velocityChange.y = 0; // set the y velocity change to 0
        rb.AddForce(velocityChange, ForceMode.VelocityChange); // add the velocity change to the player
    }
}