class Student:
	def __init__(self):
		self.fullName = None
		self.course = None
		self.group = None
		self.favouriteSubject = None
		self.grades = None
		self.InputInfo()

	def PrintInfo(self):
		print(f"  Повне ім'я: {self.fullName}")
		print(f"  Курс: {self.course}")
		print(f"  Група: {self.group}")
		print(f"  Улюблений предмет: {self.favouriteSubject}")
		print(f"  Оцінки:")
		if self.grades is None:
			print("    Немає")
		else:
			for subject in self.grades.keys():
				print(f"    {subject}: {str(self.grades[subject])}")

	def PrintPerformance(self):
		if self.grades is None:
			print("У даного студента немає оцінок!")
		else:
			for subject in self.grades.keys():
				gradeSum = 0
				for grade in self.grades[subject]:
					gradeSum += grade
				averageGrade = gradeSum / len(self.grades[subject])
				print(f"  {subject}: Середня оцінка - {str(averageGrade)}; {"Усе добре!" if averageGrade > 60 else "Потрібно краще вчитися!"}")

	def InputInfo(self):
		print("Введіть інформацію про студента (або залишіть ввід пустим для зберігання попередньої інформації (якщо вона взагалі є))")

		while True:
			newFullName = input("  Повне ім'я: ")
			if newFullName == "":
				if self.fullName is not None:
					break
			else:
				self.fullName = newFullName
				break

		while True:
			newCourse = input("  Курс: ")
			if newCourse == "":
				if self.course is not None:
					break
			else:
				try:
					self.course = newCourse
					break
				except ValueError:
					print("Введіть ціле число!")

		while True:
			newGroup = input("  Група: ")
			if newGroup == "":
				if self.group is not None:
					break
			else:
				self.group = newGroup
				break

		while True:
			newFavouriteSubject = input("  Улюблений предмет: ")
			if newFavouriteSubject == "":
				if self.favouriteSubject is not None:
					break
			else:
				self.favouriteSubject = newFavouriteSubject
				break

	def EditGrades(self):
		while True:
			print("Що Ви хочете зробити зі списком оцінок?")
			print("> 0. Нічого")
			print("> 1. Додати оцінку до предмета [назва предмета]")
			print("> 2. Відредагувати оцінку з предмета [назва предмета]")
			gradesInputLine = input()
			gradesInputLineSplit = gradesInputLine.split()

			try:
				gradesCommand = int(gradesInputLineSplit[0])
			except ValueError:
				print("Введіть ціле число!")
				continue

			gradesSubject = ""
			gradesIsThereSubject = len(gradesInputLineSplit) > 1
			if gradesIsThereSubject:
				gradesSubject = " ".join(gradesInputLineSplit[1:])

			match gradesCommand:
				case 0:
					break
				case 1:
					if self.grades is None:
						self.grades = {}

					if gradesIsThereSubject:
						if gradesSubject not in self.grades:
							self.grades[gradesSubject] = []

						print("Введіть нову оцінку (за 100-бальною системою): ")

						while True:
							try:
								newGrade = int(input())
								break
							except ValueError:
								print("Введіть ціле число!")

						self.grades[gradesSubject].append(newGrade)
					else:
						print("Додайте назву предмета!")
				case 2:
					if gradesIsThereSubject:
						if gradesSubject not in self.grades:
							print("Оцінок з такого предмета немає у даного студента!")
							continue

						print(f"Оцінки даного студента з {gradesSubject}: {str(self.grades[gradesSubject])}")

						print("Введіть індекс, за яким потрібно замінити оцінку: ")

						while True:
							try:
								newGradeIndex = int(input())
								if newGradeIndex < 0 or newGradeIndex >= len(self.grades[gradesSubject]):
									print("Введіть індекс, що існує в даному списку!")
								else:
									break
							except ValueError:
								print("Введіть ціле число!")

						print("Введіть нову оцінку: ")

						while True:
							try:
								newGrade = int(input())
								break
							except ValueError:
								print("Введіть ціле число!")

						self.grades[gradesSubject][newGradeIndex] = newGrade
					else:
						print("Додайте назву предмета!")
				case _:
					print("Невідома команда!")



students = []



while True:
	print("Що Ви хочете зробити? (ціле число):")
	print("> 0. Завершити програму")
	print("> 1. Вивести інформацію про всіх студентів")
	print("> 2. Вивести інформацію про студента [індекс]")
	print("> 3. Додати нового студента")
	print("> 4. Відредагувати дані про студента [індекс]")
	print("> 5. Додати/Відредагувати оцінки студента [індекс]")
	print("> 6. Перевірити успішність студента [індекс]")
	inputLine = input()
	inputLineSplit = inputLine.split()

	try:
		command = int(inputLineSplit[0])
	except ValueError:
		print("Введіть ціле число!")
		continue

	argument = ""
	isThereArgument = len(inputLineSplit) > 1
	if isThereArgument:
		try:
			argument = int(inputLineSplit[1])
		except ValueError:
			print("Введіть ціле число!")
			continue

	match command:
		case 0:
			break
		case 1:
			for i in range(len(students)):
				print(f"Інформація про студента (індекс {str(i)}):")
				students[i].PrintInfo()
		case 2:
			if isThereArgument:
				if argument < 0 or argument >= len(students):
					print("Введіть індекс, що існує в даному списку!")
				else:
					students[argument].PrintInfo()
			else:
				print("Додайте індекс!")
		case 3:
			students.append(Student())
		case 4:
			if isThereArgument:
				if argument < 0 or argument >= len(students):
					print("Введіть індекс, що існує в даному списку!")
				else:
					students[argument].InputInfo()
			else:
				print("Додайте індекс!")
		case 5:
			if isThereArgument:
				if argument < 0 or argument >= len(students):
					print("Введіть індекс, що існує в даному списку!")
				else:
					students[argument].EditGrades()
			else:
				print("Додайте індекс!")
		case 6:
			if isThereArgument:
				if argument < 0 or argument >= len(students):
					print("Введіть індекс, що існує в даному списку!")
				else:
					students[argument].PrintPerformance()
			else:
				print("Додайте індекс!")
		case _:
			print("Невідома команда!")