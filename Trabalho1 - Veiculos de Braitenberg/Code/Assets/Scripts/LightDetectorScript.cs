using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class LightDetectorScript : MonoBehaviour {

	public float angle;

	public float output;
	public int numObjects;

	public float limiteDown;
	public float limiteUp;
	public float limiarUp;
	public float limiarDown;
	public float bias;
	public double mean;
	public double stdev;

	void Start () {
		output = 0;
		numObjects = 0;
	}

	void Update () {
		GameObject[] lights = GetVisibleLights ();

		output = 0;
		numObjects = lights.Length;

		foreach (GameObject light in lights) {
			float r = light.GetComponent<Light> ().range;
			Debug.DrawLine(transform.position, light.transform.position,Color.green);
			output += 1f / Mathf.Pow((transform.position - light.transform.position).magnitude / r + 1, 2);
		}

		/*if(numObjects>0){
			output = output/numObjects;
		}*/
	}


		// Get Sensor output value
		public float getLinearOutput(){
			float strength, sensor_output;
			strength = output;
			sensor_output = output; // fiz assim estas duas linhas porque enquanto na gaussiana transformamos o output nesta y(x) = x e assim mantém se o modelo do outro

			//se forem a -1 os limites e limiares quer dizer que nao há
			if (limiarDown != -1 && limiarUp != -1) {
				if (strength <= limiarDown) {
					if (limiteDown != -1) {
						sensor_output= limiteDown;
					} else {
						sensor_output = 0;
					}
				}

				if (strength >= limiarUp) {
					if (limiteDown != -1) {
						sensor_output = limiteDown;
					} else {
						sensor_output = 0;
					}
				}
			}

			//mesma cena que em cima, quase x)
			if (limiteDown != -1 && limiteUp != -1) {
				if (sensor_output <= limiteDown) {
					sensor_output = limiteDown;
				}

				if (sensor_output >= limiteUp) {
					sensor_output = limiteUp;
				}
			}

			sensor_output = sensor_output * bias;
			return sensor_output;
		}

		// Get Gaussian Sensor output value
		//fiz isto de acordo com os grafico e com o x ser a strength e o y o sensor output que é o que está nos graficos
		public float getGaussianOutput() {
			double strength,sensor_output;
			strength = output;
			sensor_output = Math.Exp (-0.5 * (Math.Pow ((strength - mean) / stdev, 2))) / (stdev * Math.Sqrt (2 * Math.PI));
			//sensor_output = a * Math.Exp (-(Math.Pow ((strength - mean), 2.0d) / (2 * Math.Pow (stdev, 2.0d))));
			
			//if (output > 0.5f) output = 0.5f;
			//if (output < 0.2f) output = 0.2f;

			//se forem a -1 os limites e limiares quer dizer que nao há
			if (limiarDown != -1 && limiarUp != -1) {
				if (strength < limiarDown) {
					if (limiteDown != -1) {
						sensor_output = limiteDown;
					} else {
						//sensor_output = 0;
					}
				}

				if (strength > limiarUp) {
					if (limiteUp != -1) {
						sensor_output = limiteUp;
					} else {
						//sensor_output = 0;
					}
				}
			}

			//mesma cena que em cima, quase x)
			/*if (limiteDown != -1 && limiteUp != -1) {
				if (sensor_output < limiteDown) {
					sensor_output = limiteDown;
				}

				if (sensor_output > limiteUp) {
					sensor_output = limiteUp;
				}
			}*/

			

			sensor_output = sensor_output * bias;

		Debug.Log ("sensor output " + sensor_output);

		return (float) sensor_output;
		}

	// Returns all "Light" tagged objects. The sensor angle is not taken into account.
	GameObject[] GetAllLights()
	{
		return GameObject.FindGameObjectsWithTag ("Light");
	}

	// Returns all "Light" tagged objects that are within the view angle of the Sensor. Only considers the angle over
	// the y axis. Does not consider objects blocking the view.
	GameObject[] GetVisibleLights()
	{
		ArrayList visibleLights = new ArrayList();
		float halfAngle = angle / 2.0f;

		GameObject[] lights = GameObject.FindGameObjectsWithTag ("Light");

		foreach (GameObject light in lights) {
			Vector3 toVector = (light.transform.position - transform.position);
			Vector3 forward = transform.forward;
			//toVector.y = 0;
			//forward.y = 0;
			float angleToTarget = Vector3.Angle (forward, toVector);

			if (angleToTarget <= halfAngle) {
				visibleLights.Add (light);
			}
		}

		return (GameObject[])visibleLights.ToArray(typeof(GameObject));
	}


}
