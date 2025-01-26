using UnityEngine;

public class BubbleElevator : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 10f;
    [SerializeField] private float maxHeight = 50f;
    [SerializeField] private KeypadController keypad;

    private bool isFloating = false;
    private BoxCollider triggerCollider;
    private BoxCollider platformCollider;

    void Start()
    {
        // Add two box colliders
        triggerCollider = gameObject.AddComponent<BoxCollider>();
        platformCollider = gameObject.AddComponent<BoxCollider>();

        // Set up trigger collider
        triggerCollider.isTrigger = true;
        triggerCollider.size = new Vector3(1.2f, 0.5f, 1.2f); // Slightly larger

        // Set up platform collider
        platformCollider.size = new Vector3(1f, 0.1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (keypad != null && keypad.HasUsedCorrectCode)
        {
            isFloating = true;
        }
    }

    void Update()
    {
        if (isFloating)
        {
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
            {
                isFloating = false;
            }
        }
    }
}
