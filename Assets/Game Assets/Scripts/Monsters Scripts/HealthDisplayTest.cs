using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;
public class HealthDisplayTest : NetworkBehaviour
{
    public HealthTest healthTest;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    internal object s;


    //public override void OnStartClient()
    //{ 
    //    base.OnStartClient();
    //    healthTest.SetHealth(healthTest.currentHealth);
    //    HandleHealthChanged(healthTest.currentHealth);
    //}

    public void OnEnable()
    {
        healthTest.EventHealthChanged += HandleHealthChanged;
    }

    public void OnDisable()
    {
        healthTest.EventHealthChanged -= HandleHealthChanged;

    }
    public void Start()
    {
        slider.maxValue = healthTest.maxHealth;
        slider.value = healthTest.maxHealth;
        //3shan ybd2 a5dr
        fill.color = gradient.Evaluate(1f);
    }

    public void Update()
    {
        slider.value = healthTest.currentHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //[ClientRpc]
    public void HandleHealthChanged(int currentHealth)
    {
        Debug.Log("Setting Health UI");
        //slider.value = currentHealth;
        //Debug.Log("Current Health: "+ currentHealth);
        //Debug.Log("Slider Value: " + slider.value);
        //fill.color = gradient.Evaluate(slider.normalizedValue);

    }
}
