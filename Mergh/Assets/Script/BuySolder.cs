using Unity.VisualScripting;
using UnityEngine;

public class BuySolder : MonoBehaviour
{
    public Wallet Wallet;
    public RandomObjectSpawner Spawner;

    public void TryBuySolder()
    {
        if(Wallet.CoinCount >= 10)
        {
            Wallet.RemoveCoins(10);
            Spawner.SpawnRandomObject();
        }
        else
        {
            print("No Many");
        }
    }
}
