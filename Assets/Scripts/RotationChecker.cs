using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationChecker : MonoBehaviour
{
    [SerializeField]
    private Transform enemyTransform;

    [SerializeField]
    private UnityEvent<Vector3> OnPlayerRotate;

    private Player player;

    private void Start()
    {
        player = transform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Helper.FacingRight(transform) && transform.position.x > enemyTransform.position.x && !player.isRotated)
        {
            OnPlayerRotate.Invoke(enemyTransform.position);
        }

        if (!Helper.FacingRight(transform) && transform.position.x < enemyTransform.position.x && !player.isRotated)
        {
            OnPlayerRotate.Invoke(enemyTransform.position);
        }
    }
}
