using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Circle.Settings.NoSql
{
    public class CircleBlockchainEntity : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-circle-blockchain";

        public static string GeneratePartitionKey(string brokerId) => $"broker:{brokerId}";
        public static string GenerateRowKey(string blockchain) => blockchain;

        public string BrokerId { get; set; }
        public string CircleBlockchain { get; set; }
        public string Blockchain { get; set; }

        public static CircleBlockchainEntity Create(CircleBlockchainEntity circleBlockchainEntity)
        {
            var entity = new CircleBlockchainEntity()
            {
                PartitionKey = GeneratePartitionKey(circleBlockchainEntity.BrokerId),
                RowKey = GenerateRowKey(circleBlockchainEntity.CircleBlockchain),
                BrokerId = circleBlockchainEntity.BrokerId,
                CircleBlockchain = circleBlockchainEntity.CircleBlockchain,
                Blockchain = circleBlockchainEntity.Blockchain
            };

            return entity;
        }
    }
}