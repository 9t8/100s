using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float sens { get; } = .5f;

    float camXRot { get; set; }
    Transform camTran { get; set; }

    Rigidbody rb { get; set; }
    GroundChecker gc { get; set; }

    void Start()
    {
        camXRot = 0;
        camTran = transform.Find("Main Camera").transform;

        rb = GetComponent<Rigidbody>();
        gc = transform.Find("GroundChecker").GetComponent<GroundChecker>();

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
        // can't use RigidBody.MovePosition because jitter
        Vector3 motion = transform.right * Input.GetAxisRaw("Horizontal")
                       + transform.forward * Input.GetAxisRaw("Vertical");
        if (motion != Vector3.zero)
        {
            transform.position += motion * 5 / motion.magnitude * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // jumping
        if (Input.GetButton("Jump") && gc.contacting != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 4, rb.velocity.z);
        }
    }
}
