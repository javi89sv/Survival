using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{

    public Ring data;
    public RingCakePiece ringCakePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected RingCakePiece[] pieces;
    protected RingMenu parent;
    public string path;


    private void Start()
    {
        var stepLength = 360f / data.elements.Length;
        var iconDist = Vector3.Distance(ringCakePrefab.icon.transform.position, ringCakePrefab.cakePiece.transform.position);


        pieces = new RingCakePiece[data.elements.Length];

        for (int i = 0; i < data.elements.Length; i++)
        {
            pieces[i] = Instantiate(ringCakePrefab, transform);

            pieces[i].transform.localPosition = Vector3.zero;
            pieces[i].transform.localRotation = Quaternion.identity;

            pieces[i].cakePiece.fillAmount = 1f / data.elements.Length - GapWidthDegree / 360f;
            pieces[i].cakePiece.transform.localPosition = Vector3.zero;
            pieces[i].cakePiece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength);
            pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.5f);

            pieces[i].icon.transform.localPosition = pieces[i].cakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            pieces[i].icon.sprite = data.elements[i].icon;

        }
    }

    private void Update()
    {
        var stepLength = 360f / data.elements.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);
        for (int i = 0; i < data.elements.Length; i++)
        {
            if (i == activeElement)
                pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.75f);
            else
                pieces[i].cakePiece.color = new Color(1f, 1f, 1f, 0.5f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            var _path = path + "/" + data.elements[activeElement].nameElement;
            if (data.elements[activeElement].nextRing != null)
            {
                var newSubRing = Instantiate(gameObject, transform.parent).GetComponent<RingMenu>();
                newSubRing.parent = this;
                for (var j = 0; j < newSubRing.transform.childCount; j++)
                    Destroy(newSubRing.transform.GetChild(j).gameObject);
                newSubRing.data = data.elements[activeElement].nextRing;
                newSubRing.path = path;
                newSubRing.callback = callback;
            }
            else
            {
                callback?.Invoke(path);
                Debug.Log("gastamos: " + data.elements[activeElement].itemObject.name + " " + data.elements[activeElement].amount);
            }
            gameObject.SetActive(false);
        }
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;

}
