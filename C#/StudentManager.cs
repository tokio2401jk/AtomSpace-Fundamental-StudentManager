using System;
using System.Diagnostics;

namespace StudentManagerProgram
{
	class StudentManager
	{
		static void Main(string[] args)
		{
			Console.InputEncoding = System.Text.Encoding.Unicode;
			Console.OutputEncoding = System.Text.Encoding.Unicode;

			List<Student> students = [];

			bool run = true;
			do
			{
				Console.WriteLine("Що Ви хочете зробити? (ціле число):");
				Console.WriteLine("> 0. Завершити програму");
				Console.WriteLine("> 1. Вивести інформацію про всіх студентів");
				Console.WriteLine("> 2. Вивести інформацію про студента [індекс]");
				Console.WriteLine("> 3. Додати нового студента");
				Console.WriteLine("> 4. Відредагувати дані про студента [індекс]");
				Console.WriteLine("> 5. Додати/Відредагувати оцінки студента [індекс]");
				Console.WriteLine("> 6. Перевірити успішність студента [індекс]");
				string inputLine = Console.ReadLine();
				string[] inputLineSplit = inputLine.Split();

				int command;
				try
				{
					command = int.Parse(inputLineSplit[0]);
				}
				catch
				{
					Console.WriteLine("Введіть ціле число!");
					continue;
				}

				int argument = -1;
				bool isThereArgument = inputLineSplit.Length > 1;
				if(isThereArgument)
				{
					try
					{
						argument = int.Parse(inputLineSplit[1]);
					}
					catch
					{
						Console.WriteLine("Введіть ціле число!");
						continue;
					}
				}

				switch(command)
				{
					case 0:
						run = false;
						break;

					case 1:
						for(int i = 0; i < students.Count; i++)
						{
							Console.WriteLine($"Інформація про студента (індекс {i}):");
							students[i].PrintInfo();
						}
						break;

					case 2:
						if(isThereArgument)
						{
							if(argument < 0 || argument >= students.Count)
								Console.WriteLine("Введіть індекс, що існує в даному списку!");
							else
								students[argument].PrintInfo();
						}
						else
							Console.WriteLine("Додайте індекс!");
						break;

					case 3:
						students.Add(new());
						break;

					case 4:
						if(isThereArgument)
						{
							if(argument < 0 || argument >= students.Count)
								Console.WriteLine("Введіть індекс, що існує в даному списку!");
							else
								students[argument].InputInfo();
						}
						else
							Console.WriteLine("Додайте індекс!");
						break;

					case 5:
						if(isThereArgument)
						{
							if(argument < 0 || argument >= students.Count)
								Console.WriteLine("Введіть індекс, що існує в даному списку!");
							else
								students[argument].EditGrades();
						}
						else
							Console.WriteLine("Додайте індекс!");
						break;

					case 6:
						if(isThereArgument)
						{
							if(argument < 0 || argument >= students.Count)
								Console.WriteLine("Введіть індекс, що існує в даному списку!");
							else
								students[argument].PrintPerformance();
						}
						else
							Console.WriteLine("Додайте індекс!");
						break;

					default:
						Console.WriteLine("Невідома команда!");
						break;
				}
			} while(run);

			Console.ReadKey();
		}
	}

	public class Student
	{
		public string? fullName;
		public int? course;
		public string? group;
		public string? favouriteSubject;
		public Dictionary<string, List<int>>? grades;

		public Student()
		{
			InputInfo();
		}

		public void PrintInfo()
		{
			Console.WriteLine($"  Повне ім'я: {fullName}");
			Console.WriteLine($"  Курс: {course}");
			Console.WriteLine($"  Група: {group}");
			Console.WriteLine($"  Улюблений предмет: {favouriteSubject}");
			Console.WriteLine($"  Оцінки:");
			if(grades is null)
				Console.WriteLine("    Немає");
			else
				foreach(string subject in grades.Keys)
					Console.WriteLine($"    {subject}: [{string.Join(", ", grades[subject])}]");
		}

		public void PrintPerformance()
		{
			if(grades is null)
				Console.WriteLine("У даного студента немає оцінок!");
			else
			{
				foreach(string subject in grades.Keys)
				{
					int gradeSum = 0;
					foreach(int grade in grades[subject])
						gradeSum += grade;
					double averageGrade = (double)gradeSum / grades[subject].Count;
					Console.WriteLine($"  {subject}: Середня оцінка - {averageGrade}; {(averageGrade > 60 ? "Усе добре!" : "Потрібно краще вчитися!")}");
				}
			}
		}

		public void InputInfo()
		{
			Console.WriteLine("Введіть інформацію про студента (або залишіть ввід пустим для зберігання попередньої інформації (якщо вона взагалі є))");

			do
			{
				Console.Write("  Повне ім'я: ");
				string newFullName = Console.ReadLine();
				if(newFullName == "")
				{
					if(fullName is not null)
						break;
				}
				else
				{
					fullName = newFullName;
					break;
				}
			} while(true);

			do
			{
				Console.Write("  Курс: ");
				string newCourse = Console.ReadLine();
				if(newCourse == "")
				{
					if(course is not null)
						break;
				}
				else
				{
					try
					{
						course = int.Parse(newCourse);
						break;
					}
					catch
					{
						Console.WriteLine("Введіть ціле число!");
					}
				}
			} while(true);

			do
			{
				Console.Write("  Група: ");
				string newGroup = Console.ReadLine();
				if(newGroup == "")
				{
					if(group is not null)
						break;
				}
				else
				{
					group = newGroup;
					break;
				}
			} while(true);

			do
			{
				Console.Write("  Улюблений предмет: ");
				string newFavouriteSubject = Console.ReadLine();
				if(newFavouriteSubject == "")
				{
					if(favouriteSubject is not null)
						break;
				}
				else
				{
					favouriteSubject = newFavouriteSubject;
					break;
				}
			} while(true);
		}

		public void EditGrades()
		{
			bool editGrades = true;
			do
			{
				Console.WriteLine("Що Ви хочете зробити зі списком оцінок?");
				Console.WriteLine("> 0. Нічого");
				Console.WriteLine("> 1. Додати оцінку до предмета [назва предмета]");
				Console.WriteLine("> 2. Відредагувати оцінку з предмета [назва предмета]");
				string gradesInputLine = Console.ReadLine();
				string[] gradesInputLineSplit = gradesInputLine.Split();

				int gradesCommand;
				try
				{
					gradesCommand = int.Parse(gradesInputLineSplit[0]);
				}
				catch
				{
					Console.WriteLine("Введіть ціле число!");
					continue;
				}

				string gradesSubject = "";
				bool gradesIsThereSubject = gradesInputLineSplit.Length > 1;
				if(gradesIsThereSubject)
					gradesSubject = string.Join(' ', gradesInputLineSplit.Skip(1));

				switch(gradesCommand)
				{
					case 0:
						editGrades = false;
						break;

					case 1:
						grades ??= [];

						if(gradesIsThereSubject)
						{
							if(!grades.ContainsKey(gradesSubject))
								grades[gradesSubject] = [];

							Console.WriteLine("Введіть нову оцінку (за 100-бальною системою): ");

							int newGrade;
							do
							{
								try
								{
									newGrade = int.Parse(Console.ReadLine());
									break;
								}
								catch
								{
									Console.WriteLine("Введіть ціле число!");
								}
							} while(true);

							grades[gradesSubject].Add(newGrade);
						}
						else
							Console.WriteLine("Додайте назву предмета!");

						break;

					case 2:
						if(gradesIsThereSubject)
						{
							if(!grades.ContainsKey(gradesSubject))
							{
								Console.WriteLine("Оцінок з такого предмета немає у даного студента!");
								continue;
							}

							Console.WriteLine($"Оцінки даного студента з {gradesSubject}: [{string.Join(", ", grades[gradesSubject])}]");

							Console.WriteLine("Введіть індекс, за яким потрібно замінити оцінку: ");

							int newGradeIndex;
							do
							{
								try
								{
									newGradeIndex = int.Parse(Console.ReadLine());
									if(newGradeIndex < 0 || newGradeIndex >= grades[gradesSubject].Count)
										Console.WriteLine("Введіть індекс, що існує в даному списку!");
									else
										break;
								}
								catch
								{
									Console.WriteLine("Введіть ціле число!");
								}
							} while(true);
							
							Console.WriteLine("Введіть нову оцінку: ");

							int newGrade;
							do
							{
								try
								{
									newGrade = int.Parse(Console.ReadLine());
									break;
								}
								catch
								{
									Console.WriteLine("Введіть ціле число!");
								}
							} while(true);
							
							grades[gradesSubject][newGradeIndex] = newGrade;
						}
						else
							Console.WriteLine("Додайте назву предмета!");

						break;

					default:
						Console.WriteLine("Невідома команда!");
						break;
				}
			} while(editGrades);
		}
	}
}