using UnityEngine;

public class BubbleCollisionManager : MonoBehaviour
{
    public Material targetMaterial;
    public float floatSpeed = 5f;
    private bool shouldFloat = false;
    private Transform objectToFloat;
    public float maxHeight = 10f; // Maximum height before destroying

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit: {other.gameObject.name}");
        
        var meshRenderer = other.GetComponentInChildren<MeshRenderer>();
        var skinnedRenderer = other.GetComponentInChildren<SkinnedMeshRenderer>();

        Debug.Log($"MeshRenderer: {meshRenderer != null}");
        Debug.Log($"SkinnedRenderer: {skinnedRenderer != null}");

        if (skinnedRenderer)
        {
            skinnedRenderer.material = targetMaterial;

            shouldFloat = true;
            objectToFloat = other.transform;

        }
        
    }

    void Update()
    {
        if (shouldFloat && objectToFloat != null)
        {
            objectToFloat.Translate(Vector3.up * floatSpeed * Time.deltaTime);

            if (objectToFloat.position.y >= maxHeight)
            {
                Destroy(objectToFloat.gameObject);
                shouldFloat = false;
            }
        }
    }
}
