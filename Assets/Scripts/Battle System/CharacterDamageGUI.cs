using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterDamageGUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] float lifetime = 1f, movespped = 1f, textVibration = 0.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
        transform.position += new Vector3(0f, movespped * Time.deltaTime);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        float jitterAmount = UnityEngine.Random.Range(-textVibration, textVibration);
        transform.position += new Vector3(jitterAmount, jitterAmount, 0f);
    }
}
