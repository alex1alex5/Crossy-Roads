using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField] private GameObject log;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    private IEnumerator SpawnLogs()
    {
		while (true)
        {
            //Time in between spawns
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            Instantiate(log, spawnPos.position, Quaternion.identity);
        }
       
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LogSpawner : MonoBehaviour
{
    [Range(0f, 15f)]
    public float speedMultiplier = 1f;
    public GameObject[] logAssets;
    public List<Log> spawnedLogs = new List<Log>();

    private void Start()
    {
        GenerateTerrain.OnSpawn += spawnListener;
    }

    private void spawnListener(GameObject block)
    {
        if (block.name.Contains("Water"))
        {
            Log newLog = new Log(
                block.transform.position + new Vector3(0, block.transform.lossyScale.y / 2, 0),
                logAssets[Random.Range(0, logAssets.Length)]
            );
            GenerateTerrain.OnDelete += (GameObject obj) =>
            {
                if (obj == block) newLog.Delete();
            };
            spawnedLogs.Add(newLog);
        }
    }

    private void Update()
    {
        Log log;
        for (int i = spawnedLogs.Count - 1; i > -1; i--)
        {
            log = spawnedLogs[i];
            if (log.spawnedObject != null)
                log.Update(speedMultiplier);
            else
                spawnedLogs.RemoveAt(i);
        }
    }

    public class Log
    {
        public readonly GameObject spawnedObject;
        public readonly float speed;
        public readonly int direction;
        private float lastUpdate;

        public Log(Vector3 location, GameObject selected)
        {
            speed = Random.Range(4.7f, 4.9f);
            direction = 2 * Random.Range(0, 2) - 1;
            spawnedObject = Instantiate(selected);
            spawnedObject.transform.position = location + new Vector3(
                0,
                spawnedObject.transform.lossyScale.y / 2f,
                15f
            );
            lastUpdate = Time.unscaledTime;
        }

        // Used to wrap the Z coordinate around
        private static float WrapPos(float pos)
        {
            return Mathf.Repeat(pos + 15f, 30f) - 15f;
        }

        // Update the location of the log
        public void Update(float speedMultiplier)
        {
            float cTime = Time.unscaledTime;
            Vector3 pos = spawnedObject.transform.position;
            pos.z = WrapPos(pos.z + (cTime - lastUpdate) * speed * speedMultiplier * direction); // Based on the current location, time since last update, and speed
            spawnedObject.transform.position = pos;
            lastUpdate = cTime;
        }

        public void Delete()
        {
            Destroy(spawnedObject);
        }
    }
}
*/
