using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public NetworkPlayer owner;
    public GameObject thecam;
    GameObject pcam;
    public float velocity = 5.0f;
    public float sens = 25.0f;

    void Awake() {
        if (!networkView.isMine) {
            enabled = false;
        }
    }

    void Start() {
        if (networkView.isMine) {
            pcam = (GameObject) Instantiate(thecam, new Vector3(0.0f, 5.0f, 0.0f), new Quaternion());
        }
    }

    [RPC]
    void SetPlayer(NetworkPlayer player) {
        owner = player;
        if (player == Network.player) {
            enabled = true;
        }
    }

	void Update () {

        if (networkView.isMine) {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            Vector3 mdir = new Vector3(0.0f, Input.GetAxis("Mouse X"), 0.0f);
            transform.Translate(dir * velocity * Time.deltaTime);
            pcam.transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            transform.Rotate(mdir * Time.deltaTime * sens);
            pcam.transform.rotation = transform.rotation;
        }

	}

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info) {
        if (stream.isWriting) {
            Vector3 poss = transform.position;
            stream.Serialize(ref poss);
        } else {
            Vector3 posr = Vector3.zero;
            stream.Serialize(ref posr);
            transform.position = posr;
        }
    }
}
