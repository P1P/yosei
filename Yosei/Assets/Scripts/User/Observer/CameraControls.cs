using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
    // Inspector-set values
    public float Base_speed = 0.5f;
    public float Speed_increment = 0.01f;
    public float Max_speed_increment = 1f;
    public float Mouse_sensitivity = 5f;
    public float Mouse_wheel_bonus_speed_factor = 5f;

    public bool Inverted_vertical = true;
    public int Mouse_button_look = 1;
    public int Mouse_button_move = 2;

    private float _current_speed;
    private float _current_speed_increment;
    private Vector3 _rotation;

	void Awake ()
    {
        _rotation = transform.rotation.eulerAngles;
	}
	
	void FixedUpdate ()
    {
        Screen.lockCursor = false;

        if (Input.GetMouseButton(Mouse_button_move))
        {
            transform.position += GetDragMovement();
            Screen.lockCursor = true;
        }
        else
        {
            transform.position += GetNormalMovement();
        }

        if (Input.GetMouseButton(Mouse_button_look))
        {
            transform.rotation = Quaternion.Euler(GetRotation());
            Screen.lockCursor = true;
        }
	}

    private Vector3 GetDragMovement()
    {
        Vector3 direction = Vector3.zero;

        direction += -Input.GetAxis("Mouse X") * Vector3.right;
        direction += -Input.GetAxis("Mouse Y") * Vector3.forward;

        // Transforming direction from the camera rotation
        direction = transform.TransformDirection(direction);

        direction = new Vector3(direction.x, 0f, direction.z);

        // Applying total speed
        direction = direction * (Base_speed * (1f + _current_speed_increment));

        return direction;
    }

    private Vector3 GetNormalMovement()
    {
        Vector3 direction = Vector3.zero;

        direction += Input.GetAxis("Vertical") * Vector3.forward;
        direction += Input.GetAxis("Horizontal") * Vector3.right;

        direction += Input.GetAxis("Mouse ScrollWheel") * Mouse_wheel_bonus_speed_factor * Vector3.forward;

        // Transforming direction from the camera rotation
        direction = transform.TransformDirection(direction);

        // This is absolute, not related to the camera's transform
        direction += Input.GetAxis("Altitude") * Vector3.up;

        // Holding means more speed
        if (direction == Vector3.zero)
        {
            _current_speed_increment = 0f;
        }
        else
        {
            _current_speed_increment = Mathf.Min(Max_speed_increment, _current_speed_increment + Speed_increment);
        }

        // Applying total speed
        direction = direction * (Base_speed * (1f + _current_speed_increment));

        return direction;
    }

    public Vector3 GetRotation()
    {
        _rotation += Vector3.up * Mouse_sensitivity * Input.GetAxis("Mouse X");
        _rotation += (Inverted_vertical ? -1 : 1) * Vector3.right * Mouse_sensitivity * Input.GetAxis("Mouse Y");

        return _rotation;
    }
}
