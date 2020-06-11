using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSpawner : MonoBehaviour
{
    public Transform spawnTransformPos;
    public GameObject spritePrefab;
    public Sprite sprite;
    public GameObject newSprite;
    public int fadeTime = 6;
    public bool isSpriteSpawned;


    void Start()
    {
        isSpriteSpawned = false;
        if (spawnTransformPos == null)
        {
            spawnTransformPos = transform;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            SpawnImage();
        }
    }

    public void SpawnImage()
    {
        if (isSpriteSpawned == false)
        {
            isSpriteSpawned = true;
            newSprite = (GameObject)Instantiate(spritePrefab, spawnTransformPos);
            Material material = newSprite.GetComponent<SpriteRenderer>().material;
            Debug.Log("Image spawned");

            StartCoroutine(FadeTimer(material));
        }
    }

    IEnumerator FadeTimer(Material _material)
    {
        int fadeTimer = fadeTime;
        while(fadeTimer > 0)
        {
            yield return new WaitForSeconds (1);
            fadeTimer--;
        }
        StartCoroutine(FadeSprite(_material));
    }
    
    IEnumerator FadeSprite(Material _material)
    {
        Color color = _material.color;
        while (color.a > 0.0f)
        {
            color.a -= 1.0f * Time.deltaTime;
            _material.color = color;
            if(color.a <= 0)
            {
                Destroy(newSprite);
            }
            yield return null;
        }
       
    }
}
