using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Items
{
    Bomb,Shield,Gun
}

public class ItemAreaScript : MonoBehaviour
{
    [SerializeField] private GameObject Bomb,Shield,Gun;
    private bool[] ItemSlotAvailability = new bool[4] { true, true, true, true };
    [SerializeField] private float InitialY;
    [SerializeField] private short Upward;
    private int ShieldSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Items item)
    {
        int slot = GetNextAvailableSlot();
        ItemSlotAvailability[slot] = false;
        switch (item)
        {
            case Items.Bomb:
                StartCoroutine(SpawnItem(Bomb, slot, 10));
                break;
            case Items.Shield:
                ShieldSlot = slot;
                StartCoroutine(SpawnItem(Shield, slot, 10));
                break;
            case Items.Gun:
                StartCoroutine(SpawnItem(Gun, slot, 10));
                break;
        }
    }

    public void RemoveAllItems()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < ItemSlotAvailability.Length; i++)
        {
            ItemSlotAvailability[i] = true;
        }

    }

    public void RemoveShield()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Shield")
            {
                Destroy(child.gameObject);
            }
        }
        ItemSlotAvailability[ShieldSlot]=true;
    }

    private IEnumerator SpawnItem(GameObject prefab, int slot, int delaySeconds)
    {
        Vector3 slotPos = new Vector3(0, InitialY + slot*Upward, 0);
        GameObject obj = Instantiate(prefab, transform.position+slotPos , Quaternion.identity, transform);
        yield return new WaitForSeconds(delaySeconds);
        // Žc‚è‚R•b‚Å“_–Å‚Æ‚©‚à‚ ‚è‚©‚à
        Destroy(obj);
        ItemSlotAvailability[slot] = true;
    }

    private int GetNextAvailableSlot()
    {
        for (int i = 0; i < ItemSlotAvailability.Length; i++)
        {
            if (ItemSlotAvailability[i])
            {
                return i;
            }
        }
        return -1;
    }
}
