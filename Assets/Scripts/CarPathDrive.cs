using UnityEngine;
using System.Collections;

public class CarPathDrive : MonoBehaviour
{
    public float velocity = 5f;
    public string pathName = "";
    public float zeroAngle = 90f;

    private Vector2 prevPos = Vector2.zero;
    public enum CAR_STATE {
        CAR_STATE_INCOMING,         // 向かってきている
        CAR_STATE_OUTGOING,         // 遠ざかっている
        CAR_STATE_WAIT_INDICATION,  // 指示待ち
        CAR_STATE_CONGESTION,       // 渋滞待ち
    }
    private CAR_STATE carState = CAR_STATE.CAR_STATE_INCOMING;

    void Start() {
        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "time", velocity));
    }

    void Update() {
        // 前回位置からの移動ベクトルから方位を計算
        if (Vector2.zero != prevPos) {
            float diffy = transform.position.y - prevPos.y;
            float diffx = transform.position.x - prevPos.x;
            if (0f != diffy || 0f != diffx) {
                float angle = Mathf.Atan2(diffy, diffx) * Mathf.Rad2Deg + zeroAngle;
                if (0 > angle) {
                    angle = 360f + angle;
                }
                transform.eulerAngles = new Vector3(0f, 0f, angle);
            }
        }

        // 位置を保持
        prevPos = new Vector2(transform.position.x, transform.position.y);
    }

    public CAR_STATE getCarState() {
        return carState;
    }

    public void pause() {
        iTween.Pause(gameObject);
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void resume() {
        iTween.Resume(gameObject);
        //gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void indicate() {
        resume();
        carState = CAR_STATE.CAR_STATE_OUTGOING;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (("stop" == col.name) && (CAR_STATE.CAR_STATE_INCOMING == carState)) {
            // 停止線に達したため停止
            pause();
            carState = CAR_STATE.CAR_STATE_WAIT_INDICATION;
        } else if (("Car" == col.tag) && (CAR_STATE.CAR_STATE_INCOMING == carState) &&
            (CAR_STATE.CAR_STATE_INCOMING != col.GetComponent<CarPathDrive>().carState)) {
            pause();
            carState = CAR_STATE.CAR_STATE_CONGESTION;
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        if("Car" == col.tag) {
            // 移動先にいる車が移動したので移動再開
            if (CAR_STATE.CAR_STATE_CONGESTION == carState) {
                resume();
                carState = CAR_STATE.CAR_STATE_INCOMING;
            }
        }
    }
}
