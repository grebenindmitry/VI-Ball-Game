using UnityEngine;

public class BoxCollider : MonoBehaviour
{

    public GameObject endCanvas;

    private void OnCollisionEnter(Collision collision)
    {       
        collision.rigidbody.useGravity = false;
        
        endCanvas.SetActive(true);
    }
}
