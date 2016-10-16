using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StatisticsLogger {
	
	public Dictionary<int,float> bestFitness;
	public Dictionary<int,float> worstFitness;
	public Dictionary<int,float> meanFitness;
	public Dictionary<int,float> desvio;
	private string filename;
	private StreamWriter logger;


	public StatisticsLogger(string name) {
		filename = name;
		bestFitness = new Dictionary<int,float> ();
		worstFitness = new Dictionary<int,float> ();
		meanFitness = new Dictionary<int,float> ();
		desvio = new Dictionary<int,float> ();
	}

	//saves fitness info and writes to console
	public void PostGenLog(List<Individual> pop, int currentGen) {
		pop.Sort((x, y) => x.fitness.CompareTo(y.fitness));
	
		bestFitness.Add (currentGen, pop[0].fitness);
		worstFitness.Add (currentGen, pop[pop.Count-1].fitness);
		meanFitness.Add (currentGen, 0f);
		desvio.Add (currentGen, 0f);

		foreach (Individual ind in pop) {
			meanFitness[currentGen]+=ind.fitness;
		}
		meanFitness [currentGen] /= pop.Count;

		foreach (Individual ind in pop) {
			float temp = ind.fitness - meanFitness [currentGen];
			desvio [currentGen] += (temp*temp);
		}
		desvio [currentGen] = Mathf.Sqrt (desvio [currentGen] / (pop.Count - 1));
		
			Debug.Log ("generation: "+currentGen+"\tbest: " + bestFitness [currentGen]+"\tworst: " + worstFitness [currentGen] + "\tmean: " + meanFitness [currentGen]+"\tdesvio: " + desvio [currentGen]+"\n");
	}

	//writes to file
	public void finalLog() {
		logger = File.CreateText (filename+".txt");

		//writes with the following format: generation, bestfitness, meanfitness
		for (int i=0; i<bestFitness.Count; i++) {
			logger.WriteLine(i+" melhor: "+bestFitness[i]+"    pior:"+worstFitness[i]+"    media:"+meanFitness[i]+"    desvio:"+desvio[i]);
		}

		logger.Close ();
	}
}
