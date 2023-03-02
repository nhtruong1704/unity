using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    public AudioSource soundEff;
    private void OnCollisionEnter(Collision collision)
    {
        soundEff.Play();
    }
}
