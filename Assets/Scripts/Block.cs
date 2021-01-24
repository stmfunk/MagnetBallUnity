using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject shatterVFX;
    [SerializeField] Sprite[] hitSprites;

    [NonSerialized] LevelGamePlay gamePlay;
    GameState gameState;

    int timesHit = 0;

    private void Start() {
        gamePlay = FindObjectOfType<LevelGamePlay>();
        if (tag == "Breakable") gamePlay.AddBlock();
        gameState = FindObjectOfType<GameState>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision) {
        if (tag == "Breakable") HandleHit();
    }

    private void HandleHit() {
        timesHit++;
        if (timesHit >= hitSprites.Length) {
            DestroyBlock();
        } else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        try
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit % hitSprites.Length];

        } catch (NullReferenceException err) {
            Debug.LogError("No sprite in array for " + gameObject.name);
        } catch (IndexOutOfRangeException err) {
            Debug.LogError("Sprite Array too short for hitCount");
        }
    }

    private void DestroyBlock() {
          AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
          gameState.AddToScore();
          gamePlay.BlockBroken();
          TriggerShatterVFX();
          Destroy(gameObject);
    }

    private void TriggerShatterVFX() {
        GameObject shatter = Instantiate(shatterVFX, transform.position, transform.rotation);
        Destroy(shatter, 1f);
    }
}
