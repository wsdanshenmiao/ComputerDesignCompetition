using UnityEngine;

public class ClonedPrefab : MonoBehaviour, ITargetable
{
    public bool CanBeTargeted => true;
    
    [SerializeField]private SpriteRenderer[] spriteRenderers;
    private float fadeSpeed = 3f;
    private bool isDeleting = false;
    private Animator cloneAnimator;

    public Transform GetTargetTransform()
    {
        return transform;
    }

    public void Delete()
    {
        isDeleting = true;
    }

    private void Update()
    {
        if (isDeleting)
        {
            bool allInvisible = true;
            
            // 让所有精灵逐渐消失
            foreach (var sr in spriteRenderers)
            {
                Color color = sr.color;
                color.a -= Time.deltaTime * fadeSpeed;
                sr.color = color;
                
                if (color.a > 0)
                {
                    allInvisible = false;
                }
            }

            if (allInvisible)
            {
                Destroy(gameObject);
            }
        }
    }
} 