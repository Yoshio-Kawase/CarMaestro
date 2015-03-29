using UnityEngine;
using System.Collections;

public class CarPathDrive : MonoBehaviour
{
    public float velocity = 5f;
    public string pathName = "";
    public float zeroAngle = 90f;
    public GameObject popup;        // ポップアップ
    public GameObject guide;        // ガイドアロー

    private Vector2 prevPos = Vector2.zero;
    public enum CAR_STATE {
        CAR_STATE_INCOMING,         // 向かってきている
        CAR_STATE_OUTGOING,         // 遠ざかっている
        CAR_STATE_WAIT_INDICATION,  // 指示待ち
        CAR_STATE_CONGESTION,       // 渋滞待ち
    }
    private CAR_STATE carState = CAR_STATE.CAR_STATE_INCOMING;
    
    // クローンポップアップ
    private GameObject clonePopup = null;
    // 表示位置位置調整
    private float shiftPopup_y = 1f;
    private float shitfPopup_z = -1f;

    // クローンガイドアロー
    private GameObject cloneGuide = null;
    // 表示位置調整
    private float shiftGuide_y = 1f;
    private float shitfGuide_z = -2f;

    void Start() {
        // 移動方向に基づいて移動パスを設定
        iTween.MoveTo(this.gameObject, iTween.Hash(
            "path", iTweenPath.GetPath(pathName),
            "time", velocity,
            "oncomplete", "OnCompleteDrive",
            "oncompletetarget", gameObject));

        // ポップアップオブジェクトのクローン作成
        if(null != popup) {
            clonePopup = (GameObject)(Instantiate(popup, transform.position, transform.rotation));
        }

        // ガイドアローオブジェクトのクローン生成
        if(null != guide) {
            // ガイドアローの方向に合わせて回転させる
            float rotate = 0f;
            if("TopStraightPath" == pathName) {
                // 下方向
                rotate = -90f;
            } else if("TopTurnRightPath" == pathName) {
                // 右方向
                rotate = 0f;
            } else if("TopTurnLeftPath" == pathName) {
                // 左方向
                rotate = 180f;
            } else if("LeftStraightPath" == pathName) {
                // 右方向
                rotate = 0f;
            } else if("LeftTurnDownPath" == pathName) {
                // 下方向
                rotate = -90f;
            } else if("LeftTurnUpPath" == pathName) {
                // 上方向
                rotate = 90f;
            } else if("RightStraightPath" == pathName) {
                // 左方向
                rotate = 180f;
            } else if("RightTurnUpPath" == pathName) {
                // 上方向
                rotate = 90f;
            } else if("RightTurnDownPath" == pathName) {
                // 下方向
                rotate = -90f;
            } else if("BottomStraightPath" == pathName) {
                // 上方向
                rotate = 90f;
            } else if("BottomTurnRightPath" == pathName) {
                // 右方向
                rotate = 0f;
            } else if("BottomTurnLeftPath" == pathName) {
                // 左方向
                rotate = 180f;
            }
            cloneGuide = (GameObject)(Instantiate(guide, transform.position, transform.rotation));
            cloneGuide.transform.Rotate(0f, 0f, rotate);
        }
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

        // ポップアップの位置更新
        if(null != clonePopup) {
            clonePopup.transform.localPosition = new Vector3(
                transform.position.x,
                transform.position.y + shiftPopup_y,
                transform.position.z + shitfPopup_z);
        }

        // ガイドアローの位置更新
        if(null != clonePopup) {
            cloneGuide.transform.localPosition = new Vector3(
                transform.position.x,
                transform.position.y + shiftGuide_y,
                transform.position.z + shitfGuide_z);
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
        } else if(("Car" == col.tag) && (CAR_STATE.CAR_STATE_OUTGOING == carState)) {
            CarPathDrive colCar = col.GetComponent<CarPathDrive>();
            if(null != colCar) {
                if(CAR_STATE.CAR_STATE_OUTGOING == colCar.carState) {
                    // 指示後に車どうしが衝突したらゲームオーバー
                    Application.LoadLevel("end");
                }
            }
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

    public void OnCompleteDrive() {
        // 移動完了したので破棄
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnDestroy() {
        Destroy(clonePopup);
        Destroy(cloneGuide);
    }
}
