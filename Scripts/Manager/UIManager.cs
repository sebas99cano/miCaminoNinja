using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")] [SerializeField] private CharacterStats stats;
    [Header("Panels")] [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelInventory;

    [Header("Images")] [SerializeField] private Image life;
    [SerializeField] private Image chakra;
    [SerializeField] private Image experience;

    [Header("Texts")] [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI chakraText;
    [SerializeField] private TextMeshProUGUI experienceText;

    [Header("CharacterStats")] [SerializeField]
    private TextMeshProUGUI damageStatsText;

    [SerializeField] private TextMeshProUGUI defenseStatsText;
    [SerializeField] private TextMeshProUGUI chakraStatsText;
    [SerializeField] private TextMeshProUGUI dodgeStatsText;
    [SerializeField] private TextMeshProUGUI velocityStatsText;
    [SerializeField] private TextMeshProUGUI levelStatsText;
    [SerializeField] private TextMeshProUGUI experienceStatsText;
    [SerializeField] private TextMeshProUGUI needExperienceStatsText;
    [SerializeField] private TextMeshProUGUI ninjutsuStatsText;
    [SerializeField] private TextMeshProUGUI genjutsuStatsText;
    [SerializeField] private TextMeshProUGUI taijutsuStatsText;
    [SerializeField] private TextMeshProUGUI pointsStatsText;


    private float _actualLife;
    private float _maxLife;
    private float _actualChakra;
    private float _maxChakra;
    private float _actualExperience;
    private float _requiredExperience;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenCloseStatsPanel();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenCloseInventoryPanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelInventory.activeSelf)
            {
                OpenCloseInventoryPanel();
            }

            if (panelStats.activeSelf)
            {
                OpenCloseStatsPanel();
            }
        }

        UpdateUICharacter();
        UpdatePanelStats();
    }

    private void UpdateUICharacter()
    {
        life.fillAmount =
            Mathf.Lerp(life.fillAmount, _actualLife / _maxLife, 10f * Time.deltaTime);
        chakra.fillAmount =
            Mathf.Lerp(chakra.fillAmount, _actualChakra / _maxChakra, 10f * Time.deltaTime);
        experience.fillAmount =
            Mathf.Lerp(experience.fillAmount, _actualExperience / _requiredExperience, 10f * Time.deltaTime);

        levelText.text = $"Level {stats.Level}";
        lifeText.text = $"{_actualLife:F0}/{_maxLife}";
        chakraText.text = $"{_actualChakra}/{_maxChakra}";
        experienceText.text = $"{((_actualExperience / _requiredExperience) * 100):F1} % ";
    }

    private void UpdatePanelStats()
    {
        if (panelStats.activeSelf == false)
        {
            return;
        }

        damageStatsText.text = stats.Damage.ToString();
        defenseStatsText.text = stats.Defense.ToString();
        chakraStatsText.text = stats.Chakra.ToString();
        dodgeStatsText.text = stats.Dodge.ToString();
        velocityStatsText.text = stats.Velocity.ToString();
        levelStatsText.text = stats.Level.ToString();
        experienceStatsText.text = stats.Experience.ToString();
        needExperienceStatsText.text = stats.NeededExperience.ToString();

        ninjutsuStatsText.text = stats.Ninjutsu.ToString();
        genjutsuStatsText.text = stats.Genjutsu.ToString();
        taijutsuStatsText.text = stats.Taijutsu.ToString();

        pointsStatsText.text = $"Points : {stats.points}";
    }

    public void UpdateCharacterLife(float pActualLife, float pMaxLife)
    {
        _actualLife = pActualLife;
        _maxLife = pMaxLife;
    }

    public void UpdateCharacterChakra(float pActualChakra, float pMaxChakra)
    {
        _actualChakra = pActualChakra;
        _maxChakra = pMaxChakra;
    }

    public void UpdateCharacterExperience(float pActualExperience, float pRequiredExperience)
    {
        _actualExperience = pActualExperience;
        _requiredExperience = pRequiredExperience;
    }

    #region Paneles

    public void OpenCloseStatsPanel()
    {
        if (panelInventory.activeSelf && !panelStats.activeSelf)
        {
            panelInventory.SetActive(false);
            panelStats.SetActive(true);
        }
        else
        {
            panelStats.SetActive(!panelStats.activeSelf);
        }
    }

    public void OpenCloseInventoryPanel()
    {
        if (!panelInventory.activeSelf && panelStats.activeSelf)
        {
            panelStats.SetActive(false);
            panelInventory.SetActive(true);
        }
        else
        {
            panelInventory.SetActive(!panelInventory.activeSelf);
        }
    }

    #endregion
}