using UnityEngine;
using System.Collections;

public class TrafficControl : MonoBehaviour
{
    // 画面タッチからフリック状態に遷移するまでの遊び量(単位：座標)
    public float FlickMarginCoordNorm = 1.5f;
    // XY軸に対するフリック方向許容角度(度)
    public float FlickPermissiveAngle = 20.0f;
    // 上から下
    public bool EnableTopDownPath = true;
    // 上から左
    public bool EnableTopLeftPath = true;
    // 上から右
    public bool EnableTopRightPath = true;
    // 左から上
    public bool EnableLeftUpPath = true;
    // 左から右
    public bool EnableLeftRightPath = true;
    // 左から下
    public bool EnableLeftDownPath = true;
    // 右から上
    public bool EnableRightUpPath = true;
    // 右から左
    public bool EnableRightLeftPath = true;
    // 右から下
    public bool EnableRightDownPath = true;
    // 下から上
    public bool EnableBottomUpPath = true;
    // 下から左
    public bool EnableBottomLeftPath = true;
    // 下から右
    public bool EnableBottomRightPath = true;

    public enum FLICK_DIRECTION
    {
        FLICK_NEUTRAL,      // ニュートラル
        FLICK_LEFT,         // 左方向にフリック
        FLICK_RIGHT,        // 右方向にフリック
        FLICK_UP,           // 上方向にフリック
        FLICK_DOWN,         // 下方向にフリック
        FLICK_UNDEFINED,    // 未定義の方向のフリック
    }

    // XY軸のフリック移動量
    private Vector2 m_flickVector;
    // フリック状態
    protected FLICK_DIRECTION m_flickState = FLICK_DIRECTION.FLICK_NEUTRAL;

    TrafficControl()
    {
        // 移動量初期化
        m_flickVector.Set(0.0f, 0.0f);
    }

    ~TrafficControl()
    {
    }

    FLICK_DIRECTION judgeFlickVector()
    {
        FLICK_DIRECTION flickDirection = m_flickState;

        // マウス移動量の取得
        // TODO:スマホのタッチ処理に変える必要あり
        Vector2 moveVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // オブジェクトを移動させるテストコード
        //rigidbody2D.AddForce(moveVector * TransRatio);

        // 移動量加算
        m_flickVector += moveVector;
        if ((FlickMarginCoordNorm <= m_flickVector.magnitude ||  FLICK_DIRECTION.FLICK_NEUTRAL != m_flickState) &&
            (0f < m_flickVector.magnitude))
        {
            // フリック遊び閾値を超えたのでフリック方向の判定を行う
            // 一度でも遊びを超えたら、他の方向にフリックしなおしても遊びなしで再判定する
            float flickAngle = Mathf.Atan2(m_flickVector.y, m_flickVector.x) * Mathf.Rad2Deg;
            if (0f - FlickPermissiveAngle < flickAngle && 0f + FlickPermissiveAngle > flickAngle) {
                // 右フリック
                flickDirection = FLICK_DIRECTION.FLICK_RIGHT;
            } else if ((180f - FlickPermissiveAngle < flickAngle && 180f + FlickPermissiveAngle > flickAngle) ||
                        (-180f - FlickPermissiveAngle < flickAngle && -180f + FlickPermissiveAngle > flickAngle)) {
                // 左フリック
                flickDirection = FLICK_DIRECTION.FLICK_LEFT;
            } else if (90f - FlickPermissiveAngle < flickAngle && 90f + FlickPermissiveAngle > flickAngle) { 
                // 上フリック    
                flickDirection = FLICK_DIRECTION.FLICK_UP;
            } else if (-90f - FlickPermissiveAngle < flickAngle && -90f + FlickPermissiveAngle > flickAngle) { 
                // 下フリック
                flickDirection = FLICK_DIRECTION.FLICK_DOWN;
            } else { 
                // フリック範囲外
                flickDirection = FLICK_DIRECTION.FLICK_UNDEFINED;
                print("flickAngle[" + flickAngle + "]");
            }

            // 次の判定のために初期化する
            m_flickVector.Set(0.0f, 0.0f);
        } else {
            // まだフリック遊び範囲内
            print("m_flickVector.magnitude[" + m_flickVector.magnitude + "]");
        }

        if (m_flickState != flickDirection)
        {
            print(flickDirection);
        }

        return flickDirection;
    }

    void resumeDrivingCar(FLICK_DIRECTION flickDirection) {
        // 車リストを取得する
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        foreach(GameObject car in cars) {
            CarPathDrive driveCar = car.GetComponent<CarPathDrive>();
            if((null != driveCar) &&
                   (CarPathDrive.CAR_STATE.CAR_STATE_WAIT_INDICATION == driveCar.getCarState())) {
                FLICK_DIRECTION carPath = FLICK_DIRECTION.FLICK_NEUTRAL;
                if (("TopTurnRightPath" == driveCar.pathName) && (true == EnableTopRightPath)) {
                    carPath = FLICK_DIRECTION.FLICK_RIGHT;
                } else if (("TopStraightPath" == driveCar.pathName) && (true == EnableTopDownPath)) {
                    carPath = FLICK_DIRECTION.FLICK_DOWN;
                } else if (("TopTurnLeftPath" == driveCar.pathName) && (true == EnableTopLeftPath)) {
                    carPath = FLICK_DIRECTION.FLICK_LEFT;
                } else if (("LeftStraightPath" == driveCar.pathName) && (true == EnableLeftRightPath)) {
                    carPath = FLICK_DIRECTION.FLICK_RIGHT;
                } else if (("LeftTurnUpPath" == driveCar.pathName) && (true == EnableLeftUpPath)) {
                    carPath = FLICK_DIRECTION.FLICK_UP;
                } else if (("LeftTurnDownPath" == driveCar.pathName) && (true == EnableLeftDownPath)) {
                    carPath = FLICK_DIRECTION.FLICK_DOWN;
                } else if (("BottomStraightPath" == driveCar.pathName) && (true == EnableBottomUpPath)) {
                    carPath = FLICK_DIRECTION.FLICK_UP;
                } else if (("BottomTurnRightPath" == driveCar.pathName) && (true == EnableBottomRightPath)) {
                    carPath = FLICK_DIRECTION.FLICK_RIGHT;
                } else if (("BottomTurnLeftPath" == driveCar.pathName) && (true == EnableBottomLeftPath)) {
                    carPath = FLICK_DIRECTION.FLICK_LEFT;
                } else if (("RightStraightPath" == driveCar.pathName) && (true == EnableRightLeftPath)) {
                    carPath = FLICK_DIRECTION.FLICK_LEFT;
                } else if (("RightTurnUpPath" == driveCar.pathName) && (true == EnableRightUpPath)) {
                    carPath = FLICK_DIRECTION.FLICK_UP;
                } else if (("RightTurnDownPath" == driveCar.pathName) && (true == EnableRightDownPath)) {
                    carPath = FLICK_DIRECTION.FLICK_DOWN;
                }

                if (flickDirection == carPath) {
                    // 指示待ちの車の動作再開
                    CarPathDrive drive = car.GetComponent<CarPathDrive>();
                    if (null != drive) {
                        drive.indicate();
                    }
                }
            }
        }
    }

    void OnMouseUp()
    {
        // フリックに応じてリスナーを呼び出す
        switch(m_flickState) {
            // 左フリック
            case FLICK_DIRECTION.FLICK_LEFT:
                OnFlickLeft();
                break;
            // 右フリック
            case FLICK_DIRECTION.FLICK_RIGHT:
                OnFlickRight();
                break;
            // 上フリック
            case FLICK_DIRECTION.FLICK_UP:
                OnFlickUp();
                break;
            // 下フリック
            case FLICK_DIRECTION.FLICK_DOWN:
                OnFlickDown();
                break;
            default:
                break;
        }

        // フリック状態をニュートラルにする
        m_flickState = FLICK_DIRECTION.FLICK_NEUTRAL;
    }

    void OnMouseDrag()
    {
        // フリック時の動作呼び出しとインスタンスの状態更新
        m_flickState = judgeFlickVector();
    }

    protected virtual void OnFlickLeft()  {
        resumeDrivingCar(FLICK_DIRECTION.FLICK_LEFT);
	}
    protected virtual void OnFlickRight() {
        resumeDrivingCar(FLICK_DIRECTION.FLICK_RIGHT);
    }
    protected virtual void OnFlickUp() {
        resumeDrivingCar(FLICK_DIRECTION.FLICK_UP);
    }
    protected virtual void OnFlickDown() {
        resumeDrivingCar(FLICK_DIRECTION.FLICK_DOWN);
    }
}
