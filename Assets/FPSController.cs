using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    float sens { get; } = .5f;

    float camXRot { get; set; }
    Transform camTran { get; set; }

    CharacterController cc { get; set; }
    float yVel { get; set; }

    void Start()
    {
        camXRot = 0;
        camTran = transform.Find("Main Camera").transform;

        cc = GetComponent<CharacterController>();
        yVel = 0;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // mouse input seems to have to be here to be immune to
        // changes in framerate

        // movement happens here or else jitter
        // should refactor to FixedUpdate and manually extrap here

        // horizontal looking
        float mouseX = Input.GetAxisRaw("Mouse X") * sens;
        transform.Rotate(0, mouseX, 0);

        // vertical looking
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens;
        camXRot = Mathf.Clamp(camXRot - mouseY, -90, 90);
        camTran.localRotation = Quaternion.Euler(camXRot, 0, 0);

        // horizontal movement
        Vector3 motion = transform.right * Input.GetAxisRaw("Horizontal")
                       + transform.forward * Input.GetAxisRaw("Vertical");
        if (motion != Vector3.zero)
        {
            motion *= 5 / motion.magnitude;
        }

        // jumping + gravity
        if (cc.isGrounded)
        {
            if (Input.GetButton("Jump"))
            {
                yVel = 4;
            }
            else
            {
                yVel = 0;
            }
        }
        else
        {
            yVel += Physics.gravity.y * Time.deltaTime;
        }
        motion.y = yVel;

        cc.Move(motion * Time.deltaTime);
    }
}
