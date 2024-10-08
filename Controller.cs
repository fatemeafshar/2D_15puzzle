using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
//using MetaMethods;

public class Controller : MonoBehaviour
{
    int[] numbers;
    public Transform parentPanel;
    Vector2 startPosition = new Vector2(-200, 200);
    Vector2 emptyPos;
    public List<Block> blocks = new List<Block>();
    public List<Block> instantiatedBlocks = new List<Block>();
    int[] ShuffleBlocks(int puzzle_number)
    {

        int[] numbers = new int[puzzle_number];
        for (int i = 0; i < puzzle_number; i++)
        {
            numbers[i] = (int)i;
        }

        System.Random r = new System.Random();
        Debug.Log(puzzle_number);
        for (int i = puzzle_number - 1; i > -1; i--)
        {
            int k = r.Next(i);
            int temp = numbers[i];
            numbers[i] = numbers[k];
            numbers[k] = temp;

        }

        return numbers;
    }
    GameObject barInstance;

    void Start()
    {
        int puzzle_number = 16;
        int[] numbers = new int[puzzle_number];

        numbers = ShuffleBlocks(puzzle_number);
        //for (int i = 0; i < puzzle_number; i++)
        //{
        //    numbers[i] = (int)i + 1;
        //}
        //numbers[15] = 15;
        //numbers[14] = 0;
        for (int i = 0; i < puzzle_number; i++)
        {
            
            Debug.Log(numbers[i]);
            int row = - i / 4;
            int col = i % 4;
            if (numbers[i] == 0)
            {
                emptyPos = new Vector2(col, row);
            }
            var block = Instantiate(blocks[numbers[i]], new Vector3(startPosition.x + col*100f, startPosition.y + row * 100f, 0.0f), Quaternion.identity);
            block.transform.SetParent(parentPanel, false);
            block.pos = new Vector2(col, row);
            block.controller = this;
            instantiatedBlocks.Add(block);
        }
        //Debug.Log("puzzle");
        //for (int i = 0; i < 16; i++) {
        //    Debug.Log(instantiatedBlocks[i].label);
        //    Debug.Log(instantiatedBlocks[i].pos);
        //}

    }
    public Boolean checkEmpty(Vector2 pos)
    {
        //Debug.Log(emptyPos);
        //Debug.Log(pos);
        if (emptyPos == pos)
        {
            
            return true;
        }
        return false;
    }
    public void setEmpty(Vector2 pos)
    {
        //Debug.Log(emptyPos);
        //Debug.Log(pos);
        Block tempBlock = instantiatedBlocks[(int)(pos.x  - pos.y*4)];
        instantiatedBlocks[(int)(pos.x - pos.y * 4)] = instantiatedBlocks[(int)(emptyPos.x - emptyPos.y * 4)];
        instantiatedBlocks[(int)(emptyPos.x - emptyPos.y*4)] = tempBlock;
        //Debug.Log((int)(pos.x - pos.y * 4));
        //Debug.Log((int)(emptyPos.x - emptyPos.y * 4));
        for (int i = 0; i < instantiatedBlocks.Count; i++)
        {
            Debug.Log(instantiatedBlocks[i].label);
            

        }

        emptyPos = pos;

        int correctBlocks = 0;
        //Debug.Log(instantiatedBlocks.Count);
        for (int i = 0; i < instantiatedBlocks.Count; i++)
        {
            Debug.Log(i);
            if (instantiatedBlocks[i].label - 1 == i)
            {
                Debug.Log("in");
                //Debug.Log(instantiatedBlocks[i].label - 1);
                
                //Debug.Log(instantiatedBlocks[i].pos);
                correctBlocks++;
            }
            
        }
        //Debug.LogError(correctBlocks);
        if (correctBlocks == 15)
        {
            instantiatedBlocks[instantiatedBlocks.Count-1].gameObject.SetActive(true);
            startPosition = instantiatedBlocks[0].transform.position;
            instantiatedBlocks[instantiatedBlocks.Count - 1].transform.position = startPosition + new Vector2(100 * 3, -100 * 3);

            var myResults = parentPanel.GetComponentsInChildren<TMP_Text>();
            Debug.Log(myResults);
            foreach(var result in myResults)
            {
                
                Debug.Log("in text");
                result.text = "";
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

