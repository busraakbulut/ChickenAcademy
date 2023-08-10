using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Incubation : MonoBehaviour
{
    public event Action OnHatched;

    [SerializeField] private TextMeshProUGUI hatchCountDownText;

    [SerializeField] private GameObject egg;

    [SerializeField] private float hatchTime;

    [SerializeField] private Animator eggAnim;

    private AllyUnit members;

    private SoliderSpawnPoints spawnPoints;

    private float hatchTimer;
    public bool HasEgg { get; private set; }

    private void Awake()
    {
        egg.GetComponent<Egg>().OnCrackedAnimComplated += Egg_OnCrackedAnimComplated;

        DisableEgg();

        HasEgg = false;
    }

    private void Egg_OnCrackedAnimComplated()
    {
        OnHatched?.Invoke();

        StartCoroutine(DisableEggAnimation());
    }

    public void TakeEgg()
    {
        // Already has an egg
        if (HasEgg)
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

        eggAnim.SetBool("Crack",true);
    }

    private void UpdateCountDownText()
    {
        hatchCountDownText.text = (hatchTime - hatchTimer).ToString("0");
    }

    private void EnableEgg()
    {
        egg.SetActive(true);
        
        eggAnim.SetBool("Crack", false);

        HasEgg = true;

        hatchTimer = 0;

        hatchCountDownText.gameObject.SetActive(true);

        UpdateCountDownText();

        StartCoroutine(Hatch());
    }

    private void DisableEgg()   
    {
        if (egg.gameObject.activeSelf)
        {
            eggAnim.SetBool("Crack", false);

            egg.SetActive(false);
        }

        hatchCountDownText.gameObject.SetActive(false);
    }

    private IEnumerator DisableEggAnimation()
    {
        DisableEgg();
        
        yield return new WaitForSeconds(5);

        HasEgg = false;
    }

    public void SetUnit(AllyUnit members)
    {
        this.members = members;

        spawnPoints = GetComponent<SoliderSpawnPoints>();

        if(spawnPoints != null )
        {
            spawnPoints.Init(members.MaxMember);
        }
    }



}
