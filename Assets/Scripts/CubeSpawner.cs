using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;


    Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesCapacity = 20;
    [SerializeField] private bool autoQueueGrew = true;

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;

    [HideInInspector] public int maxCubeNumber;//4096 2^12

    private int maxPower = 12;

    private Vector3 deafaultSpawnPosition;

    private void Awake()
    {
        Instance = this;

        deafaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        IntializeCubesQueue();

    }

    private void IntializeCubesQueue()
    {
        for (int i = 0; i < cubesCapacity; i++)
            AddCubetoCubeQueue();
    }

   private void AddCubetoCubeQueue()
    {
        Cube cube = Instantiate(cubePrefab, deafaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQueue.Enqueue(cube);

    }

    public Cube Spawn(int number,Vector3 potition)
    {
        if(cubesQueue.Count == 0)
        {
            if (autoQueueGrew)
            {
                cubesCapacity++;
                AddCubetoCubeQueue();
            }
            else
            {
                Debug.Log("NO More Cubes to Spawn");
                return null;
            }
        }
        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = potition;
        cube.setNumber(number);
        cube.SetColour(GetColor(number));
        cube.gameObject.SetActive(true);
        return cube;
    }



    
    public Cube spawnRandom()
    {
        return Spawn(GenerateRandomNumber(),deafaultSpawnPosition)  ;

    }
    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidBody.velocity = Vector3.zero ;
        cube.CubeRigidBody.angularVelocity = Vector3.zero; 
        cube.transform.rotation = Quaternion.identity ;
        cube.IsMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, UnityEngine.Random.Range(1, 6));

    }

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }

}
