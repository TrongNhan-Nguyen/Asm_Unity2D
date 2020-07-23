using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static AudioClip health, jump, attack, die, hurt;
    static AudioSource audioSource;
    void Start()
    {
        health = Resources.Load<AudioClip>("PlayerHealth");
        jump = Resources.Load<AudioClip>("PlayerJump");
        attack = Resources.Load<AudioClip>("PlayerAttack");
        die = Resources.Load<AudioClip>("PlayerDie");
        hurt = Resources.Load<AudioClip>("PlayerHurt");

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySounds(string name)
    {
        switch (name)
        {
            case "health":
                audioSource.PlayOneShot(health);
                break;
            case "jump":
                audioSource.PlayOneShot(jump);
                break;
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
