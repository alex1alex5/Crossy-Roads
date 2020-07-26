//this script generates the terrain objects
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GenerateTerrain : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> generatedObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> decorations = new List<GameObject>();
    [SerializeField] private Transform TerrainBin;
    private List<GameObject> currentTerrain = new List<GameObject>();
    private List<GameObject> currentDecorations = new List<GameObject>();

    private int counter = 10;
    private int prev = 100;  
    private int steps = 0;



    public delegate void onSpawn(GameObject  block);
    public static event onSpawn OnSpawn;

    public delegate void onDelete(GameObject block);
    public static event onDelete OnDelete;

    void Start()        
    {   //spawn the first couple blocks by default

        for(int i = 0; i < counter; i++)
        {
            GameObject thing = Instantiate(generatedObjects[0]);
            thing.transform.position = new Vector3(i, 0, 0);
            thing.transform.SetParent(TerrainBin);
            OnSpawn?.Invoke(thing);
        }   
    }

    
    void Update()   
    {
        if (Input.GetButtonDown("up"))
        {
            SpawnTerrain();
            steps++;
            deleteTerrain();
        }
    }

    void SpawnTerrain()
    {
        int selectedBlock = Random.Range(0, 4);     //choose random block
        int selectedLength = Random.Range(1, 3);    //how many times will spawn in a row

        while (selectedBlock == prev)   //get new block incase selected the same one again             
        {
            selectedBlock = Random.Range(0, 4);   
        }

        for (int i = 0; i < selectedLength; i++)    //spawn the block and its associated scenery (trees,etc)
        {
            GameObject block = Instantiate(generatedObjects[selectedBlock]); 
            block.transform.position = new Vector3(counter, 0, 0);
            AddDecorations(selectedBlock, counter);
            currentTerrain.Add(block);              //this list will be deleted so real list is protected
            block.transform.SetParent(TerrainBin);      //block will be put under TerrainBin in heirarchy                    
            OnSpawn?.Invoke(block);
            counter++;                            
        }
        prev = selectedBlock;             
    }

    void AddDecorations(int selectedBlock, int counter)
    {
        if (selectedBlock == 0) //the block is grass, so add trees and stuff
        { 
            for(int i = 0; i < Random.Range(1, 4); i++)
            {
                GameObject tree = Instantiate(decorations[0]);
                tree.transform.position = new Vector3(counter, 1, Random.Range(-6, 6)); 

                currentDecorations.Add(tree);
                tree.transform.SetParent(TerrainBin);
            }
        }
    }


    void deleteTerrain()
    {

        if (currentTerrain[0].gameObject.name == "Grass(Clone)" && steps > 30)
        {    
            Destroy(currentDecorations[0]);
            currentDecorations.RemoveAt(0);
        }
        if (steps > 30)
        {
            OnDelete?.Invoke(currentTerrain[0]);
            Destroy(currentTerrain[0]);
            currentTerrain.RemoveAt(0);
        }

    }
}
