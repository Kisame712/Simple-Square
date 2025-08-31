using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Door : MonoBehaviour
{
    public AudioClip doorSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(LevelChange());
        }
    }

    IEnumerator LevelChange()
    {
        EffectSoundManager.Instance.PlaySoundEffect(doorSound);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
