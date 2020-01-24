using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Globalization;

public class PlayerMovement : MonoBehaviour
{   //Read info from serial port
    SerialPort sp = new SerialPort("/dev/tty.usbmodem1411", 9600);

    public float sensitivity;//turning sensitivity
    public Vector3 rotationOffset ;//offset values
    public GameObject capsule;

    void Start()
    {
      //open serial port
      sp.Open();
      sp.ReadTimeout = 1000;
    }

    // Update is called once per frame
    void Update()
    {


        //store mpu values in array
        string[] values = sp.ReadLine().Split('/');

        if (values.Length == 5)
        {
        //split array up and convert string to float
        float w = (float.Parse(values[1]));
        float x = float.Parse(values[2]);
        float y = float.Parse(values[3]);
        float z = float.Parse(values[4]);

        //take the inverse of quaternion values

        z = -z;

        //control camera based on mpu quaternion values
        Quaternion cam = new Quaternion  (y, x, z, w);
        Quaternion ang = Quaternion.Lerp(this.transform.localRotation, cam , Time.deltaTime * sensitivity);
        Quaternion camAng = this.transform.localRotation = ang ;


        }
        //rotate the parent object of the camera to create an offset
        capsule.transform.eulerAngles = rotationOffset;

  }
}
