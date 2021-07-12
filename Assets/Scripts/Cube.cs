using UnityEngine;
using TMPro;
public class Cube : MonoBehaviour
{
    static int staticId = 0;
    [SerializeField] private TMP_Text[] numbersText;

    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidBody;
    [HideInInspector] public bool IsMainCube;
    [HideInInspector] public int cubeId;
    private MeshRenderer CubeMeshRenderer;

    private void Awake()
    {
        cubeId = staticId++;
        CubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRigidBody = GetComponent<Rigidbody>();

    }
    public void SetColour(Color color)
    {
        CubeColor = color;
        CubeMeshRenderer.material.color = color;
    }
    public void setNumber(int number)
    {
        CubeNumber = number;
        for (int i=0; i<6; i++)
        {
            numbersText[i].text = number.ToString();
        }       
    }
}
