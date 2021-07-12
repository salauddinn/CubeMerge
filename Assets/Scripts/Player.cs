using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;
         private Cube mainCube;

    private bool isPointerDownValue;
    private bool canMove;
    private Vector3 cubePos;


    void Start()
    {
        canMove = true;
        spawnCube();

        touchSlider.onPointerDownEvent += onPointerDown;
        touchSlider.onPointerDragEvent += onPointerDrag;
        touchSlider.onPointerupEvent += onPointerUp;

    }

    private void onPointerDown()
    {
        isPointerDownValue = true;
    }

    private void onPointerDrag(float xMovement)
    {
        if (isPointerDownValue)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }

    private void onPointerUp()
    {
        if (isPointerDownValue && canMove)
        {
            canMove = false;
          isPointerDownValue = false;
          mainCube.CubeRigidBody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            Invoke("SpawnNewCube",0.3f);     
        }

    }
    private void SpawnNewCube()
    {
        mainCube.IsMainCube = false;
        canMove = true; 
        spawnCube();

    }


    // Update is called once per frame
    void Update()
    {
        if (isPointerDownValue)
        {
            mainCube.transform.position = Vector3.Lerp(mainCube.transform.position,
                                           cubePos,
                                           moveSpeed * Time.deltaTime);    
        }
        
        
    }

    private void spawnCube()
    {
        mainCube = CubeSpawner.Instance.spawnRandom();
        mainCube.IsMainCube = true;
        cubePos = mainCube.transform.position; 

    }
    void OnDestroy() { 
        touchSlider.onPointerDownEvent -= onPointerDown;
        touchSlider.onPointerDragEvent -= onPointerDrag;
        touchSlider.onPointerupEvent -= onPointerUp;
    }


}
