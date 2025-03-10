using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    private PlayerInputController playerInput;
    public GameObject signSprite;

    private bool canPress = false;
    private I_Interactable interactableObj;

    private void Awake()
    {
        interactableObj = null;

        playerInput = new PlayerInputController();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.GamePlay.Interact.started += OnInteract;
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        if (!canPress) return;


        Vector3 scale = transform.localScale;
        transform.localScale = PlayerController.Instance.transform.localScale.x < 0 ?
            new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z) :
            new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (canPress && interactableObj != null)
        {
            interactableObj.TriggerAction();
            AudioManager.PlayAudio(AudioName.ClickButton);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        CanPress(coll);
    }

    protected void OnCollisionStay2D(Collision2D coll)
    {
        CanPress(coll.collider);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        CantPress(coll);
    }

    protected void OnCollisionExit2D(Collision2D coll)
    {
        CantPress(coll.collider);
    }

    private void CanPress(Collider2D coll)
    {
        if (coll.CompareTag("Interactable") && SceneLoader.Instance.OnFadeFinish() == true)
        {
            canPress = true;
            interactableObj = coll.GetComponent<I_Interactable>();
            //Debug.Log("已经获取碰撞体");
        }
    }

    private void CantPress(Collider2D coll)
    {
        if (coll.CompareTag("Interactable"))
        {
            canPress = false;
        }
    }
}
