
using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar el espacio de nombres para manejar escenas

public class SkullHealth : MonoBehaviour, IConsume
{
    [Range(0, 100)]
    public float Health;
    public float MaxHealth = 100;

    public static Action<float> OnChangeHealth;

    private void Start()
    {
        Health = MaxHealth;
        OnChangeHealth?.Invoke(Health / MaxHealth);
    }

    private void OnMouseDown()
    {
        TakeDamage(10); // Reducir la salud al hacer clic
    }

    // Implementación de IConsume
    public void Use(ConsumableItem item)
    {
        if (item is ItemPotion potion)
        {
            Health += potion.HealthPoints;
            Health = Mathf.Clamp(Health, 0, MaxHealth);

            // Notificar el cambio de salud
            OnChangeHealth?.Invoke(Health / MaxHealth);

            Debug.Log($"Health restored! Current health: {Health}");
        }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, MaxHealth); // Asegurarse de que la salud no sea negativa

        // Notificar el cambio de salud
        OnChangeHealth?.Invoke(Health / MaxHealth);

        // Verificar si la salud es menor o igual a 0
        if (Health <= 0)
        {
            Die(); // Llamar al método Die() cuando la salud llega a 0
        }
    }

    // Método para manejar la muerte del jugador
    private void Die()
    {
        Debug.Log("Player has died! Loading ending scene...");

        // Cargar la escena "Ending"
        SceneManager.LoadScene("Ending");
    }
}