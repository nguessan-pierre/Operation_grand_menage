using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingScriptWhite : MonoBehaviour
{
    GameObject player;
    bool playerInRange;
    int sizeChildren;
    Mesh meshObject;
    Material outlineMaterial;
    SphereCollider sc;

    void Awake()
    {

        outlineMaterial = Resources.Load("Outline_Silhouette_White") as Material;
        meshObject = gameObject.GetComponent<MeshFilter>().sharedMesh;
        player = GameObject.FindGameObjectWithTag("Player");
        sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;

        // Le gameObject Principal
        GameObject parent = new GameObject();
        parent.transform.parent = gameObject.transform;
        parent.AddComponent<MeshFilter>();
        parent.GetComponent<MeshFilter>().sharedMesh = meshObject;
        parent.AddComponent<MeshRenderer>();

        // TO-DO: Solve when several material
        parent.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
        //for (int i = 0; i < gameObject.GetComponent<MeshRenderer>().materials.Length - 1; i++)
        //{
        parent.GetComponent<MeshRenderer>().material = outlineMaterial;
        //}
        parent.transform.position = gameObject.transform.position;
        parent.transform.rotation = gameObject.transform.rotation;
        parent.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);


        //Chaque enfant du GameObject
        sizeChildren = gameObject.transform.childCount;

        for (int i = 0; i < sizeChildren - 1; i++)
        {
            GameObject child = new GameObject();
            GameObject GrandChild = new GameObject();
            Mesh meshObjectChild = new Mesh();
            //Assigns the first child of the Game Object this script is attached to.
            child = gameObject.transform.GetChild(i).gameObject;
            meshObjectChild = child.GetComponent<MeshFilter>().sharedMesh;
            GrandChild.transform.parent = child.transform;
            GrandChild.AddComponent<MeshFilter>();
            GrandChild.GetComponent<MeshFilter>().sharedMesh = meshObjectChild;
            GrandChild.AddComponent<MeshRenderer>();
            GrandChild.GetComponent<MeshRenderer>().material = child.GetComponent<MeshRenderer>().material;
            //for (int j = 0; j < child.GetComponent<MeshRenderer>().materials.Length; j++)
            //{
            GrandChild.GetComponent<MeshRenderer>().material = outlineMaterial;
            //}
            GrandChild.transform.position = child.transform.position;
            GrandChild.transform.rotation = child.transform.rotation;
            GrandChild.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        sc.isTrigger = true;
        sc.radius = 3.0f;

    }


    // Update is called once per frame
    void Update()
    {

        if (playerInRange)
        {
            //enfant.SetActive(true);
        }
        else
        {
            //enfant.SetActive(false);
        }

    }


    void OnTriggerEnter(Collider sc)
    {
        if (sc.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider sc)
    {
        if (sc.gameObject == player)
        {
            playerInRange = false;
        }
    }
}

