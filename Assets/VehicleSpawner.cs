using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class VehicleSpawner : MonoBehaviour
{
    [Range(0f, 15f)]
    public float speedMultiplier = 1f;
    public GameObject[] vehicleAssets;
    public List<Car> spawnedVehicles = new List<Car>();

    private void Start()
    {
        GenerateTerrain.OnSpawn += spawnListener;
    }

    private void spawnListener(GameObject block)
    {
        if (block.name.Contains("Road"))
        {
            Car newCar = new Car(
                block.transform.position + new Vector3(0, block.transform.lossyScale.y/2, 0),
                vehicleAssets[Random.Range(0, vehicleAssets.Length)]
            );
            GenerateTerrain.OnDelete += (GameObject obj) =>
            {
                if (obj == block) newCar.Delete();
            };
            spawnedVehicles.Add(newCar);
        }
    }

    private void Update()
    {
        Car vehicle;
        for (int i = spawnedVehicles.Count - 1; i > -1; i--)
        {
            vehicle = spawnedVehicles[i];
            if (vehicle.spawnedObject != null)
                vehicle.Update(speedMultiplier);
            else
                spawnedVehicles.RemoveAt(i);
        }
    }

    public class Car
    {
        public readonly GameObject spawnedObject;
        public readonly float speed;
        public readonly int direction;
        private float lastUpdate;

        public Car(Vector3 location, GameObject selected)
        {
            speed = Random.Range(4.7f, 5.8f);
            direction = 2 * Random.Range(0, 2) - 1;
            spawnedObject = Instantiate(selected);
            spawnedObject.transform.position = location + new Vector3(
                0, 
                spawnedObject.transform.lossyScale.y/2f, 
                15f
            );
            lastUpdate = Time.unscaledTime;
        }

        // Used to wrap the Z coordinate around
        private static float WrapPos(float pos)
        {
            return Mathf.Repeat(pos + 15f, 30f) - 15f;
        }

        // Update the location of the vehicle
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
