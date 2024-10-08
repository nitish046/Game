using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using DG.Tweening;
public class StackCollectables : MonoBehaviour
{
    public Transform ItemHolder;
    int NumOfItemsHoldind;
    public float Ypos;
    public void AddNewItem(Transform _toAdd)
    {
        _toAdd.DOJump(ItemHolder.position+new Vector3(0, Ypos * NumOfItemsHoldind, 0), 1.5f, 1, 0.35f).OnComplete(() => {
            NumOfItemsHoldind++;
            _toAdd.SetParent(ItemHolder, true);
            //_toAdd.localPosition = new Vector3(0, Ypos * NumOfItemsHoldind, 0);
            //Temp fix for duel replace below with above later
            _toAdd.localPosition = new Vector3(0, -3, 0);
            _toAdd.localRotation = Quaternion.identity;
        });
        
    }
}

