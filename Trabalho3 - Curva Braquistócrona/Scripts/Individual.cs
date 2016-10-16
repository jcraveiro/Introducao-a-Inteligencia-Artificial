using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Individual {

	
	public Dictionary<float, float> trackPoints;
	protected BrachystochroneProblem problem;
	protected ProblemInfo info;

	public float fitness;
	public FitnessInfo eval;

	public Individual(ProblemInfo inf) {

		info = inf;
		fitness = 0f;
		problem = new BrachystochroneProblem (info);
		trackPoints = new Dictionary<float,float > ();

	}

	//override on each specific individual class
	public abstract void Initialize ();
	public abstract void Mutate (float probability);

	public abstract void CalcTrackPoints ();
	public abstract void CalcFitness();
	public abstract Individual Clone ();

	public void evaluate() {
		CalcTrackPoints ();
		eval = problem.evaluate (trackPoints);
		CalcFitness ();

	}

	public override string ToString ()
	{
		List<float> result = new List<float> ();

		foreach (KeyValuePair<float, float> point in trackPoints) {
			result.Add (point.Key);
			result.Add (point.Value);
		}

		return "[Individual] track points: [" + string.Join (",", result.ConvertAll<string> (f => f.ToString()).ToArray()) + "]";
	}

	//RECOMBINAÇÃO
	public void Crossover (Individual partner, float probability){
		float auxiliar;
		List<int> selected = new List<int> ();
		int temp;
		//ESCOLHE PONTOS ALEATÓRIOS PRIMEIRO
		for(int i=0;i<info.numberOfCrossoverPoints;i++){
			temp = Random.Range(0,info.numTrackPoints);
			while (selected.Contains(temp)){
				temp = Random.Range(0,info.numTrackPoints);
			}
			selected.Add(temp);
		}
		selected.Sort();
		int current = 0;
		int alternate = 0;
		List<float> keys = new List<float>(trackPoints.Keys);

		//FAZ RECOMBINAÇÃO AOS PONTOS SELECIONADOS
		foreach(int element in selected){
			while (current < element){
				if (alternate == 0){
					auxiliar = trackPoints[keys[current]];
					trackPoints[keys[current]] = partner.trackPoints[keys[current]];
					partner.trackPoints[keys[current]] = auxiliar;
				}
				else{
				}
				current++;
			}
			if (alternate == 0) alternate = 1;
			else alternate = 0;
		}
	}






}
