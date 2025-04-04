using YourApp.DALL.Models;
using YourApp.DALL;
using Microsoft.EntityFrameworkCore;

namespace YourApp.DALL.Repositories
{
    public class StudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
    }
}
