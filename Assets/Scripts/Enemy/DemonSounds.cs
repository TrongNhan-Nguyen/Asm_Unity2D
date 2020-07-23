using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSounds : MonoBehaviour
{
    public static AudioClip attack, die, hurt;
    static AudioSource audioSource;
    void Start()
    {

        attack = Resources.Load<AudioClip>("DemonAttack");
        die = Resources.Load<AudioClip>("DemonDie");
        hurt = Resources.Load<AudioClip>("DemonHurt");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySounds(string name)
    {
        switch (name)
        {

            case "attack":
                audioSource.PlayOneShot(attack);
                break;
            case "die":
                audioSource.PlayOneShot(die);
                break;
            case "hurt":
                audioSource.PlayOneShot(hurt);
                break;

        }
    }
}
