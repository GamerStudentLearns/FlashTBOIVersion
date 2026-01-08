using UnityEngine;

public class RoomController : MonoBehaviour

{

    public GameObject enemySpawner;

    public DoorController[] doors;

    public GameObject rewardPrefab;

    private bool roomCleared = false;

    private bool rewardSpawned = false;

    public void ActivateRoom()

    {

        foreach (var door in doors)

            door.Close();

        if (enemySpawner != null)

            enemySpawner.SetActive(true);

        roomCleared = false;

    }

    void Update()

    {

        if (roomCleared || enemySpawner == null) return;

        EnemyHealth[] enemies = enemySpawner.GetComponentsInChildren<EnemyHealth>(true);

        foreach (var e in enemies)

        {

            if (e != null && e.gameObject.activeInHierarchy)

                return;

        }

        ClearRoom();

    }

    void ClearRoom()

    {

        roomCleared = true;

        foreach (var door in doors)

            door.Open();

        if (!rewardSpawned && rewardPrefab != null)

        {

            Instantiate(rewardPrefab, transform.position, Quaternion.identity);

            rewardSpawned = true;

        }

    }

    public bool IsCleared()

    {

        return roomCleared;

    }

}
