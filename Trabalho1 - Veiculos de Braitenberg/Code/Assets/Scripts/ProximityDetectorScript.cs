using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class ProximityDetectorScript : MonoBehaviour
{

    public float angle;

    public float output;
    public int numObjects;

    void Start()
    {
        output = 0;
        numObjects = 0;
    }

    void Update()
    {
        GameObject[] blocks = GetAllBlocks();

        output = 0;
        //definimos uma distância maior que o tamanho do ambiente para efeitos de comparação
        float minDistance = 1000;
        Boolean found = false;
        GameObject closestBlock = blocks[0];

        foreach (GameObject block in blocks)
        {
            float distance = Vector3.Distance(block.transform.position, transform.position);
            //encontra o bloco mais próximo
            if (distance < minDistance && distance < 20)
            {
                minDistance = distance;
                closestBlock = block;
                found = true;
            }
        }

        if (found == true)
        //se houver algum bloco, o output depende da sua posição. senão o output é 0
            output += 1f / Mathf.Pow((transform.position - closestBlock.transform.position).magnitude, 2);
        else output = 0;

    }

    // Get Sensor output value
    public float getLinearOutput()
    {

        return output;
    }

    // Devolve os blocos todos
    GameObject[] GetAllBlocks()
    {
        return GameObject.FindGameObjectsWithTag("Block");
    }


}
