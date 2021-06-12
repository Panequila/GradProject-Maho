using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPosition;
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerPosition.transform.position, 1f * Time.deltaTime);
    }
}
