using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Johnathan Spangler
 * 11/11/2025
 * Controls the game's UI
 */

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI UI, instUI;
    public string template, lastUp1, lastUp2/*, lastUp3*/;

    public int lastLives;

    public float delay = 10f, fadeDuration = 2f;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<PlayerController>();
        StartCoroutine(HideInstructions());
        template = UI.text;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerLives != lastLives || player.jumpUpgrade.ToString() != lastUp1 || player.bulletUpgrade.ToString() != lastUp2/* || player.ballUpgrade.ToString() != lastUp3*/)//If elements change
        {
            UpdateUI();
        }
    }

    /// <summary>
    /// Makes sure the ui has no empty spaces between upgrade lines, regardless of when the player collects them
    /// </summary>
    public void UpdateUI()
    {
        lastLives = player.playerLives;

        //Jump Upgrade UI
        if (player.jumpUpgrade)
        {
            lastUp1 = "* Higher Jump";
        }
        else
        {
            lastUp1 = "None";
        }

        //Big Bullet Upgrade UI
        if (player.bulletUpgrade && player.jumpUpgrade)
        {
            lastUp2 = "* Bigger Bullet";
        }
        else if (player.bulletUpgrade && !player.jumpUpgrade)
        {
            lastUp1 = "* Bigger Bullet";
            lastUp2 = "";
        }

        //Ball Form Upgrade UI
        /*if (player.ballUpgrade && player.bulletUpgrade && player.jumpUpgrade)
        {
            lastUp3 = "* Ball Form";
        }
        else if (player.ballUpgrade && !player.bulletUpgrade && !player.jumpUpgrade)
        {
            lastUp1 = "* Ball Form";
            lastUp2 = "";
            lastUp3 = "";
        }
        else if (player.ballUpgrade && player.bulletUpgrade && !player.jumpUpgrade)
        {
            lastUp2 = "* Ball Form";
            lastUp3 = "";
        }
        else if (player.ballUpgrade && !player.bulletUpgrade && player.jumpUpgrade)
        {
            lastUp2 = "* Ball Form";
            lastUp3 = "";
        }*/

        UI.text = string.Format(template, lastLives, lastUp1, lastUp2/*, lastUp3*/);
    }

    /// <summary>
    /// Start fading out the instructions after 10 seconds
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideInstructions()
    {

        Color original = instUI.color;
        original.a = 1f;//ensure alpha is set to 1
        instUI.color = original;

        yield return new WaitForSeconds(delay);

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float lerpTime = Mathf.Clamp01(time / fadeDuration);

            // Lerp alpha from 1 to 0
            Color color = instUI.color;
            color.a = Mathf.Lerp(1f, 0f, lerpTime);
            instUI.color = color;

            yield return null;
        }

        // Fix issue where occasionally it wasn't fully transparent at the end
        Color end = instUI.color;
        end.a = 0f;
        instUI.color = end;//This comment is pointless, I just wish I could just do `instUI.color.a = 0f` .. I really hope this is changed in future versions of the engine
    }
}
