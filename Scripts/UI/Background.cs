using UnityEngine;

public class Background : MonoBehaviour
{
    public enum backgroundColor {blue,brown,gray,green,pink,purple,yellow };

    [Header("Color")]
    [SerializeField] private backgroundColor bgColor;
    [SerializeField] private Texture2D[] textures;


    [Header("Texture Movement")]

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private Vector2 movementDirection;


    private void Awake()
    {
        backgroundUpdate();
    }
    void Update()
    {
        mesh.material.mainTextureOffset += movementDirection * Time.deltaTime;
    }

    public void backgroundUpdate()
    {
        if(mesh == null)
        {
            mesh = GetComponent<MeshRenderer>();
        }
        mesh.material.mainTexture = textures[(int)bgColor];
    }
}
