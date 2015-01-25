using XamarinDroidCustomListView.BusinessLayer.Contracts;
using XamarinDroidCustomListView.DataAccess;

namespace XamarinDroidCustomListView.Model
{
    public class ServiceItem : IBusinessEntity
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}