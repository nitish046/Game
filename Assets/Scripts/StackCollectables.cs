using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StackCollectables : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text winText;
    public Button restartButton;
    public Button quitButton;

    public Transform ItemHolder;
    int NumOfItemsHoldind;
    public float Ypos;


    private void Start()
    {
        NumOfItemsHoldind = 0;
        setScore();
        winText.gameObject.SetActive(false);
    }

    public void AddNewItem(Transform _toAdd)
    {
        _toAdd.DOJump(ItemHolder.position+new Vector3(0, Ypos * NumOfItemsHoldind, 0), 1.5f, 1, 0.35f).OnComplete(() => {
            NumOfItemsHoldind++;
            setScore();
            _toAdd.SetParent(ItemHolder, true);
            //_toAdd.localPosition = new Vector3(0, Ypos * NumOfItemsHoldind, 0);
            //Temp fix for duel replace below with above later
            _toAdd.localPosition = new Vector3(0, -3, 0);
            _toAdd.localRotation = Quaternion.identity;
            
        });
        
    }

    public void setScore()
    {
        scoreText.text = "Food Collected: " + NumOfItemsHoldind.ToString() + " /2";
        if(NumOfItemsHoldind == 2)
        {
            winText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }
}

