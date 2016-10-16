using UnityEngine;
using System.Collections;

public class CarBehaviour3 : CarBehaviour {

	public ObjectDetector northOD;
	public ObjectDetector southOD;
	public ObjectDetector eastOD;
	public ObjectDetector westOD;

	void Update()
	{
		//diz se há objectos ou nao
		bool northSensor = northOD.getOutput();
		bool southSensor = southOD.getOutput();
		bool eastSensor = eastOD.getOutput();
		bool westSensor = westOD.getOutput();


		if(westSensor && !northSensor)
		{
			//ir para norte


		}
		else if(southSensor && !westSensor)
		{
			//ir para oeste

		}
		else if(eastSensor && !southSensor)
		{
			//ir para sul
		}
		else if(northSensor && !eastSensor)
		{
			//ir para este
			m_LeftWheelSpeed = m_LeftWheelSpeed + 20;
		}
		else
		{
			m_LeftWheelSpeed = Mathf.Abs(m_LeftWheelSpeed);
			m_RightWheelSpeed = Mathf.Abs(m_RightWheelSpeed);

		}

	}
}
