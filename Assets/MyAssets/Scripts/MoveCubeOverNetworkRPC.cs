using UnityEngine;
using System.Collections;

public class MoveCubeOverNetworkRPC : MonoBehaviour {

    float speed = 2.0f;
    float timer = 3.0f;
    bool toLeft = true;

    Vector3 lastpos;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update() {

        if (Network.isServer) {
            if (toLeft == true) {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                timer -= Time.deltaTime;
            } else if (toLeft == false) {
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                timer += Time.deltaTime;
            }
            if (timer >= 5.0f) {
                toLeft = true;
            } else if (timer <= 0.0f) {
                toLeft = false;
            }

            if (Vector3.Distance(transform.position, lastpos) >= 0.05) {
                lastpos = transform.position;
                networkView.RPC("SetPosition", RPCMode.Others, transform.position);
            }
        }
        
    }
    
    [RPC]
    void SetPosition(Vector3 newpos) {
        transform.position = newpos;
    }
    

}
