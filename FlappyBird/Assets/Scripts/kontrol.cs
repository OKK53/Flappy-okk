using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class kontrol : MonoBehaviour {
   
    //karakter kontrol kodlari
    [SerializeField]
    Sprite[] KusSprite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;
    int puan = 0;
    int enYuksekPuan = 0;
    Rigidbody2D fizik;

    [SerializeField]
    Text puanText;

    bool oyunbitti = true;

    OyunKontrol oyunKontrol;
    AudioSource []sesler;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontrol").GetComponent<OyunKontrol>();
        sesler = GetComponents<AudioSource>();
        enYuksekPuan = PlayerPrefs.GetInt("enYuksekPuanKayit");
    }
		
	void Update ()  
    {
        //debug.log(fizik.velocity); //yercekimi artıyor zamanla hızartıyor
        if(Input.GetMouseButtonDown(0) && oyunbitti)
        {
            fizik.velocity = new Vector2(0, 0);//hizi sifir yaptik.
            fizik.AddForce(new Vector2(0,200));//sonra kuvvet uyguladik.
            sesler[0].Play();
        }
        if (fizik.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        Animasyon();
	}
    void Animasyon()
    {
        kusAnimasyonZaman += Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)//1saniyede bir buraya giriyor.
        {
            kusAnimasyonZaman = 0;
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--; //2ser kere tekrarın onune gecmek icin
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;//2ser kere tekrarın onune gecmek icin
                    ileriGeriKontrol = true;
                }
            }
        }
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="puan")
        {
            sesler[1].Play();
            puan++;
            puanText.text = "Puan = " + puan;
            // Debug.Log(puan);
        }
        if (col.gameObject.tag == "engel")
        {
            oyunbitti = false;
            oyunKontrol.oyunbitti();
            sesler[2].Play();
            GetComponent<CircleCollider2D>().enabled = false;

            if (puan>enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("enYuksekPuanKayit", enYuksekPuan);
            }
            Invoke("anaMenuyeDon", 1.5f);
        }
    }
    void anaMenuyeDon()
    {
        PlayerPrefs.SetInt("puanKayit", puan);
        SceneManager.LoadScene("anaMenu");
    }
}
