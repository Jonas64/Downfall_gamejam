using UnityEngine;

public class Patrolpath : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
