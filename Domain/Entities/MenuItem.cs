namespace InvoicingAPI.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;

        // NEW — Parent Menu
        public Guid? ParentId { get; set; }
        public MenuItem? Parent { get; set; }

        // NEW — Children Menus
        public ICollection<MenuItem> Children { get; set; } = new List<MenuItem>();
    }
}
