using UnityEngine;
using System.Collections;

public class MoveCubeOverNetwork : MonoBehaviour {

    float speed = 2.0f;
    float timer = 3.0f;
    bool toLeft = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Network.isServer) {
            

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
        }

	}


    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        if (stream.isWriting) {
            Vector3 pos = transform.position;
            stream.Serialize(ref pos);
        } else {
            Vector3 posr = Vector3.zero;
            stream.Serialize(ref posr);
            transform.position = posr;
        }
    }

}