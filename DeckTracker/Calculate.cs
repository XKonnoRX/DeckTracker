using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckTracker
{
    internal class Calculate //класс с расчетом комбинаций и эквити (не доделано)
    {
        public static List<string> GetPack(string[] startPack, UserOutput user, List<UserOutput> team, string deck)
        {
            var result = new List<string>();
            foreach (var file in startPack)
            {
                var card = Path.GetFileName(file).Replace(".png", "");
                if (!user.Hand.Text.Contains(card) && !deck.Contains(card) && !card.Contains("nf"))
                {
                    bool exist = false;
                    foreach (var hand in team)
                        if (hand.Hand.Text.Contains(card))
                        {
                            exist = true;
                            break;
                        }
                    if (!exist)
                        result.Add(card);

                }
            }
            return result;
        }

        public static bool CheckReapeted(string cards)
        {
            var buf = cards;
            for (int i = 0; i < buf.Length / 2; i++)
            {
                string card = buf[0].ToString() + buf[1];
                buf = buf.Substring(2);
                if (buf.Contains(card))
                    return true;
            }
                return false;
        }
        

        public static double Eqity(List<string> Pack, string user, string enemy, string deck)
        {
            List<string> brootPack = new List<string>(Pack);
            double result = 0;
            int rank = 0;
            int count = 0;
            if (deck.Length == 6)
            {
                while(brootPack.Count>0)
                {
                    Eqity(brootPack, user, enemy, deck + brootPack[0]);
                    brootPack.RemoveAt(0);
                }
            }
            if (deck.Length == 8)
            {
                for (int i = 1; i < Pack.Count; i++)
                {
                    Eqity(brootPack, user, enemy, deck + brootPack[0]);
                }
                 brootPack.RemoveAt(0);
            }
            if (deck.Length == 10)
            {
                string brootDeck = deck;
                while (brootDeck.Length > 0)
                {
                    for (int t = 1; t < brootDeck.Length; t++)
                    {

                    }
                    brootDeck = brootDeck.Substring(2);
                }
            }
            return 0;
        }
        private static string CheckCombo(string user, string table)
        {
            string userNum = "";
            string userSuit = "";
            string tableNum = "";
            string tableSuit = "";
            for(int i = 0; i<5;i++)
            {
                userNum += user[i*2];
                userSuit += user[i * 2+1];
            }
            for (int i = 0; i < 5; i++)
            {
                tableNum += table[i * 2];
                tableSuit += table[i * 2 + 1];
            }
            IsFlash(tableSuit, tableSuit);
            return "";
        }
        private static bool IsFlash(string user, string table)
        {
            int[] count = {0,0,0,0};
            char[] cards = { 'd', 's', 'h', 'c' };
            foreach(var tableCard in table.ToCharArray())
            {
                count[Array.IndexOf(cards, tableCard)] ++;
            }
            if(count.Contains(3))
            {
                foreach (var userCard in user.ToCharArray())
                {
                    count[Array.IndexOf(cards, userCard)]++;
                }
            }
            if (count.Contains(5))
                return true;
            return false;
        }
    }
}
