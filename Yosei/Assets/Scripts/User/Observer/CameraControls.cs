using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {
    public float m_base_speed = 1f;
    public float m_speed_increment = 1f;
    public float m_max_speed_increment = 1f;
    public float m_mouse_sensitivity = 2f;
    public float m_mouse_wheel_bonus_speed_factor = 10f;

    public float m_current_speed;
    public float m_current_speed_increment;

    public bool m_inverted_vertical = true;
    public int m_mouse_button_look = 1;
    public int m_mouse_button_move = 2;

    private Vector3 m_rotation;

	void Awake ()
    {
        m_rotation = transform.rotation.eulerAngles;
	}
	
	void FixedUpdate ()
    {
        Screen.lockCursor = false;

        if (Input.GetMouseButton(m_mouse_button_move))
        {
            transform.position += GetDragMovement();
            Screen.lockCursor = true;
        }
        else
        {
            transform.position += GetNormalMovement();
        }

        if (Input.GetMouseButton(m_mouse_button_look))
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
        direction = direction * (m_base_speed * (1f + m_current_speed_increment));

        return direction;
    }

    private Vector3 GetNormalMovement()
    {
        Vector3 direction = Vector3.zero;

        direction += Input.GetAxis("Vertical") * Vector3.forward;
        direction += Input.GetAxis("Horizontal") * Vector3.right;

        direction += Input.GetAxis("Mouse ScrollWheel") * m_mouse_wheel_bonus_speed_factor * Vector3.forward;

        // Transforming direction from the camera rotation
        direction = transform.TransformDirection(direction);

        // This is absolute, not related to the camera's transform
        direction += Input.GetAxis("Altitude") * Vector3.up;

        // Holding means more speed
        if (direction == Vector3.zero)
        {
            m_current_speed_increment = 0f;
        }
        else
        {
            m_current_speed_increment = Mathf.Min(m_max_speed_increment, m_current_speed_increment + m_speed_increment);
        }

        // Applying total speed
        direction = direction * (m_base_speed * (1f + m_current_speed_increment));

        return direction;
    }

    public Vector3 GetRotation()
    {
        m_rotation += Vector3.up * m_mouse_sensitivity * Input.GetAxis("Mouse X");
        m_rotation += (m_inverted_vertical ? -1 : 1) * Vector3.right * m_mouse_sensitivity * Input.GetAxis("Mouse Y");

        return m_rotation;
    }
}
