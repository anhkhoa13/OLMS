using OLMS.Domain.Primitives;


public class SectionItem : Entity
{
    public int Order { get; set; }
    public SectionItemType ItemType { get; private set; }
    public Guid ItemId { get; private set; }
    public Guid SectionId { get; private set; }

    private SectionItem() : base() { }

    private SectionItem(Guid id, Guid itemId, int order, SectionItemType itemType, Guid sectionId) : base(id)
    {
        ItemId = itemId;
        Order = order;
        ItemType = itemType;
        SectionId = sectionId;
    }

    public static SectionItem Create(Guid id, Guid itemId, int order, SectionItemType itemType, Guid sectionId)
    {
        return new SectionItem(id, itemId, order, itemType, sectionId);
    }

    public void IncreaseOrder() => Order++;
    public void DecreaseOrder() => Order--;
}
