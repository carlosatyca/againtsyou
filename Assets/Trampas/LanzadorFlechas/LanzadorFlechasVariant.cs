using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzadorFlechasVariant : MonoBehaviour
{
    public Sprite sprite;
    public PlayerHealth player;

    private void Update()
    {
        if (player.currentHealth <= 0)
        {
            GetComponent<Animator>().SetBool("Attack", false);
        }
    }

    public void shootArrow()
    {
        GameObject arrow = new GameObject("arrow");
        arrow.transform.SetParent(gameObject.transform);
        arrow.layer = LayerMask.NameToLayer("Map");
        arrow.transform.localPosition = new Vector3(1.8f, -0.34f, 0f);

        arrow.AddComponent<SpriteRenderer>();
        arrow.AddComponent<BoxCollider2D>();
        arrow.AddComponent<LanzadorFlechas_Hitbox>();
        arrow.AddComponent<Rigidbody2D>();

        arrow.GetComponent<LanzadorFlechas_Hitbox>().player = player;

        arrow.GetComponent<SpriteRenderer>().flipX = true;
        arrow.GetComponent<SpriteRenderer>().sprite = sprite;

        arrow.GetComponent<BoxCollider2D>().isTrigger = true;
        arrow.GetComponent<BoxCollider2D>().offset = new Vector2(0.006317139f, 0.002487183f);
        arrow.GetComponent<BoxCollider2D>().size = new Vector2(1.841858f, 0.2231293f);

        arrow.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        arrow.GetComponent<Rigidbody2D>().mass = 0.1f;
        arrow.GetComponent<Rigidbody2D>().gravityScale = 0f;
        arrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(75f, 0f));

        StartCoroutine(waitToDestroyArrow(arrow));
    }

    IEnumerator waitToDestroyArrow(GameObject arrow)
    {
        yield return new WaitForSeconds(7f);
        Destroy(arrow);
    }
}
