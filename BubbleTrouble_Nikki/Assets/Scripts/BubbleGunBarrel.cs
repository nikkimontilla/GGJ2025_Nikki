using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleGunBarrel : MonoBehaviour
{
    public GameObject bubble;
    public float velocity = 10f;

    [SerializeField] InputActionProperty bubbleTrigger;

    public void OnEnable()
    {
        bubbleTrigger.action.performed += BubbleTrigger;
    }

    public void BubbleTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameObject newBubble = Instantiate(bubble, transform.position, transform.rotation);
            newBubble.GetComponent<Rigidbody>().linearVelocity = transform.forward * velocity;
        }
    }
}
