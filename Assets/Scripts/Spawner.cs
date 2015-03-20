using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject[] enemies;		// Array of enemy prefabs.
	public int carCount;

    // 進行方向を表す文字列リスト
    public string[] drivePathNameList;
    // 車の進行方向(↓:90度、→:0度、↑:-90度, ←:-180度)
    public float carDirection = 0f;

	void Start ()
	{
		carCount = 0;
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
        if (carCount < 10) {
            carCount++;
            GameObject cloneEnemy = (GameObject)(Instantiate(enemies[enemyIndex], transform.position, transform.rotation));
            if (null != cloneEnemy) {
                CarPathDrive driveCar = cloneEnemy.GetComponent<CarPathDrive>();
                if (null != driveCar) {
                    // 車の進行方向を決定
                    driveCar.zeroAngle = carDirection;
                    // パスをランダムで決定する
                    int pathIndex = Random.Range(0, drivePathNameList.Length);
                    driveCar.pathName = drivePathNameList[pathIndex];
                }

                // Play the spawning effect from all of the particle systems.
                //foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
                //{
                //		p.Play();
                //}
            }
        }
	}
}
