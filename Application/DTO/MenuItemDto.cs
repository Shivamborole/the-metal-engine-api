namespace InvoicingAPI.Application.DTO
{
    public class MenuItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentId { get; set; }
    }

}
