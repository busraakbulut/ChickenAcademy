using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Incubation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hatchCountDownText;

    [SerializeField] private GameObject egg;

    [SerializeField] private GameObject chicken;

    [SerializeField] private float hatchTime;

    [SerializeField] private ObjectPool chickenPool;

    private Animator eggAnim;

    private AllyUnit members;

    private float hatchTimer;

    private bool hasEgg;

    private void Awake()
    {
        eggAnim = egg.GetComponent<Animator>();

        egg.GetComponent<Egg>().OnCrackedAnimComplated += Egg_OnCrackedAnimComplated;

        DisableEgg();
    }

    private void Egg_OnCrackedAnimComplated()
    {
        DisableEgg();

        chickenPool.TakeObject().transform.position = transform.position;

        members.ActualMember++;
    }

    public void TakeEgg()
    {
        // Already has an egg
        if (hasEgg)
        {
            return;
        }

        // Maximum Capacity
        if(members.IsFull())
        {
            return;
        }

        EnableEgg();
    }

    private IEnumerator Hatch()
    {
        while (hatchTimer < hatchTime)
        {
            yield return new WaitForSeconds(1);

            hatchTimer++;

            UpdateCountDownText();
        }

        UpdateCountDownText();

        eggAnim.SetTrigger("OnCracked");
    }

    private void UpdateCountDownText()
    {
        hatchCountDownText.text = (hatchTime - hatchTimer).ToString("0");
    }

    private void EnableEgg()
    {
        hatchTimer = 0;

        hasEgg = true;

        egg.SetActive(true);

        hatchCountDownText.gameObject.SetActive(true);

        UpdateCountDownText();

        StartCoroutine(Hatch());
    }

    private void DisableEgg()
    {
        hasEgg = false;

        egg.SetActive(false);

        hatchCountDownText.gameObject.SetActive(false);
    }


    public void SetUnit(AllyUnit members)
    {
        this.members = members;

        chickenPool = new ObjectPool(members.MaxMember, chicken, transform);
    }

    public int GetMaxUnitNumber() => members.MaxMember;
}
