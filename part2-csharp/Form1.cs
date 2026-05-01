namespace sisteminis_programavimas_2
{
    public partial class Form1 : Form
    {
        public class Student
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public List<int> Homework { get; set; }
            public int Exam { get; set; }
            public float Result { get; private set; }

            // Konstruktoriai, Vidurkio ir Medianos skaičiavimo metodai...
            public void CalculateResult(char method)
            {
                if (Homework == null || !Homework.Any())
                {
                    this.Result = 0; // or handle it as you see fit
                    return;
                }

                float homeworkResult = method == 'v' ? (float)Homework.Average() : CalculateMedian(Homework);
                this.Result = 0.4f * homeworkResult + 0.6f * Exam;
            }

            private float CalculateMedian(List<int> scores)
            {
                int numberCount = scores.Count();
                int halfIndex = scores.Count() / 2;
                var sortedNumbers = scores.OrderBy(n => n).ToList();

                if ((numberCount % 2) == 0)
                {
                    // For even number of scores, average the two middle elements
                    return (sortedNumbers[halfIndex] + sortedNumbers[halfIndex - 1]) / 2f;
                }
                else
                {
                    // For odd number of scores, return the middle element
                    return sortedNumbers[halfIndex];
                }
            }

            public override string ToString()
            {
                // Return a string representation of the student, for file output
                return $"{Name} {Surname}: Result = {Result}";
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private (List<Student> vargsiukai, List<Student> kietekai) CategorizeStudents(List<Student> students)
        {
            List<Student> vargsiukai = students.Where(s => s.Result < 5.0).ToList();
            List<Student> kietekai = students.Where(s => s.Result >= 5.0).ToList();

            return (vargsiukai, kietekai);
        }

        private List<Student> ParseStudents(string text)
        {
            List<Student> students = new List<Student>();
            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3) continue; // Skip invalid lines

                var student = new Student
                {
                    Name = parts[0].Trim(),
                    Surname = parts[1].Trim(),
                    Homework = parts.Skip(2).Take(parts.Length - 3).Select(int.Parse).ToList(),
                    Exam = int.Parse(parts[parts.Length - 1].Trim())
                };
                student.CalculateResult('v'); // Assuming this is a method to calculate the result

                students.Add(student);
            }

            return students;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Student> students = ParseStudents(textBox1.Text);
            var (vargsiukai, kietekai) = CategorizeStudents(students);

            File.WriteAllLines("Vargsiukai.txt", vargsiukai.Select(s => s.ToString()));
            File.WriteAllLines("Kietekai.txt", kietekai.Select(s => s.ToString()));

            MessageBox.Show("Failai 'Vargsiukai.txt' ir 'Kietekai.txt' buvo sugeneruota", "Failai sugeneruoti", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutText = "Programa sukure Julius Berzinskas. VVK";
            MessageBox.Show(aboutText, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}