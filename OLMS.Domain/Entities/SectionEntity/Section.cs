using OLMS.Domain.Entities.CourseAggregate;
using OLMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLMS.Domain.Entities.SectionEntity {
    public class Section : Entity {
        public string Title { get; set; }
        public Guid CourseId { get; set; }

        private readonly List<Lesson> _lessons = [];
        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

        private readonly List<SectionItem> _sectionItems = [];
        public IReadOnlyCollection<SectionItem> SectionItems => _sectionItems.AsReadOnly();

        private readonly List<Assignment> _assignments = [];
        public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

        // Constructor
        public Section(Guid id, string title, Guid courseId) : base(id) {
            Title = title;
            CourseId = courseId;
        }

        // Static factory method to create a SectionItem
        public static Section Create(string title, Guid courseId) {
            return new Section(new Guid(), title, courseId);
        }

        public void AddLesson(string title, string content, string videoUrl) {
            Lesson lesson = Lesson.Create(title, content, videoUrl, this.Id);
            _lessons.Add(lesson);
        }

        public void AddSectionItem(int order, Guid itemId, Guid sectionId) {
            SectionItem sectionItem = new SectionItem(Guid.NewGuid(),order,itemId, sectionId);
            _sectionItems.Add(sectionItem);
        }
    }
}
