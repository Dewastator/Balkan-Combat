using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve healthBarAnimation;

    [SerializeField]
    private Image healthBarPlayer, healthBarPlayer2;

    [SerializeField]
    private LayerMask player;

    [SerializeField]
    private float animationSpeed;
    public void DecreaseHealthStart(float currentHealth, float maxHealth, LayerMask layerMask)
    {
        StartCoroutine(DecreaseHealthBar(currentHealth, maxHealth, layerMask));
    }
    public IEnumerator DecreaseHealthBar(float currentHealth, float maxHealth, LayerMask layerMask)
    {
        float time = 0;
        var normalized = Helper.Map(currentHealth, 0, maxHealth, 0, 1);
        while (time < healthBarAnimation[healthBarAnimation.length - 1].time)
        {
            time += Time.deltaTime * animationSpeed;

            //var lerp = Mathf.Lerp(maxHealth, currentHealth, healthBarAnimation.Evaluate(time));

            if (layerMask == player)
            {
                healthBarPlayer.fillAmount = Mathf.Lerp(healthBarPlayer.fillAmount, normalized, healthBarAnimation.Evaluate(time));
            }
            else
            {
                healthBarPlayer2.fillAmount = Mathf.Lerp(healthBarPlayer2.fillAmount, normalized, healthBarAnimation.Evaluate(time));
            }

            yield return null;
        }
    }
}
