using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameResourceManager : MonoBehaviour
{

    private Player player;
    [SerializeField] private TMP_Text currencyText;

    [Header("Oxygen")]
    [SerializeField] private Button oxygenUpdateButton;
    [SerializeField] private TMP_Text oxygenPriceText;
    [SerializeField] private TMP_Text oxygenLevelText;
    [SerializeField] private Image  oxygenLevelMeterImage;

    [Header("Rope")]
    [SerializeField] private Button ropeUpdateButton;
    [SerializeField] private TMP_Text ropePriceText;

    [Header("Speed")]
    [SerializeField] private Button speedUpdateButton;
    [SerializeField] private TMP_Text speedPriceText;

    [Header("Power")]
    [SerializeField] private Button powerdUpdateButton;
    [SerializeField] private TMP_Text powerPriceText;


    private int priceIncreaseFactor = 30;
    private int priceIncreaseRatio = 100;
    // Start is called before the first frame update
    void Start()
    {
        ResetData();
    }

    void ResetData()
    {
        player = GameManager.Instance.Data.GetPlayerdata();
        SetButton();
        SetData();
    }
    // Update is called once per frame
    void SetButton()
    {
        if (player.oxygen_price < player.currency)
        {
            oxygenUpdateButton.onClick.RemoveAllListeners();
            oxygenUpdateButton.onClick.AddListener(OnOxygenUpdateButtonClick);
        }
        else
        {
            oxygenUpdateButton.interactable = false;
        }



        if (player.length_price < player.currency)
        {
            ropeUpdateButton.onClick.RemoveAllListeners();
            ropeUpdateButton.onClick.AddListener(OnRopeUpdateButtonClick);
        }
        else
        {
            ropeUpdateButton.interactable = false;
        }



        if (player.speed_price < player.currency)
        {
            speedUpdateButton.onClick.RemoveAllListeners();
            speedUpdateButton.onClick.AddListener(OnSpeedUpdateButtonClick);
        }
        else
        {
            speedUpdateButton.interactable = false;
        }


        if (player.power_price < player.currency)
        {
            powerdUpdateButton.onClick.RemoveAllListeners();
            powerdUpdateButton.onClick.AddListener(OnPowerUpdateButtonClick);
        }
        else
        {
            powerdUpdateButton.interactable = false;
        }

    }

    void SetData()
    {
        currencyText.text = GetPriceMinify(player.currency);
        oxygenPriceText.text = GetPriceMinify(player.oxygen_price);
        oxygenLevelText.text = player.oxygen.ToString() + "%";
        oxygenLevelMeterImage.fillAmount = (float)player.oxygen / 100;
        ropePriceText.text = GetPriceMinify(player.length_price);

        speedPriceText.text = GetPriceMinify(player.speed_price);

        powerPriceText.text = GetPriceMinify(player.power_price);
    }

    void OnOxygenUpdateButtonClick()
    {
        player.oxygen += 2;
        player.currency -= player.oxygen_price;
        player.oxygen_price += (int)((player.oxygen_price * priceIncreaseFactor) / priceIncreaseRatio);
        GameManager.Instance.Data.SetData(player);
        ResetData();
    }

    void OnRopeUpdateButtonClick()
    {
        player.length += 2;
        player.currency -= player.length_price;
        player.length_price += (int)((player.oxygen_price * priceIncreaseFactor) / priceIncreaseRatio);
        GameManager.Instance.Data.SetData(player);
        ResetData();
    }

    void OnSpeedUpdateButtonClick()
    {
        player.speed += 2;
        player.currency -= player.speed_price;
        player.speed_price += (int)((player.speed_price * priceIncreaseFactor) / priceIncreaseRatio);
        GameManager.Instance.Data.SetData(player);
        ResetData();
    }

    void OnPowerUpdateButtonClick()
    {
        player.power += 2;
        player.currency -= player.power_price;
        player.power_price += (int)((player.power_price * priceIncreaseFactor) / priceIncreaseRatio);
        GameManager.Instance.Data.SetData(player);
        ResetData();
    }

    string GetPriceMinify(int amount)
    {
        if (amount > 1000)
        {
            return amount / 1000 + "k";
        }
        else if (amount > 1000000)
        {
            return amount / 1000000 + "m";
        }
        else
        {
            return amount.ToString();
        }
    }

}
