using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rotate : MonoBehaviour
{

    public AudioClip rotateSound;
    public static event Action<string, int> Rotated = delegate { };

    private bool coroutineAllowed;

    private int numberShown;

    private void Start()
    {
        coroutineAllowed = true;
        numberShown = 0;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed)
        {

            Debug.Log("rotate!");
            StartCoroutine("RotateWheel");
        }
    }



    private IEnumerator RotateWheel()
    {
        coroutineAllowed = false;

        for (int i = 0; i <= 11; i++)
        {
            RotateSound();
            transform.Rotate(0f, 3f, 0f);
            yield return new WaitForSeconds(0.01f);

        }

        coroutineAllowed = true;

        numberShown += 1;

        if (numberShown > 9)
        {
            numberShown = 0;
        }

        Rotated(name, numberShown);
    }

    void RotateSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = rotateSound;
        audio.Play();
    }
}

