using System.Collections.Generic;
using UnityEngine;

public class RoomInfos : MonoBehaviour
{
    public bool openedNorth;
    public bool openedSouth;
    public bool openedWest;
    public bool openedEast;
    private RoomTemplates templates;
    public bool isCorridor;
    public GameObject[] zonesObject;
    public GameObject[] rocs;
    public GameObject[] statues;
    public GameObject[] loots;


    void Start()
    {
        templates = RoomTemplates.instance;
        if(zonesObject.Length>0)
            Invoke("CreateObjects",0.1f);
    }
    public void CreateWall(string direction)
    {
        Vector3 position = Vector3.zero;
        GameObject wall = templates.northWall;
        switch(direction)
        {
            case "North":
                position.y=2.5f;
                break;
            case "South":
                position.y=-4.5f;
                wall = templates.southWall;
                break;
            case "East":
                position.x=5.5f;
                position.y=0.2f;
                wall = templates.eastWall;
                break;
            case "West":
                position.x=-7.5f;
                position.y=0.2f;
                wall = templates.westWall;
                break;
            default:
                break;
        }
        Instantiate(wall,transform.position+position,Quaternion.identity);
    }
    public void CreateObjects()
    {
        foreach(GameObject zoneObject in zonesObject)
        {
            int objectNumber = Random.Range(0,15);
            GameObject objectToInstantiate = null;

            switch(objectNumber)
            {
                case 0:
                    if (rocs.Length>0)
                        objectToInstantiate = rocs[Random.Range(0,rocs.Length)];
                    break;
                case 1:
                    if (statues.Length>0)
                        objectToInstantiate = statues[Random.Range(0,statues.Length)];
                    break;
                case 2:
                    if (loots.Length>0)
                        objectToInstantiate = loots[Random.Range(0,loots.Length)];
                    break;
                default:
                    break;
            }
            if(objectToInstantiate!=null)
                Instantiate(objectToInstantiate,zoneObject.transform.position,Quaternion.identity);
                
            Destroy(zoneObject);
        }
    }
}