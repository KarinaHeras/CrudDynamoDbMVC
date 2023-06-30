using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
using WepApiMvcDynamoDb.Models;

namespace WepApiMvcDynamoDb.Services
{
    public class ServiceDynamoDB
    {
        private DynamoDBContext context;

        public ServiceDynamoDB()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateProductoAsync(Producto pro)
        {
            await this.context.SaveAsync<Producto>(pro);
        }

        public async Task DeleteProductoAsync(string idPro)
        {
            await this.context.DeleteAsync<Producto>(idPro);
        }

        public async Task<Producto> FindProductoAsync(string idPro)
        {

            return await this.context.LoadAsync<Producto>(idPro);
        }

        public async Task<List<Producto>> GetProductoAsync()
        {

            Table tabla = this.context.GetTargetTable<Producto>();

            var scanOptions = new ScanOperationConfig();
            var results = tabla.Scan(scanOptions);

            List<Document> data = await results.GetNextSetAsync();

            var productos = this.context.FromDocuments<Producto>(data);
            return productos.ToList();
        }
        public async Task UpdateProductoAsync(Producto pro)
        {
            await this.context.SaveAsync(pro);
        }
    }
}
