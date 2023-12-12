using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float mouseHorizontalSpeed = 4f;
    [SerializeField] private float mouseVerticalSpeed = 4f;
    // Start is called before the first frame update

    private float rotateX;
    private float rotateY;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotateX -= mouseVerticalSpeed * Input.GetAxis("Mouse Y");
        rotateY += mouseHorizontalSpeed * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(rotateX, rotateY, 0.0f);
    }
}
