using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;
public class HealthBarDisplay : NetworkBehaviour
{
    public HealthBar healthBar;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public Slider sliderCanvas;
    public Gradient gradientCanvas;
    public Image fillCanvas;


    //public override void OnStartClient()
    //{ 
    //    base.OnStartClient();
    //    healthTest.SetHealth(healthTest.currentHealth);
    //    HandleHealthChanged(healthTest.currentHealth);
    //}

    public void OnEnable()
    {
        healthBar.EventHealthChanged += HandleHealthChanged;
    }

    public void OnDisable()
    {
        healthBar.EventHealthChanged -= HandleHealthChanged;

    }
    public void Start()
    {
        slider.maxValue = healthBar.maxHealth;
        slider.value = healthBar.maxHealth;
        //3shan ybd2 a5dr
        fill.color = gradient.Evaluate(1f);

        sliderCanvas.maxValue = healthBar.maxHealth;
        sliderCanvas.value = healthBar.maxHealth;
        //3shan ybd2 a5dr
        fillCanvas.color = gradientCanvas.Evaluate(1f);
    }

    public void Update()
    {
        slider.value = healthBar.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        sliderCanvas.value = healthBar.currentHealth;
        fillCanvas.color = gradientCanvas.Evaluate(sliderCanvas.normalizedValue);
    }

    public void HandleHealthChanged(int currentHealth)
    {
        //Debug.Log("Setting Health UI");
        //slider.value = currentHealth;
        //Debug.Log(this.transform.gameObject.name + slider.value);

        //Debug.Log("Current Health: " + currentHealth);
        //Debug.Log("Slider Value: " + slider.value);
        //fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
