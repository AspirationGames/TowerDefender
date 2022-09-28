using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int StartingBalance = 150;
    [SerializeField] int currentBalance;
    [SerializeField] TextMeshProUGUI bankText;

    public int CurrentBalance => currentBalance;
    
    private void Awake() 
    {
            
    }
    private void Start() 
    {
        currentBalance = StartingBalance;
        UpdateBankText();    
    }
    public void IncreaseBalance(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBankText();  
    }

    public void DecreaseBalance(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBankText();  
        if(currentBalance < 0)
        {
            HandleNegativeBalance();
        }
    }

    public void UpdateBankText()
    {
        bankText.text = $"Gold: {currentBalance}";
    }

    void HandleNegativeBalance()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
