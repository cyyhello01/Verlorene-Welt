using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockControl : MonoBehaviour
{
    public AudioClip unlockSound;
    private int[] result, correctCombination;
    public bool isOpened;
    public float shackleUnlock = 0.04f;

    private void Start()
    {
        result = new int[] { 0, 0, 0, 0 };
        correctCombination = new int[] { 1, 2, 3, 4 };
        isOpened = false;
        Rotate.Rotated += CheckResults;
    }

    private void CheckResults(string wheelName, int number)
    {
        switch (wheelName)
        {
            case "WheelOne":
                result[0] = number;
                break;

            case "WheelTwo":
                result[1] = number;
                break;

            case "WheelThree":
                result[2] = number;
                break;

            case "WheelFour":
                result[3] = number;
                break;
        }

        if (result[0] == correctCombination[0] && result[1] == correctCombination[1]
            && result[2] == correctCombination[2] && result[3] == correctCombination[3] && !isOpened)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = unlockSound;
            audio.Play();

            transform.position = new Vector3(transform.position.x, transform.position.y + shackleUnlock, transform.position.z);
            isOpened = true;
            Debug.Log("Opened!");

        }
    }

    private void OnDestroy()
    {
        Rotate.Rotated -= CheckResults;
    }
}
