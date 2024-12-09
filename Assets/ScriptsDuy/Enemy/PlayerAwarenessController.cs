using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer{get ; private set;}
    public Vector2 DirectionToPlayer { get ; private set;}
    [SerializeField]
    private float _playerAwarenessDistance;
    private Transform _player;
    void Start()
    {
    GameObject playerObject = GameObject.FindWithTag("Player");
    if (playerObject != null)
    {
        _player = playerObject.transform;
    }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else
        {
            AwareOfPlayer = false;
        }
    }
}
