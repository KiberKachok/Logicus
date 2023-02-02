using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int currentLevelIndex;
    public Level[] levels;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI taskText;
    public Button nextButton;
    public Button prevButton;
    public Transform elementsRoot;
    public Level currentLevel;

    public GameObject inputNode;
    public GameObject outputNode;
    private void Start()
    { 
        prevButton.interactable = false;

        foreach (Level level in levels)
        {
            level.status = " [???]";
        }
        ActivateLevel(0);

    }

    public void NextLevel()
    {
        ActivateLevel(currentLevelIndex + 1);
        UpdateButtons();
    }
    
    public void PrevLevel()
    {
        ActivateLevel(currentLevelIndex - 1);
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        nextButton.interactable = currentLevelIndex != 4;
        prevButton.interactable = currentLevelIndex != 0;
    }
    
    public void ActivateLevel(int index)
    {
        for (int i = 0; i < elementsRoot.transform.childCount; i++)
        {
            Destroy(elementsRoot.transform.GetChild(i).gameObject);
        }
        
        currentLevelIndex = index;
        currentLevel = levels[index];
        titleText.text = currentLevel.title + currentLevel.status;
        taskText.text = currentLevel.task;
        FindObjectOfType<ElementCreator>().allowedElements = currentLevel.allowedElements;
        inputNode = Instantiate(currentLevel.inputNode, elementsRoot);
        inputNode.GetComponent<RectTransform>().anchoredPosition = new Vector2(-150f, 0);
        
        outputNode = Instantiate(currentLevel.outputNode, elementsRoot);
        outputNode.GetComponent<RectTransform>().anchoredPosition = new Vector2(150f, 0);
    }

    [ContextMenu("Check")]
    public void Check()
    {
        bool isTaskGood = true;
        
        List<Element> elements = new List<Element>();
        elements.AddRange(FindObjectsOfType<Inv>());
        elements.AddRange(FindObjectsOfType<Nand>());
        elements.AddRange(FindObjectsOfType<Or>());
        elements.AddRange(FindObjectsOfType<Xor>());
        elements.AddRange(FindObjectsOfType<And>());
        List<Connection> connections = FindObjectsOfType<Connection>().ToList();
        
        for (int i = 0; i < currentLevel.condition[0].values.Length; i++)
        {
            
            foreach (var microCondition in currentLevel.condition)
            {
                GameObject.Find(microCondition.name).GetComponent<Connector>().IsActive
                    = microCondition.values[i];
            }


            for (int j = 0; j < 20; j++)
            {
                foreach (Element element in elements)
                {
                    ((IUpdatable) element).Update();
                }
                
                foreach (Connection connection in connections)
                {
                    ((IUpdatable) connection).Update();
                }
            }
            
            bool isGood = true;
            
            foreach (var microAnswer in currentLevel.answer)
            {
                if (GameObject.Find(microAnswer.name).GetComponent<Connector>().IsActive 
                    != microAnswer.values[i])
                {
                    isGood = false;
                }
            }

            
            if(!isGood)
            {
                isTaskGood = false;
                currentLevel.status = $"<color=#FF3434> [Ошибка] Тест №{i+1}";
                break;
            }
            Debug.Log($"{i}:{isGood}");
        }

        if (isTaskGood)
        {
            currentLevel.status = "<color=#6EFF66> [Решено]";
        }
        titleText.text = currentLevel.title + currentLevel.status;
        
        //<color=#6EFF66> [Решено]
        //<color=#FF3434> [Ошибка]
        // [???]
    }
}
