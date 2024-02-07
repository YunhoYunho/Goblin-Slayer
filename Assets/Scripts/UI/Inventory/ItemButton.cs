using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ItemKeyType { Alpha1, Alpha2 }

public class ItemButton : MonoBehaviour
{
    [SerializeField]
    private ItemKeyType itemKeyType;
    [SerializeField]
    private PlayerHealth player;
    [SerializeField]
    private ItemData itemValue;
    [SerializeField]
    private InventoryItem itemInSlot;
    [SerializeField]
    private TimerSlider timerSlider;
    [SerializeField]
    private ParticleSystem healingEffect;

    private void Update()
    {
        switch (itemKeyType)
        {
            case ItemKeyType.Alpha1:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    OnClickedItemButton();
                }
                break;
            case ItemKeyType.Alpha2:
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    OnClickedItemButton();
                }
                break;
        }
    }

    public void OnClickedItemButton()
    {
        if (transform.childCount <= 0)
            return;

        itemInSlot = GetComponentInChildren<InventoryItem>();
        itemValue = itemInSlot.item;

        if (itemValue != null && itemValue.itemType != ItemType.Weapon)
        {
            switch (itemValue.potionType)
            {
                case PotionType.HP:
                    player.RestoreHP(itemValue.abilityValue, itemValue.during);
                    InventoryManager.Instance.UseItem(itemInSlot);
                    healingEffect.Play();
                    SoundManager.Instance.PlaySFX(SFX.RestoreHP);
                    break;
                case PotionType.AttackSpped:
                    timerSlider.SetMaxValue(itemValue.during);
                    timerSlider.StartDuration(itemValue.during);
                    StartCoroutine(AttakSpeedRoutine(itemValue.abilityValue, itemValue.during));
                    InventoryManager.Instance.UseItem(itemInSlot);
                    break;
            }
        }
    }

    private IEnumerator AttakSpeedRoutine(float speed, float during)
    {
        player.anim.SetFloat("AttackSpeed", speed);
        yield return new WaitForSeconds(during);
        player.anim.SetFloat("AttackSpeed", 1f);
    }
}