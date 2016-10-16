using UnityEngine;
using System.Collections;

public class CarBehaviour2a : CarBehaviour {
	
	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.getLinearOutput ();
		float rightSensor = RightLD.getLinearOutput ();

        //Calculate target motor values
		m_LeftWheelSpeed = (rightSensor  * MaxSpeed) * 7;
		m_RightWheelSpeed = (leftSensor * MaxSpeed) * 7;

	}
}
