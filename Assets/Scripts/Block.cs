using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config parameters
    [SerializeField] AudioClip[] breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    //Cached Reference
    Level level;
    GameSession gameSession;

    //State Variables
    [SerializeField] int timesHit; //only serialized for debugging purposes

    private void Start()
    {
        CountBreakableBlocks();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
            level.CountBlocks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable" || tag == "Invisible")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
            DestroyBlock();
        else
        {
            AudioSource.PlayClipAtPoint(breakSound[1], Camera.main.transform.position);
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        else
            Debug.LogError("Block sprite is missing from the array " + gameObject.name);
    }

    private void DestroyBlock()
    {
        PlayDestroyBlockSFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayDestroyBlockSFX()
    {
        gameSession.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound[0], Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);// why we added rotation
        Destroy(sparkles, 1.0f);
    }
}
