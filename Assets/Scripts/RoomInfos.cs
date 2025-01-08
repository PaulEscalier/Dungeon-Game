using UnityEngine;

public class RoomInfos : MonoBehaviour
{
    public bool openedNorth;
    public bool openedSouth;
    public bool openedWest;
    public bool openedEast;
    private RoomTemplates templates;
    void Start()
    {
        templates = RoomTemplates.instance;
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
}
