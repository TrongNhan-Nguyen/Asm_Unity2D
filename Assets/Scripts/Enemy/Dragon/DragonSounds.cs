using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSounds : MonoBehaviour
{
    public static AudioClip attack, die, hurt;
    static AudioSource audioSource;
    void Start()
    {
        attack = Resources.Load<AudioClip>("DragonAttack");
        die = Resources.Load<AudioClip>("DragonDie");
        hurt = Resources.Load<AudioClip>("DragonHurt");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
