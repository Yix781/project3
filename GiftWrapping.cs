
using UnityEngine;

public class GiftWrapping : MonoBehaviour
{
    public Renderer boxRenderer;
    public Renderer lidRenderer;
    public Material[] wrappingMaterials;

    // 新增：连接 Checklist
    public GiftChecklistManager checklist;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touched: " + other.name);

        WrappingPaper paper =
            other.GetComponentInParent<WrappingPaper>();

        if (paper == null)
        {
            Debug.Log("No WrappingPaper script found");
            return;
        }

        int index = paper.paperIndex;

        Debug.Log("Paper Index: " + index);

        if (index < 0 || index >= wrappingMaterials.Length)
            return;

        Material selectedMaterial = wrappingMaterials[index];

        if (selectedMaterial == null)
            return;

        if (boxRenderer != null)
        {
            boxRenderer.material = selectedMaterial;
            Debug.Log("Box material changed!");
        }

        if (lidRenderer != null)
        {
            lidRenderer.material = selectedMaterial;
            Debug.Log("Lid material changed!");
        }

        // 新增：包装成功后完成 Checklist 第四步
        if (checklist != null &&
            (boxRenderer != null || lidRenderer != null))
        {
            checklist.CompleteStep4();
            Debug.Log("Checklist Step 4 completed!");
        }
    }
}
