using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaceObjects4 : MonoBehaviour
{
    public TerrainController4 TerrainController { get; set; }

    public void Place()
    {
        int numObjects = Random.Range(TerrainController.MinObjectsPerTile, TerrainController.MaxObjectsPerTile);
        for (int i = 0; i < numObjects; i++)
        {
            int prefabType = Random.Range(0, TerrainController.PlaceableObjects.Length);
            Vector3 startPoint = RandomPointAboveTerrain();

            RaycastHit hit;
            if (Physics.Raycast(startPoint, Vector3.down, out hit) && hit.point.y > TerrainController.Water.transform.position.y && hit.collider.CompareTag("Terrain"))
            {
                Quaternion orientation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
                RaycastHit boxHit;
                if (Physics.BoxCast(startPoint, TerrainController.PlaceableObjectSizes[prefabType], Vector3.down, out boxHit, orientation) && boxHit.collider.CompareTag("Terrain"))
                {
                    Instantiate(TerrainController.PlaceableObjects[prefabType], new Vector3(startPoint.x, hit.point.y, startPoint.z), orientation, transform);
                }
            }

        }
    }

    private Vector3 RandomPointAboveTerrain()
    {
        return new Vector3(
            Random.Range(transform.position.x - TerrainController.TerrainSize.x / 2, transform.position.x + TerrainController.TerrainSize.x / 2),
            transform.position.y + TerrainController.TerrainSize.y * 2,
            Random.Range(transform.position.z - TerrainController.TerrainSize.z / 2, transform.position.z + TerrainController.TerrainSize.z / 2)
        );
    }
}
