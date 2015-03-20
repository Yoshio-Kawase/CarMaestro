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
			Instantiate (enemies [enemyIndex], transform.position, transform.rotation);
		}

        // パスをランダムで決定する
        int pathIndex = Random.Range(0, drivePathNameList.Length);
        CarPathDrive driveCar = enemies[enemyIndex].GetComponent<CarPathDrive>();
        if(null != driveCar) {
            driveCar.pathName = drivePathNameList[pathIndex];
        }

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
				p.Play();
		}
	}
}
