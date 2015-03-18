using UnityEngine;
using System.Collections;

public class TopTurnRightPath : MonoBehaviour
{
    public float velocity = 5f;

    private Vector2 prevPos = Vector2.zero;

    void Start() {
        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath("TopTurnRightPath"), "time", velocity));
    }

    void Update() {
        // 前回位置からの移動ベクトルから方位を計算
        if (Vector2.zero != prevPos) {
            float diffy = transform.position.y - prevPos.y;
            float diffx = transform.position.x - prevPos.x;
            if (0f != diffy || 0f != diffx) {
                float angle = Mathf.Atan2(diffy, diffx) * Mathf.Rad2Deg + 90f;
                if (0 > angle) {
                    angle = 360f + angle;
                }
                transform.eulerAngles = new Vector3(0f, 0f, angle);
            }
        }

        // 位置を保持
        prevPos = new Vector2(transform.position.x, transform.position.y);
    }
}
