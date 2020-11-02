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

    [SerializeField]
    private RectTransform container;

    private int highestDataPoint = 0;
    private float graphHeight;

    private void Start()
    {
        graphHeight = container.rect.height;
    }

    private void OnEnable()
    {
        ShowGraphFromCollector();
    }

    public void ShowGraphFromCollector()
    {
        Dictionary<LifeForm, List<int>> pop = collector.GetPopulationData();
        foreach(List<int> list in pop.Values)
        {
            int max = list.Max();
            if(max > highestDataPoint)
            {
                highestDataPoint = max;
            }
        }

        foreach (Transform child in container)
        {
            if (child.name != "Background")
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var item in pop)
        {
            ShowGraph(item.Value, item.Key.graphLineColor);
        }
        //ShowGraph(collector.GetData());
    }

    void CreateCircle(Vector2 anchoredPos, Color color)
    {
        GameObject obj = new GameObject("circle", typeof(Image));
        obj.transform.SetParent(container, false);
        obj.GetComponent<Image>().sprite = node;
        obj.GetComponent<Image>().color = color;
        RectTransform objRect = obj.GetComponent<RectTransform>();
        objRect.anchoredPosition = anchoredPos;
        objRect.sizeDelta = new Vector2(11f, 11f);
        objRect.anchorMin = new Vector2(0, 0);
        objRect.anchorMax = new Vector2(0, 0);
    }

    void CreateDotConnection(Vector2 posA, Vector2 posB)
    {
        GameObject line = new GameObject("DotConnection", typeof(Image));
        line.transform.SetParent(container);

    }

    void ShowGraph(List<int> data, Color color)
    {
        if(container != null)
        {
            int rounder = (int)Mathf.Ceil(highestDataPoint / 20f);
            float maxY = 20 * rounder;

            float xSize = container.rect.width / data.Count;

            for (int i = 0; i < data.Count; i++)
            {
                float xPosition = xSize + i * xSize;
                float yPosition = (data[i] / maxY) * graphHeight;
                CreateCircle(new Vector2(xPosition, yPosition), color);
            }
        }
    }
}
