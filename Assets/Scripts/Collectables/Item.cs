using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private float timeToUp = 0;
    private float timeToDown = 0;
    private float yCord;

    private void Start()
    {
        yCord = transform.position.y;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.transform.gameObject.TryGetComponent(out PlayerController playerController);

            UseItem(playerController);

            Consume();
        }

    }

    void Consume()
    {
        Debug.Log("Health should increase by {healthRecoveryAmount}.");
        Destroy(gameObject);
    }

    protected void Rotation()
    {
        transform.Rotate(Vector3.up,100 * Time.deltaTime);
    }

    private void MoveUpDown()
    {
        if (timeToUp < 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(yCord, yCord + 1, timeToUp), transform.position.z);
            timeToUp += Time.deltaTime;
            return;
        }
        else if (timeToDown < 1)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(yCord + 1, yCord, timeToDown), transform.position.z);
            timeToDown += Time.deltaTime;
            return;
        }
        timeToUp = 0;
        timeToDown = 0;
    }

    private void Update()
    {
        Rotation();
        MoveUpDown();
    }

    public abstract void UseItem(PlayerController playerController);

}
