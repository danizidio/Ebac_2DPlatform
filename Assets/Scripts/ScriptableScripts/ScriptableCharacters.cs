using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Class Name", menuName = "Player Class", order = 1)]
public class ScriptableCharacters : ScriptableObject
{
    [Tooltip("Nome do Personagem")]
    [SerializeField] string _characterName;
    public string characterName {get{return _characterName;}}

    [Tooltip("Quantidade máxima de pontos de vida do Player")]
    [SerializeField] float _maxHealth;
    public float maxHealth
    {
        get { return _maxHealth; }
    }

    [Tooltip("Quantidade máxima de pontos de habilidade do Player")]
    [SerializeField] float _maxMana;
    public float maxMana
    {
        get { return _maxMana; }
    }

    [Tooltip("A força do ataque do Player")]
    [SerializeField] int _damage;
    public int damage
    {
        get { return _damage; }
    }

    [Tooltip("Desesa do Player")]
    [SerializeField] int _playerArmor;
    public int playerArmor
    {
        get { return _playerArmor; }
    }

    [SerializeField] float _speed;
    public float speed
    {
        get { return _speed; } 
    }
    [SerializeField] float _speedJump;
    public float speedJump
    {
        get { return _speedJump; }
    }

    [Tooltip("Quantidade máxima de pontos de habilidade do Player")]
    [SerializeField] float _manaRegen;
    public float manaRegen
    {
        get { return _manaRegen; }
    }

    [Tooltip("Chance de Dano Critico")]
    [SerializeField] float _criticalChance;
    public float criticalChance
    {
        get { return _criticalChance; }
    }

    [Header("Custo de Uso Habilidades")]
    [SerializeField]
    int _costMobility;
    public int costMobility { get { return _costMobility; } }

    [SerializeField] int _costShadow;
    public int costShadow { get { return _costShadow; } }


    float _count = 5;
    public float count
    {
        get { return _count; }
    }

    [SerializeField] GameObject _lifeBar;
    public GameObject lifeBar => _lifeBar;


    [SerializeField] GameObject _damageText;
    public GameObject damageText 
    { 
        get { return _damageText; } 
    }
}
