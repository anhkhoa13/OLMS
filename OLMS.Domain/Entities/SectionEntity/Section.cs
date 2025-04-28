using OLMS.Domain.Primitives;

namespace OLMS.Domain.Entities.SectionEntity;

public class Section : Entity {
    #region Properties
    public string Title { get; set; }
    public Guid CourseId { get; set; }
    public int Order {  get; set; }
    #endregion

    private readonly List<Lesson> _lessons = [];
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

    private readonly List<SectionItem> _sectionItems = [];
    public IReadOnlyCollection<SectionItem> SectionItems => _sectionItems.AsReadOnly();

    private readonly List<Assignment> _assignments = [];
    public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

    // Constructor
    public Section(Guid id, string title, Guid courseId, int order) : base(id) {
        Title = title;
        CourseId = courseId;
        Order = order;
    }

    // Static factory method to create a SectionItem
    public static Section Create(string title, Guid courseId, int order) {
        return new Section(new Guid(), title, courseId, order);
    }

    public SectionItem AddLesson(Lesson lesson, int order) {
        // Dồn các order lớn hơn hoặc bằng order mới
        foreach (var item in _sectionItems.Where(item => item.Order >= order).OrderByDescending(item => item.Order)) {
            item.IncreaseOrder();
        }

        _lessons.Add(lesson);
        SectionItem sectionItem = SectionItem.Create(Guid.NewGuid(), lesson.Id, order, SectionItemType.Lesson, Id);
        _sectionItems.Add(sectionItem);
        return sectionItem;
    }

    public SectionItem AddAssigment(Assignment assignment, int order) {
        foreach (var item in _sectionItems.Where(item => item.Order >= order).OrderByDescending(item => item.Order)) {
            item.IncreaseOrder();
        }

        _assignments.Add(assignment);
        SectionItem sectionItem = SectionItem.Create(Guid.NewGuid(), assignment.Id, order, SectionItemType.Assignment, Id);
        _sectionItems.Add(sectionItem);
        return sectionItem;
    }

    public void RemoveItem(Guid itemId) {
        var item = _sectionItems.FirstOrDefault(i => i.ItemId == itemId);
        if (item != null) {
            _sectionItems.Remove(item);
            foreach (var sectionItem in _sectionItems.Where(i => i.Order > item.Order)) {
                sectionItem.DecreaseOrder();
            }

            switch (item.ItemType) {
                case SectionItemType.Assignment:
                    var assignment = _assignments.FirstOrDefault(a => a.Id == item.ItemId);
                    if (assignment != null) {
                        _assignments.Remove(assignment);
                    }
                    break;
                case SectionItemType.Lesson:
                    var lesson = _lessons.FirstOrDefault(l => l.Id == item.ItemId);
                    if (lesson != null) {
                        _lessons.Remove(lesson);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid item type");
            }
        }
    }
}
