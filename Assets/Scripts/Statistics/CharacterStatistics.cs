using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Statistics
{
    public class CharacterStatistics : NetworkBehaviour
    {
        new public string name = "New character";
        public int MaximumHealth = 100;
        [SyncVar(hook = "OnChangedHealth")] public int CurrentHealth;
        public float Weight = 75;
        
        public Statistic Strength;
        public Statistic Intelligence;
        public Statistic Agility;
        public Statistic Precision;
        public Statistic Armor;

        public bool Damaged = false;
        public bool IsDead = false;
        
        public delegate void OnHealthChanged(int currentHealth, int formerHealth);
        public OnHealthChanged RpcOnHealthChangedCallback;

        public Inventory inventory;

        private PlayerHealth _playerHealth;
        
        public Animator anim;
        public GameObject gameManager;
        public GlobalGameVariables GlobalVariables;
        public BloodManager BloodManager;
        public SoundManager SoundManagerZombie;

        private void Awake()
        {
            BloodManager = GetComponent<BloodManager>();
            SoundManagerZombie = GetComponent<SoundManager>();
            //GlobalVariables = gameManager.GetComponent<GlobalGameVariables>();
                GlobalVariables = GameObject.Find("GameManager").GetComponent<GlobalGameVariables>();

            inventory = GetComponent<Inventory>();
            anim = gameObject.GetComponent<Animator>();

            CurrentHealth = MaximumHealth;
            _playerHealth = GetComponent<PlayerHealth>();
        }

        public void Heal(int h)
        {
            if(CurrentHealth + h > MaximumHealth)
            {
                CurrentHealth = MaximumHealth;
            } else
            {
                CurrentHealth += h;
            }
        }

        public void TakeDamage(int damage)
        {
            if (!isServer || IsDead)
                return;
            
            int formerHealth = CurrentHealth;
            damage -= Armor.GetValue();
            damage = Math.Max(0, damage);

            CurrentHealth -= damage;
            
            Debug.Log(name + " took " + damage + " damage. Former health: " + formerHealth);

            Debug.Log("Invoking callbacks...");
            
            if(BloodManager != null)
                BloodManager.Play();

            if (SoundManagerZombie != null)
                SoundManagerZombie.play_hitting();
            
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            // Die in some way.
            Debug.LogError(name + " died.");
            if (isLocalPlayer && gameObject.CompareTag("Player") && !IsDead)
            {
                IsDead = true;
                anim.SetTrigger("dead");
                StartCoroutine(WaitAfterDie());
            }
        }

        public IEnumerator WaitAfterDie()
        {
            Debug.LogError(name + " attend.");
            yield return new WaitForSeconds(5);
            Application.LoadLevel("GameOver");
        }
        
        public float GetCarriableWeight()
        {
            return (float) (0.01 * Strength.GetValue() * inventory.maximumWeight);
        }
        
        void OnChangedHealth (int health)
        {
            if(isLocalPlayer && _playerHealth != null)
                _playerHealth.RpcOnHealthChangedCallback(health, health);
        }
    }
}