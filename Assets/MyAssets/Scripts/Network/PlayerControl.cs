using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float velocity = 5.0f;

    void Awake() {
        if (!networkView.isMine) {
            enabled = false;
        }
    }

	void Update () {

        if (networkView.isMine) {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(dir * velocity * Time.deltaTime);
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
