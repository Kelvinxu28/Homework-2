using UnityEngine;

public class CollectObject : MonoBehaviour
{
 private Object hedgecoin;

 private void Awake() {
    hedgecoin = GetComponent<Object>();

 }

 private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.CompareTag("Player")){
        Destroy(gameObject);
    }
 }
}
