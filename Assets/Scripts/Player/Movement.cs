using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jump = 6f;
    public int maxHealth;
    public HealthBar healthBar;
    Animator anim;
    public Text txtScores;
    public Text txtAlert;
    public static string currentScreen;
    public float delay = 0.5f;
    public GameObject Shop;
    bool isShop = false;
    public Text coins;
   
    void Start()
    {
        anim = GetComponent<Animator>();
        currentScreen = SceneManager.GetActiveScene().name;
        healthBar.SetMaxHealth(maxHealth);
       
        if (currentScreen == "Screen1" || currentScreen == "Screen2")
        {
            txtAlert.gameObject.SetActive(false);
        }
        StartCoroutine(getItem());

    }
    IEnumerator getItem()
    {
        yield return new WaitForSeconds(1.0f);
        speed = UserInfo.speed;
        maxHealth = UserInfo.maxHealth;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = (UserInfo.speed + 2f);
            Debug.Log("shift" + (UserInfo.speed + 2f));
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = UserInfo.speed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerSounds.PlaySounds("jump");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isShop == true)
            {
                if(Shop.activeSelf == false)
                {
                    Shop.SetActive(true);
                }
                else
                {
                    Shop.SetActive(false);

                }
            }
        }
        if (currentScreen == "Screen1" || currentScreen == "Screen2")
        {
            txtScores.text = Enemy.enemyRemains.ToString();
        }
        
    }


    private void FixedUpdate()
    {
        Vector3 facing = transform.localScale;
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        if (h > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            anim.SetBool("Run", true);
            facing.x = 0.3f;
            transform.localScale = facing;
           

        }
        else if (h < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
            anim.SetBool("Run", true);
            facing.x = -0.3f;
            transform.localScale = facing;
          

        }
        if (v > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * jump);
            anim.SetBool("Jump", true);
           

        }
        else
        {
            anim.SetBool("Jump", false);
        }

        if (h == 0 && v == 0)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string nameColision = collision.gameObject.tag;
        if (nameColision == "DeadZone")
        {
            SceneManager.LoadScene("GameOver");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string nameCol = collision.gameObject.tag;
        if (nameCol == "Screen2")
        {
            if (Enemy.enemyRemains == 0)
            {
                StartCoroutine(updateUser(UserInfo.UserID, "2", Enemy.coins.ToString(), "Screen2"));
            }
            else
            {
                txtAlert.gameObject.SetActive(true);
            }

        }
        if (nameCol == "Enemy")
        {
            maxHealth -= 1;
            healthBar.SetHealth(maxHealth);
            PlayerSounds.PlaySounds("hurt");
            if (maxHealth <= 0)
            {
                PlayerSounds.PlaySounds("die");
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Attack>().enabled = false;
                this.enabled = false;

                anim.SetBool("Die", true);
            }
        }
        if (nameCol == "Heart")
        {
            PlayerSounds.PlaySounds("health");
            maxHealth += 1;
            healthBar.SetHealth(maxHealth);
            if (maxHealth == 8)
            {
                maxHealth = 8;
            }
            Destroy(collision.gameObject);
        }
        if(nameCol == "BossKey")
        {
            if (Enemy.enemyRemains == 0)
            {

                Destroy(collision.gameObject);
                StartCoroutine(updateUser(UserInfo.UserID, "3", Enemy.coins.ToString(), "Screen3"));
            }
            else
            {
                txtAlert.gameObject.SetActive(true);
            }
        }
        if(nameCol == "FireBall")
        {
            Destroy(collision.gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            isShop = true;
          

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Screen2")
        {
            txtAlert.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "BossKey")
        {
            txtAlert.gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Shop")
        {
            isShop = false;
            Shop.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        maxHealth -= damage;

        healthBar.SetHealth(maxHealth);

        if (maxHealth <= 0)
        {
            PlayerSounds.PlaySounds("die");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Attack>().enabled = false;
            this.enabled = false;
            anim.SetBool("Die", true);
        }
    }
    public void GameOver()
    {

        SceneManager.LoadScene("GameOver");
    }

    IEnumerator updateUser(string id, string level, string coins ,string screens)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("level", level);
        form.AddField("coins", coins);
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/assignment/update_user.php", form))
        {
            yield return www.SendWebRequest();
            string result = www.downloadHandler.text;
            clg(result);
            SceneManager.LoadScene(screens);
        }
    }
    void clg(string s)
    {
        Debug.Log(s);
    }
}
//