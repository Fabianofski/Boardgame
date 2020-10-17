using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayersStartingPos : MonoBehaviour
{
    [Header("Waypoints Offset")]
    public Vector3 targetOffset;
    public Vector2 maxOffsetsX;
    public Vector2 maxOffsetsY;
    public bool Colliding;
    int attempts;
    float z;

    private GameController gamecontroller;

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        z = transform.position.z; 
    }

    public void SetPosition()
    {
        gamecontroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(SetPositionCoroutine());
    }

    public IEnumerator SetPositionCoroutine()
    {
        Vector2 startpos = transform.position;

        do
        {
            targetOffset = new Vector3(Random.Range(maxOffsetsX.x, maxOffsetsX.y), Random.Range(maxOffsetsY.x, maxOffsetsY.y), 0);
            transform.position = startpos;
            transform.position = transform.position + targetOffset;
            attempts++;
            GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForFixedUpdate();
            GetComponent<BoxCollider2D>().enabled = true;
        } while (Colliding && attempts < 100);

        yield return new WaitForFixedUpdate();
        CheckAgainForCollisions(startpos);
    }

    void CheckAgainForCollisions(Vector2 startpos)
    {
        if (Colliding)
        {
            transform.position = startpos;
            StartCoroutine(SetPositionCoroutine());
        }
        else
        {
            GetComponent<MovePlayer>().targetOffset = targetOffset;
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector3(transform.position.x , transform.position.y, z);
            gamecontroller.SetPlayersPositons();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) => Colliding = true;
    void OnTriggerExit2D() => Colliding = false;
}
