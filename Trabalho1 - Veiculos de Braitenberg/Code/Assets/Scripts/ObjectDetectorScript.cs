using UnityEngine;
using System.Collections;

public class ObjectDetector : MonoBehaviour
{

	// Use this for initialization
	public float angle;
	public float distance;
	public bool output;

	void Start()
	{
		angle = 60;
		distance = 1;
		output = false;
	}

	// Update is called once per frame
	void Update()
	{
		GetSensorValue();
	}


	// Get Sensor output value
	public bool getOutput()
	{
		return output;
	}

	public void GetSensorValue()
	{
		ArrayList visibleObjects = new ArrayList();
		float halfAngle = angle / 2.0f;
		float minDistance;

		GameObject[] cubes = GameObject.FindGameObjectsWithTag("Block");
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");

		foreach (GameObject cube in cubes)
		{
			if (Vector3.Distance(transform.position, cube.transform.position) <= distance)
			{
				Vector3 toVector = (cube.transform.position - transform.position);
				Vector3 forward = transform.forward;
				toVector.y = 0;
				forward.y = 0;
				float angleToTarget = Vector3.Angle(forward, toVector);

				if (angleToTarget <= halfAngle)
				{
					visibleObjects.Add(cube);
				}
			}
		}
		foreach (GameObject wall in walls)
		{
			if (Vector3.Distance(transform.position, wall.transform.position) <= distance)
			{
				Vector3 toVector = (wall.transform.position - transform.position);
				Vector3 forward = transform.forward;
				toVector.y = 0;
				forward.y = 0;
				float angleToTarget = Vector3.Angle(forward, toVector);

				if (angleToTarget <= halfAngle)
				{
					Debug.Log(transform.position);
					Debug.Log(wall.transform.position);
					visibleObjects.Add(wall);
				}
			}
		}

		if(visibleObjects.Count==0)
		{
			output = false;
		}
		else
		{
			output = true;
		}

	}

}
