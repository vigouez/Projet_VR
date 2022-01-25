using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointPush : MonoBehaviour
{
    HingeJoint hinge;
    JointMotor motor;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        hinge = gameObject.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        motor = new JointMotor();
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            motor.force = speed * Time.deltaTime;
            motor.targetVelocity = -100000f;
            hinge.motor = motor;
            hinge.useMotor = true; 
        }
        else if (Input.GetKey(KeyCode.KeypadMinus))
        {
            motor.force = speed * Time.deltaTime;
            motor.targetVelocity = 100000f;
            hinge.motor = motor;
            hinge.useMotor = true;
        }
        else
        {
            hinge.useMotor = false;
        }
    }
}
