using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Circle.Settings.NoSql
{
    public class CircleAssetEntity : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-circle-asset";

        public static string GeneratePartitionKey(string brokerId) => $"broker:{brokerId}";
        public static string GenerateRowKey(string asset) => asset;

        public string BrokerId { get; set; }
        public string CircleAsset { get; set; }
        public string AssetSymbol { get; set; }
        public string CircleWalletId { get; set; }

        public static CircleAssetEntity Create(CircleAssetEntity circleAssetEntity)
        {
            var entity = new CircleAssetEntity()
            {
                PartitionKey = GeneratePartitionKey(circleAssetEntity.BrokerId),
                RowKey = GenerateRowKey(circleAssetEntity.CircleAsset),
                BrokerId = circleAssetEntity.BrokerId,
                CircleAsset = circleAssetEntity.CircleAsset,
                AssetSymbol = circleAssetEntity.AssetSymbol,
                CircleWalletId = circleAssetEntity.CircleWalletId
            };

            return entity;
        }
    }
}