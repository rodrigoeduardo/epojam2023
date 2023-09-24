using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daughter : MonoBehaviour 
{
    public AudioClip soundWalking;
    public static string DAUGHTER_WALKING = "DaughterWalking";

    private Animator animator;
    private SpriteRenderer sprite;
    [SerializeField] private float rotationSpeed = 1.0f; // Velocidade de rotação no quarto
    [SerializeField] private string currentRoom = "SALA"; // Inicialmente na sala
    [SerializeField] private float radius = 10.0f; // Raio do círculo

    private Vector3 targetPosition; // Posição alvo para a cozinha
    private Vector3 centerPosition; // Posição central para o quarto
    private float moveSpeed = 2.0f; // Velocidade de movimento

    private float angle = 0f; // Ângulo atual

    public void Start() {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        centerPosition = transform.position; // Define a posição central como a posição inicial
        targetPosition = centerPosition; // Define a posição alvo inicial como a posição central
    }

    public void Update() {
        // Verifica a sala atual e toma decisões com base nela
        switch (currentRoom) {

            case "SALA":
                Idle();
                break;

            case "QUARTO-PAI":
                MoveInCircle();
                break;

            case "QUARTO-FILHA":
                MoveToMouseCursor();
                break;

            default:
                Debug.LogError("Sala desconhecida: " + currentRoom);
                break;
        }
    }

    // Move o NPC em um trajeto circular no quarto
    private void MoveInCircle()
    {
        animator.SetBool(DAUGHTER_WALKING, true); // Define o parâmetro DAUGHTER_WALKING para true

        // Atualiza a posição do NPC no círculo
        angle += rotationSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
        transform.position = centerPosition + offset;
        sprite.flipX = offset.x > 0f;
        AudioManager.instance.PlayAudio(soundWalking);
    }

    // Faz o NPC ficar parado (IDLE)
    private void Idle()
    {
        if (animator.GetBool(DAUGHTER_WALKING)) {
            animator.SetBool(DAUGHTER_WALKING, false); // Define o parâmetro DAUGHTER_WALKING para false
        }
    }

    // Move o NPC em direção ao cursor do mouse na cozinha
    private void MoveToMouseCursor()
    {
        animator.SetBool(DAUGHTER_WALKING, true); // Define o parâmetro DAUGHTER_WALKING para true

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Certifique-se de que o NPC está no mesmo plano Z do mouse

        targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z); // Atualiza a posição alvo para o cursor do mouse

        // Move o NPC em direção à posição alvo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Ajusta o flip do sprite com base na direção do movimento
        sprite.flipX = targetPosition.x < transform.position.x;
        AudioManager.instance.PlayAudio(soundWalking);
    }
}
