using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitcherSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _warp;


    public void PlayWarp()
    {
        _warp.Play();
    }
}
