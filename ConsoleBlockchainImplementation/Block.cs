using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlockchainImplementation
{
    public class Block
    {
        
        private readonly DateTime dateTime;
        private long nonce;
        public string PreviousHash { get; set; }
        public List<Transaction> Transactions {get; set;}
        public string Hash { get; set;}

        
        public Block(DateTime timestamp, List<Transaction> transactions,string previousHash="")
        {
            dateTime = timestamp;
            nonce = 0;

            Transactions = transactions;
            PreviousHash = previousHash;
            Hash = CreateHash();
        }
        public string CreateHash()
        {
            using (SHA256 sHA = SHA256.Create())
            {
                string rawData = PreviousHash + dateTime + nonce + Transactions;
                byte[] encoded = sHA.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Encoding.Default.GetString(encoded);
            }
            
        }
        public void MineBlock(int _difficulty)
        {
            string hashValidationTemplate = new String('0', _difficulty);
            while (Hash.Substring(0,_difficulty) != hashValidationTemplate)
            {
                nonce++;
                Hash = CreateHash();
            }
            Console.WriteLine("Blocked with HASH={0} successfully mined!", Hash);
        }

    }
}
