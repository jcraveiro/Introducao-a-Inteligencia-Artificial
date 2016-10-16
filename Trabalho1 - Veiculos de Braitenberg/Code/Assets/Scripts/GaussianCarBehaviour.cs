using UnityEngine;
using System.Collections;

public class GaussianCarBehaviour : CarBehaviour {

	void Update()
	{
		//Read sensor values
		float leftSensor = LeftLD.getGaussianOutput ();
		float rightSensor = RightLD.getGaussianOutput ();

		//Calculate target motor values
		m_LeftWheelSpeed  = (rightSensor * MaxSpeed) * 5;
		m_RightWheelSpeed = (leftSensor  * MaxSpeed) * 5;

	}
}
