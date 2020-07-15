using System;
using System.IO;
namespace hacknet_startfolder
{
    public class dataManagement
    {
        //saves the account Name + Password.
        //returns 0 if all worked
        //returns 1 if the account already exists
        public int safeNewAccount(string accountName, string accountPassword)
        {
            string[] accountData = new string[2];
            try
            {
                accountData = File.ReadAllLines("account.txt");
            }
            catch { }

            foreach(string line in accountData)
            {
                if (line == accountName + "$" + accountPassword)
                    return 1;
            }

            if (accountData[0] != null)
                accountData[0] = Convert.ToString(Convert.ToInt16(accountData[0]) + 1);           
            else
                accountData[0] = "1";

            string save = "";

            int temp = 0;
            foreach(string item in accountData)
            {
                if (temp == 0)
                    save += item + "\n";
                else
                    save += item;
                temp++;
            }

            save += "\n" + accountName + "$" + accountPassword;
            File.WriteAllText("account.txt", save);
            return 0;
        }

        //returns all saved accounts in an multidimensional array: [[accountName][accountPassword], ...]
        //first array segment lists the amount of existing accounts
        public string[,] getAccounts()
        {
            string[] accountData = new string[0];
            string[,] returnArray = new string[0, 0];
            try
            {
                accountData = File.ReadAllLines("account.txt");
            }
            catch { return returnArray; }

            returnArray = new string[accountData.Length, 2];

            int i = 0;
            foreach(string line in accountData)
            {
                try
                {
                    string[] temp = line.Split('$');
                    returnArray[i, 0] = temp[0];
                    returnArray[i, 1] = temp[1];
                }
                catch
                {
                    returnArray[i, 0] = line;
                    returnArray[i, 1] = "";
                }
                i++;
            }
            return returnArray;
        }
    }
}
