using System;
using System.Collections;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{

    public GameObject[] northRooms;
    public GameObject[] southRooms;
    public GameObject[] eastRooms;
    public GameObject[] westRooms;
    public GameObject crossSection;
    public GameObject northWall;
    public GameObject southWall;
    public GameObject eastWall;
    public GameObject westWall;

    public static RoomTemplates instance;
    public int roomObjective = 10;
    public int roomCount = 0;
    private bool wallSpawned = false;
    void Awake()
    {
        if(instance!=null)
        {
            Debug.LogError("There are 2 instances of RoomTemplates");
            return;
        }
        instance = this;
    }
    
    void Update()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Room SpawnPoint");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        int inactiveSpawners = 0;
        foreach (GameObject spawner in spawners)
        {
            RoomSpawner spawnerComponent = spawner.GetComponent<RoomSpawner>();
            if (spawnerComponent != null && spawnerComponent.spawned == true)
            {
                inactiveSpawners++;
            }
        }

        
        if(spawners.Length-inactiveSpawners==0 && roomCount<roomObjective && !wallSpawned)
        {
            GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room Center");
            Array.Sort(rooms, (roomA, roomB) =>
            {
                float distanceA = Vector3.Distance(player.transform.position, roomA.transform.position);
                float distanceB = Vector3.Distance(player.transform.position, roomB.transform.position);
                return distanceB.CompareTo(distanceA); // Décroissant
            });
            GameObject mostDistantRoom = rooms[0];
            Destroy(mostDistantRoom.transform.parent.gameObject);
            Instantiate(crossSection,mostDistantRoom.transform.position,Quaternion.identity);

        }else if(spawners.Length>0 && roomCount<roomObjective && !wallSpawned)
        {
            foreach (GameObject spawner in spawners)
            {
                RoomSpawner spawnerComponent = spawner.GetComponent<RoomSpawner>();
                if (spawnerComponent != null && !spawnerComponent.spawned)
                {
                    spawnerComponent.Invoke("Spawn",0.2f);
                    break;
                }
            }
        }
        
        if(!wallSpawned && roomCount >= roomObjective)
        {
            Invoke("SpawnWalls",0.2f);
            wallSpawned = true;
        }
    }
    void SpawnWalls()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Room SpawnPoint");
            if(spawnPoints.Length>0)
            {
                foreach(GameObject spawnPoint in spawnPoints)
                {
                    switch(spawnPoint.GetComponent<RoomSpawner>().openingDirection)
                    {
                        case 1:
                            spawnPoint.GetComponentInParent<RoomInfos>().CreateWall("North");
                            Destroy(spawnPoint);
                            break;
                        case 2:
                            spawnPoint.GetComponentInParent<RoomInfos>().CreateWall("South");
                            Destroy(spawnPoint);
                            break;
                        case 3:
                            spawnPoint.GetComponentInParent<RoomInfos>().CreateWall("East");
                            Destroy(spawnPoint);
                            break;
                        case 4:
                            spawnPoint.GetComponentInParent<RoomInfos>().CreateWall("West");
                            Destroy(spawnPoint);
                            break;
                        default:
                            break;
                    }
                    
                }
            }
            
    }

}
