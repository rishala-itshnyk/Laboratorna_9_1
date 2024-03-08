using System.Runtime.InteropServices;

namespace Laboratorna_9_1
{
    public enum Specialization
    {
        ComputerScience,
        Informatics,
        MathematicsEconomics,
        PhysicsInformatics,
        LaborTraining
    }

    public struct Marks
    {
        public int Physics;
        public int Mathematics;
        public int Informatics;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct AdditionalMarks
    {
        [FieldOffset(0)] public int Programming;

        [FieldOffset(4)] public int NumericalMethods;

        [FieldOffset(8)] public int Pedagogy;
    }

    public struct StudentLevel
    {
        public int StudentNumber;
        public string LastName;
        public int Course;
        public Specialization Specialization;
        public Marks SubjectMarks;
        public AdditionalMarks AdditionalMarks;
    }

    public class Program
    {
        static void Main()
        {
            Console.Write("Введіть к-сть студентів: ");
            int numberOfStudents = int.Parse(Console.ReadLine());

            StudentLevel[] studentsLevel = new StudentLevel[numberOfStudents];

            Create(studentsLevel);

            Console.WriteLine("\nТаблиця:");
            Print(studentsLevel);

            ComputeAverageMarks(studentsLevel);
            CountStudentsWithHighPhysicsMarks(studentsLevel);
        }

        public static void Create(StudentLevel[] students)
        {
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"\nВкажіть дані студента {i + 1}:");
                students[i].StudentNumber = i + 1;

                Console.Write("Прізвище Студента: ");
                students[i].LastName = Console.ReadLine();

                Console.Write("Курс: ");
                students[i].Course = int.Parse(Console.ReadLine());

                Console.Write(
                    "Спеціалізація (0-Комп'ютерні науки, 1-Інформатика, 2-Математика та Економіка, 3-Фізика та Інформатика, 4-Трудове навчання): ");
                students[i].Specialization = (Specialization)Enum.Parse(typeof(Specialization), Console.ReadLine());

                Console.Write("Оцінка з Фізика: ");
                students[i].SubjectMarks.Physics = int.Parse(Console.ReadLine());

                Console.Write("Оцінка з Математики: ");
                students[i].SubjectMarks.Mathematics = int.Parse(Console.ReadLine());

                switch (students[i].Specialization)
                {
                    case Specialization.ComputerScience:
                        Console.Write("Оцінка з Програмування: ");
                        students[i].AdditionalMarks.Programming = int.Parse(Console.ReadLine());
                        break;
                    case Specialization.Informatics:
                        Console.Write("Оцінка з Чисельних Методів: ");
                        students[i].AdditionalMarks.NumericalMethods = int.Parse(Console.ReadLine());
                        break;
                    default:
                        Console.Write("Оцінка з Педагогіки: ");
                        students[i].AdditionalMarks.Pedagogy = int.Parse(Console.ReadLine());
                        break;
                }
            }
        }

        public static void Print(StudentLevel[] students)
        {
            Console.WriteLine(
                "\n| № | Прізвище | Курс | Спеціалізація | Фізика | Математика | Програмування | Чисельні Методи | Педагогіка |");
            Console.WriteLine(
                "--------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(
                    $"| {students[i].StudentNumber,2} | {students[i].LastName,-10} | {students[i].Course,4} | {students[i].Specialization,-13} | {students[i].SubjectMarks.Physics,6} | {students[i].SubjectMarks.Mathematics,10} | {students[i].AdditionalMarks.Programming,13} | {students[i].AdditionalMarks.NumericalMethods,14} | {students[i].AdditionalMarks.Pedagogy,10} |");
            }
        }

        public static void ComputeAverageMarks(StudentLevel[] students)
        {
            double averagePhysics = students.Any() ? students.Average(s => s.SubjectMarks.Physics) : 0;
            double averageMathematics = students.Any() ? students.Average(s => s.SubjectMarks.Mathematics) : 0;
            double averageProgramming = students.Where(s => s.Specialization == Specialization.ComputerScience).Any()
                ? students.Where(s => s.Specialization == Specialization.ComputerScience)
                    .Average(s => s.AdditionalMarks.Programming)
                : 0;
            double averageNumericalMethods = students.Where(s => s.Specialization == Specialization.Informatics).Any()
                ? students.Where(s => s.Specialization == Specialization.Informatics)
                    .Average(s => s.AdditionalMarks.NumericalMethods)
                : 0;
            double averagePedagogy = students.Where(s => s.Specialization == Specialization.LaborTraining).Any()
                ? students.Where(s => s.Specialization == Specialization.LaborTraining)
                    .Average(s => s.AdditionalMarks.Pedagogy)
                : 0;

            Console.WriteLine(
                $"\nСередні оцінки з різних предметів:\nФізика: {averagePhysics}\nМатематика: {averageMathematics}\nСередня Оцінка з Програмування: {averageProgramming}\nСередня Оцінка з Чисельних Методів: {averageNumericalMethods}\nСередня Оцінка з Педагогіки: {averagePedagogy}");
        }

        public static void CountStudentsWithHighPhysicsMarks(StudentLevel[] students)
        {
            int highPhysicsCount = students.Count(s => s.SubjectMarks.Physics == 5 || s.SubjectMarks.Physics == 4);

            Console.WriteLine($"\nК-сть студентів з високою оцінкою з Фізики (5 або 4): {highPhysicsCount}");
        }
    }
}