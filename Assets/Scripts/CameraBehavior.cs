using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    GameObject player;
    public float timeOffset;
    public Vector3 posOffSet;
    private Vector3 velocity;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,player.transform.position+posOffSet,ref velocity,timeOffset);
    }
}
