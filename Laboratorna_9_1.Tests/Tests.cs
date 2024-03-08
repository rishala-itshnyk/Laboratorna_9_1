using NUnit.Framework;

namespace Laboratorna_9_1.Tests;

[TestFixture]
public class Tests
{
    [Test]
    public void Print()
    {
        StudentLevel[] students = { new StudentLevel { StudentNumber = 1, LastName = "Лисяк", Course = 1, Specialization = Specialization.Informatics, SubjectMarks = new Marks { Physics = 5, Mathematics = 5 }, AdditionalMarks = new AdditionalMarks { NumericalMethods = 5 } } };

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.Print(students);

            var result = sw.ToString();

            Assert.IsTrue(result.Contains("| \u2116 | Прізвище | Курс | Спеціалізація | Фізика | Математика | Програмування | Чисельні Методи | Педагогіка |"));
            Assert.IsTrue(result.Contains("|  1 | Лисяк      |    1 | Informatics   |      5 |          5 |             0 |              5 |          0 |\n"));

            Console.SetOut(Console.Out);
        }
    }
    
    [Test]
    public void Create()
    {
        StudentLevel[] students = new StudentLevel[1];

        using (var sr = new StringReader("Doe\n2\n0\n4\n5\n5"))
        using (var sw = new StringWriter())
        {
            Console.SetIn(sr);
            Console.SetOut(sw);
            Program.Create(students);

            Assert.AreEqual("Doe", students[0].LastName);
            Assert.AreEqual(2, students[0].Course);
            Assert.AreEqual(Specialization.ComputerScience, students[0].Specialization);
            Assert.AreEqual(4, students[0].SubjectMarks.Physics);
            Assert.AreEqual(5, students[0].SubjectMarks.Mathematics);
            Assert.AreEqual(5, students[0].AdditionalMarks.Programming);

            Console.SetOut(Console.Out);
        }
    }

    [Test]
    public void ComputeAverageMarks()
    {
        StudentLevel[] students = new StudentLevel[0];

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.ComputeAverageMarks(students);

            var result = sw.ToString().Trim();

            Assert.AreEqual("Середні оцінки з різних предметів:\nФізика: 0\nМатематика: 0\nСередня Оцінка з Програмування: 0\nСередня Оцінка з Чисельних Методів: 0\nСередня Оцінка з Педагогіки: 0", result);
        }
    }

    [Test]
    public void CountStudentsWithHighPhysicsMarks()
    {
        StudentLevel[] students = new StudentLevel[0];

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            Program.CountStudentsWithHighPhysicsMarks(students);

            var result = sw.ToString().Trim();

            Assert.AreEqual("К-сть студентів з високою оцінкою з Фізики (5 або 4): 0", result);
        }
    }
}