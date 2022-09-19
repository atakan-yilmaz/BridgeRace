using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    public static GenerateCubes instance;

    public GameObject redCube, greenCube, blueCube;
    public Transform redCubeParent, greenCubeParent, blueCubeParent;
    public LayerMask layerMask;


    public int MinX, MaxX, MinZ, MaxZ; //degiskenleri kontrol edebilmek icin

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //0red 1blue 2green character
    public void GenerateCube(int number, CharacterAI characterAI=null)
    {
        if (number == 0)
        {
            Generate(redCube,redCubeParent,characterAI);
        }
        if (number == 1)
        {
            Generate(blueCube, blueCubeParent);
        }
        if (number == 2)
        {
            Generate(greenCube, greenCubeParent, characterAI);
        }
    }

    private void Generate(GameObject gameObject,Transform parent,CharacterAI characterAI = null)
    {
        GameObject g = Instantiate(gameObject);
        g.transform.parent = parent;

        //cubelerin ust uste gelmemesi icin gereken kod 
        Vector3 desPos = GiveRandomPos();
        g.SetActive(false);
        Collider[] colliders = Physics.OverlapSphere(desPos, 1, layerMask);

        while (colliders.Length!=0)
        {
            Debug.Log("Crush : " + colliders[0].gameObject + "  " + desPos);
            desPos = GiveRandomPos();
            colliders = Physics.OverlapSphere(desPos, 1, layerMask);
        }
        g.SetActive(true);
        g.transform.position = desPos;
        if (characterAI!=null)
        {
            characterAI.targets.Add(g);
        }
    }

    private Vector3 GiveRandomPos()
    {
        return new Vector3(Random.Range(MinX, MaxX), redCube.transform.position.y, Random.Range(MinZ, MaxZ));
    }
}