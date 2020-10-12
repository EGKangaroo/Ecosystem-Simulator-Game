using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class GraphWindow : MonoBehaviour
{
    [SerializeField]
    private DataCollector collector;

    [SerializeField]
    private Sprite node;

    private RectTransform container;

    private void Awake()
    {
        container = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ShowGraphFromCollector();
    }

    public void ShowGraphFromCollector()
    {
        ShowGraph(collector.GetData());
    }

    void CreateCircle(Vector2 anchoredPos)
    {
        GameObject obj = new GameObject("circle", typeof(Image));
        obj.transform.SetParent(container, false);
        obj.GetComponent<Image>().sprite = node;
        RectTransform objRect = obj.GetComponent<RectTransform>();
        objRect.anchoredPosition = anchoredPos;
        objRect.sizeDelta = new Vector2(11f, 11f);
        objRect.anchorMin = new Vector2(0, 0);
        objRect.anchorMax = new Vector2(0, 0);
    }

    void ShowGraph(List<int> data)
    {
        //clear graph first
        if(container != null)
        {
            foreach (Transform child in container)
            {
                if (child.name != "Background")
                {
                    Destroy(child.gameObject);
                }
            }

            float graphHeight = container.rect.height;
            int highestpoint = data.Max();
            int rounder = (int)Mathf.Ceil(highestpoint / 20f);
            float maxY = 20 * rounder;

            float xSize = container.rect.width / data.Count;

            for (int i = 0; i < data.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (data[i] / maxY) * graphHeight;
                CreateCircle(new Vector2(xPosition, yPosition));
            }
        }
    }
}
