using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinHead : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "pinHead") {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }

    }
    //注意触发器是[Collider2D collision],碰撞器是[Collision2D collision]
}
