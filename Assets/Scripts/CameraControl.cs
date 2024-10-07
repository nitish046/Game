using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
  public class CameraControl : MonoBehaviour
  {
    public GameObject player;
    public MoveObject playerScript;
    public float camSpeed;

    float radius, azimuth, polar;

    float Rad = Mathf.PI / 180;
    float Deg = 180 / Mathf.PI;

    float rotationX, rotationY;

    Vector3 relPos;

    public float max;
    public float min;

    // Start is called before the first frame update
    void Start()
    {
      radius = 10;
      polar = Mathf.PI / 2;
      azimuth = 0;

      playerScript = player.GetComponent<MoveObject>();
    }

    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
      //float rotationX = -(Input.GetAxis("Mouse X") * Rad * camSpeed);
      //float rotationY = (Input.GetAxis("Mouse Y") * Rad * camSpeed);

      azimuth = playerScript.GetAzimuth() * Rad * -1;

      polar = polar + rotationY;

      if (polar > Mathf.PI * max)
        polar = Mathf.PI * max;
      if (polar < Mathf.PI * min)
        polar = Mathf.PI * min;

      float x = radius * Mathf.Cos(azimuth) * Mathf.Sin(polar);
      float z = radius * Mathf.Sin(azimuth) * Mathf.Sin(polar);
      float y = radius * Mathf.Cos(polar);

      relPos = new Vector3(x, y, z);

      Vector3 playerPos = player.GetComponent<CharacterController>().transform.position;

      transform.position = playerPos + relPos;

      transform.LookAt(playerPos);
    }

  }
}
