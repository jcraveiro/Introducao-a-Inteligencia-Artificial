using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TournamentSelection : SelectionMethod {

	public TournamentSelection(): base() {

	}


	public override Individual selectIndividuals (List<Individual> oldpop, int num)
	{
		return tournamentSelection (oldpop, num);
	}


	Individual tournamentSelection(List<Individual>oldpop, int num) {

		List<Individual> selectedInds = new List<Individual> ();
		int chosen = 0;
		float best = 10000;
		int popsize = oldpop.Count;
		for (int i = 0; i<num; i++) {
			//make sure selected individuals are different
			Individual ind = oldpop [Random.Range (0, popsize)];
			while (selectedInds.Contains(ind)) {
				ind = oldpop [Random.Range (0, popsize)];
			}
			selectedInds.Add (ind.Clone()); //we return copys of the selected individuals
		}


		for (int i = 0; i < num; i++) {
			if (selectedInds [i].fitness < best) {
				chosen = i;
				best = selectedInds [i].fitness;
			}
		}
		return selectedInds[chosen];
	}

}
