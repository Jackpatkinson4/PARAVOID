using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ParavoidUI;

public class MonsterAttack : MonoBehaviour
{
    public Animator anim;
    public Sprite[] goodEndSprites;
    public Sprite[] badEndSprites;
    private TextProducer dialogue;
    private CanvasGroup canvasFade;
    private Image faderImage;
    private Fader fader;
    private AudioManager audioManager;
    private Color fadeColorBox;

    public void Awake()
    {
        dialogue = GameObject.Find("DialogueText").GetComponent<TextProducer>();
        canvasFade = GameObject.Find("Fader").GetComponent<CanvasGroup>();
        faderImage = GameObject.Find("Fader").GetComponent<Image>();
        fader = GameObject.Find("VisualCanvas").GetComponent<Fader>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        fadeColorBox = faderImage.color;
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        Player player = collider.GetComponent<Player>();
        player.gameObject.GetComponent<PlayerController>().DisableControls();
        if (player != null)
        {
            anim.SetBool("hasMemories", player.Memories >= 3);
        }

        anim.SetBool("playerClose", true);

        StartCoroutine(EnableFate(player, player.Memories >= 3));
    }

    private IEnumerator EnableFate(Player player, bool good)
    {
        audioManager.Play("thoomp"); 
        fader.MemoryFading(1f, 10f);
        yield return new WaitForSeconds(2f);

        fadeColorBox = new Color(255f, 255f, 255f);
        faderImage.color = fadeColorBox;

        if (good) //Good ending stuff
        {
            dialogue.ReplaceText("June! I remember her.", Effect.Type, 0.05f);
            fadeColorBox = new Color(0f, 0f, 0f);
            faderImage.color = fadeColorBox;
            // faderImage.sprite = goodEndSprites[0];
            yield return new WaitForSeconds(2f);
            dialogue.ReplaceText("She is my sister...", Effect.Type, 0.05f);
            yield return new WaitForSeconds(2.5f);
            dialogue.ReplaceText("She", Effect.Type, 0.1f);
            yield return new WaitForSeconds(1f);
            dialogue.RunText(" is my sister...", Effect.Type, 0.05f);
            yield return new WaitForSeconds(2f);
            dialogue.ReplaceText("She moved away.", Effect.Type, 0.05f);
            yield return new WaitForSeconds(2f);
            dialogue.RunText("I was visiting her.", Effect.Type, 0.05f);
            yield return new WaitForSeconds(3f);
            dialogue.ReplaceText("We were going to the mall, to get a game.", Effect.Type, 0.045f);
            yield return new WaitForSeconds(3f);
            dialogue.ReplaceText("But then...", Effect.Type, 0.1f);
            yield return new WaitForSeconds(1.5f);
            dialogue.RunText("what...", Effect.Type, 0.07f);
            yield return new WaitForSeconds(1.5f);
            dialogue.RunText("what happened...", Effect.Type, 0.07f);
            yield return new WaitForSeconds(1.3f);
            dialogue.ReplaceText("June?", Effect.Type, 0.1f);
            yield return new WaitForSeconds(1f);
            dialogue.ReplaceText("Oh... June.", Effect.Type, 0.1f);
            yield return new WaitForSeconds(2f);
            SceneLoader.LoadScene("GoodEndingMaster");
        }
        else if (!good) //Bad ending stuff
        {
            dialogue.ReplaceText("She... I... Know her...", Effect.Type, 0.05f);
            fadeColorBox = new Color(0f, 0f, 0f);
            faderImage.color = fadeColorBox;
            // faderImage.sprite = badEndSprites[0];
            yield return new WaitForSeconds(4f);
            dialogue.ReplaceText("Wait... where are we going...", Effect.Type, 0.05f);
            yield return new WaitForSeconds(3f);
            dialogue.ReplaceText("...", Effect.Type, 0.1f);
            yield return new WaitForSeconds(1f);
            dialogue.ReplaceText("Hello?", Effect.Type, 0.1f);
            yield return new WaitForSeconds(2f);
            SceneLoader.LoadScene("BadEndingMaster");
        }

        /*---Having Only Black---- 
            fadeColorBox = new Color(0f, 0f, 0f);
            faderImage.color = fadeColorBox;
            right after that above is used, if you want to proccedd with a sprite image change
            fadeColorBox = new Color(255f, 255f, 255f);
            faderImage.color = fadeColorBox;
          */  
        
        
    }



}
