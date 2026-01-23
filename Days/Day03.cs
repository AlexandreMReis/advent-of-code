namespace AdvendOfCode.Days;

public class Day03
{

    public long Run(bool runDemo = false)
    {
        var banksStr = string.Empty;
        if (runDemo)
        {
            banksStr = "987654321111111\r\n811111111111119\r\n234234234234278\r\n818181911112111";
        }
        else
        {
            banksStr = File.ReadAllText("input-d3-p1.txt");
        }

        var banks = banksStr.Split("\r\n").ToList();

        var banksHighestJoltages = GetBanksHighestJoltages(banks);

        return banksHighestJoltages.Sum();
    }

    private List<long> GetBanksHighestJoltages(List<string> banks)
    {
        var banksHighestJoltages = new List<long>();
        
        foreach(var bank in banks)
        {
            var highestBatteryJoltage = -1;
            var secondHighestBatteryJoltage = -1;
            for (int i = 0; i < bank.Length - 1; i++)
            {
                int currentBatteryJoltage = int.Parse(bank[i].ToString());
                if(currentBatteryJoltage > highestBatteryJoltage)
                {
                    highestBatteryJoltage = currentBatteryJoltage;
                    for (int j = i + 1; j < bank.Length; j++)
                    {
                        int currentSecondBatteryJoltage = int.Parse(bank[j].ToString());
                        if (currentSecondBatteryJoltage > secondHighestBatteryJoltage)
                        {
                            secondHighestBatteryJoltage = currentSecondBatteryJoltage;
                        }
                    }
                }
            }

            banksHighestJoltages.Add(int.Parse(highestBatteryJoltage.ToString() + secondHighestBatteryJoltage.ToString()));
        }

        return banksHighestJoltages;
    }
}
