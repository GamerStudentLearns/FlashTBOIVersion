using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] doors;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.length == 0)
        {
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        foreach (GameObject door in doors)
            door.SetActive(false);
    }
}
