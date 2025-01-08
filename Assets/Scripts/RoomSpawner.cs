using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawner: MonoBehaviour
{
    public int openingDirection;
    // 1 --> North
    // 2 --> South
    // 3 --> East
    // 4 --> West
    
    private int rand;
    public bool spawned = false;
    private RoomTemplates templates;
    void Start()
    {
        templates=RoomTemplates.instance;
    }
    void Spawn()
    {        
        if(!spawned && templates.roomCount<templates.roomObjective)
        {
            switch(openingDirection)
            {
                case 1:
                    rand = Random.Range(0,templates.southRooms.Length);
                    Instantiate(templates.southRooms[rand],transform.position,Quaternion.identity);
                    break;
                case 2:
                    rand = Random.Range(0,templates.northRooms.Length);
                    Instantiate(templates.northRooms[rand],transform.position,Quaternion.identity);
                    break;
                case 3:
                    rand = Random.Range(0,templates.westRooms.Length);
                    Instantiate(templates.westRooms[rand],transform.position,Quaternion.identity);
                    break;
                case 4:
                    rand = Random.Range(0,templates.eastRooms.Length);
                    Instantiate(templates.eastRooms[rand],transform.position,Quaternion.identity);
                    break;
                default:
                    break;
            }
            spawned = true;
            templates.roomCount++;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Room Center"))
        {
            RoomInfos infos = other.GetComponentInParent<RoomInfos>();
            //Debug.Log(openingDirection+" and "+"N:"+infos.openedNorth+",S:"+infos.openedSouth+",E:"+infos.openedEast+",W:"+infos.openedWest);
            if(openingDirection == 1 && infos.openedSouth)
            {
                Destroy(gameObject);
            }else if(openingDirection == 2 && infos.openedNorth)
            {
                Destroy(gameObject);
            }else if(openingDirection == 3 && infos.openedWest)
            {
                Destroy(gameObject);
            }else if(openingDirection == 4 && infos.openedEast)
            {
                Destroy(gameObject);
            }
            spawned=true;
        }
    }
}
