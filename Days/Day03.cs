namespace AdvendOfCode.Days;

//2nd part
//1st try: 166345822896410

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

        var banksHighestJoltages = GetBanksHighestJoltagesPartTwo(banks);

        return banksHighestJoltages.Sum();
    }

    private List<long> GetBanksHighestJoltagesPartOne(List<string> banks)
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
                    secondHighestBatteryJoltage = -1;
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

    private List<long> GetBanksHighestJoltagesPartTwo(List<string> banks)
    {
        var banksHighestJoltages = new List<long>();

        foreach (var bank in banks)
        {
            string highestVoltage = GetHighestJoltage(bank, 12);

            banksHighestJoltages.Add(long.Parse(highestVoltage));
        }

        return banksHighestJoltages;
    }

    private string GetHighestJoltage(string bank, int nbrBatteriesLeft)
    {
        var highestBatteryJoltage = -1;
        int highestBatteryJoltageIndex = -1;
        for (int i = 0; i < bank.Length; i++)
        {
            if(bank.Length - i < nbrBatteriesLeft)
            {
                break;
            }

            int currentBatteryJoltage = int.Parse(bank[i].ToString());
            if (currentBatteryJoltage > highestBatteryJoltage)
            {
                highestBatteryJoltage = currentBatteryJoltage;
                highestBatteryJoltageIndex = i;
            }
        }

        if(nbrBatteriesLeft == 1)
        {
            return highestBatteryJoltage.ToString();
        }

        return highestBatteryJoltage + GetHighestJoltage(bank[(highestBatteryJoltageIndex + 1)..], --nbrBatteriesLeft);
    }
}
