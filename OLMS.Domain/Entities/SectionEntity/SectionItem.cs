using OLMS.Domain.Primitives;
public class SectionItem : Entity {
    public int Order { get; set; }
    public Guid ItemId { get; set; }
    public Guid SectionId { get; set; }

    public SectionItem() { }

    public SectionItem(Guid id, int oder, Guid itemId, Guid sectionId) : base(id) {
        this.Order = oder;
        this.ItemId = itemId;
        this.SectionId = sectionId;
    }
}


